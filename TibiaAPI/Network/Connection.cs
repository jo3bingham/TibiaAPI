﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ComponentAce.Compression.Libs.zlib;

using Newtonsoft.Json;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network
{
    /// <summary>
    /// The <see cref="Connection"/> class is used to create a proxy between the Tibia client and the game server.
    /// </summary>
    public class Connection : Communication, IDisposable
    {
        private const string loginWebService1100 = "https://secure.tibia.com/services/login.php";
        private const string loginWebService1132 = "https://secure.tibia.com/services/clientservices.php";
        private const string loginWebService1186 = "https://www.tibia.com/services/clientservices.php";

        private readonly object _clientSendLock = new object();
        private readonly object _serverSendLock = new object();
        private readonly object _clientSequenceNumberLock = new object();
        private readonly object _ServerSequenceNumberLock = new object();

        private readonly Client _client;

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly HttpListener _httpListener = new HttpListener();

        private readonly NetworkMessage _clientInMessage;
        private readonly NetworkMessage _clientOutMessage;
        private readonly NetworkMessage _serverInMessage;
        private readonly NetworkMessage _serverOutMessage;

        private readonly Queue<byte[]> _clientSendQueue = new Queue<byte[]>();
        private readonly Queue<byte[]> _serverSendQueue = new Queue<byte[]>();

        private readonly Rsa _rsa = new Rsa();

        private uint[] _xteaKey;

        private ZStream _zStream = new ZStream();

        private Socket _clientSocket;
        private Socket _serverSocket;

        private Thread _clientSendThread;
        private Thread _serverSendThread;

        private TcpListener _tcpListener;

        private dynamic _loginData;

        private string _loginWebService;

        private uint _clientSequenceNumber = 1;
        private uint _serverSequenceNumber = 1;

        private bool _isPacketParsingEnabled;
        private bool _isSendingToClient = false;
        private bool _isSendingToServer = false;
        private bool _isStarted;
        private bool _recompressPackets;

        public delegate void ReceivedMessageEventHandler(byte[] data);

        public event ReceivedMessageEventHandler OnReceivedClientMessage;
        public event ReceivedMessageEventHandler OnReceivedServerMessage;

        public ConnectionState ConnectionState { get; set; } = ConnectionState.Disconnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class that acts as a proxy
        /// between the Tibia client and the game server.
        /// </summary>
        public Connection(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _clientInMessage = new NetworkMessage(_client);
            _clientOutMessage = new NetworkMessage(_client);
            _serverInMessage = new NetworkMessage(_client);
            _serverOutMessage = new NetworkMessage(_client);

            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0");
        }

        /// <summary>
        /// Starts the <see cref="HttpListener"/> and <see cref="TcpListener"/> objects that listen for incoming
        /// connection requests from the Tibia client.
        /// </summary>
        /// <returns>Returns true on success, or if already started. Returns false if an exception is thrown.</returns>
        internal bool Start(bool enablePacketParsing = true, int httpPort = 80, string loginWebService = "", bool recompressPackets = false)
        {
            if (_isStarted)
            {
                return true;
            }

            try
            {
                if (_tcpListener == null)
                {
                    _tcpListener = new TcpListener(IPAddress.Loopback, 0);
                }

                var uriPrefix = $"http://127.0.0.1:{httpPort}/";
                if (!_httpListener.Prefixes.Contains(uriPrefix))
                {
                    _httpListener.Prefixes.Add(uriPrefix);
                }

                _zStream.deflateInit(zlibConst.Z_DEFAULT_COMPRESSION, -15);
                _zStream.inflateInit(-15);

                _httpListener.Start();
                _httpListener.BeginGetContext(new AsyncCallback(BeginGetContextCallback), _httpListener);

                _tcpListener.Start();
                _tcpListener.BeginAcceptSocket(new AsyncCallback(BeginAcceptTcpClientCallback), _tcpListener);

                _isStarted = true;
                _loginWebService = loginWebService;
                _isPacketParsingEnabled = enablePacketParsing;
                _recompressPackets = recompressPackets;
                ConnectionState = ConnectionState.ConnectingStage1;
            }
            catch (Exception ex)
            {
                _isStarted = false;
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }

            return _isStarted;
        }

        /// <summary>
        /// Sends a packet to the Tibia client.
        /// </summary>
        /// <param name="message">
        /// The <see cref="ServerPacket"/> object to be sent.
        /// </param>
        public void SendToClient(ServerPacket packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            var message = new NetworkMessage(_client);
            packet.AppendToNetworkMessage(message);
            SendToClient(message);
        }

        /// <summary>
        /// Sends a packet to the Tibia client.
        /// </summary>
        /// <param name="message">
        /// The <see cref="NetworkMessage"/> object containing the data to be sent.
        /// </param>
        public void SendToClient(NetworkMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (message.Size <= 8)
            {
                return;
            }

            if (message.SequenceNumber != 0)
            {
                lock (_clientSequenceNumberLock)
                {
                    message.SequenceNumber = _clientSequenceNumber++;
                }
            }

            message.PrepareToSend(_xteaKey, (_recompressPackets ? _zStream : null));
            SendToClient(message.GetData());
        }

        /// <summary>
        /// Sends a packet to the Tibia client.
        /// </summary>
        /// <param name="data">
        /// The raw byte-array containing the data to be sent.
        /// </param>
        public void SendToClient(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length <= 8)
            {
                return;
            }

            lock (_clientSendLock)
            {
                _clientSendQueue.Enqueue(data);

                if (!_isSendingToClient)
                {
                    try
                    {
                        _isSendingToClient = true;
                        _clientSendThread = new Thread(new ThreadStart(ClientSend));
                        _clientSendThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        // TODO: Log exception.
                    }
                }
            }
        }

        /// <summary>
        /// Sends a packet to the game server.
        /// </summary>
        /// <param name="packet">
        /// The <see cref="ClientPacket"/> to be sent.
        /// </param>
        public void SendToServer(ClientPacket packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            var message = new NetworkMessage(_client);
            packet.AppendToNetworkMessage(message);
            SendToServer(message);
        }

        /// <summary>
        /// Sends a packet to the game server.
        /// </summary>
        /// <param name="message">
        /// The <see cref="NetworkMessage"/> object containing the data to be sent.
        /// </param>
        public void SendToServer(NetworkMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (message.Size <= 8)
            {
                return;
            }

            if (message.SequenceNumber != 0)
            {
                lock (_ServerSequenceNumberLock)
                {
                    message.SequenceNumber = _serverSequenceNumber++;
                }
            }

            message.PrepareToSend(_xteaKey);
            SendToServer(message.GetData());
        }

        /// <summary>
        /// Sends a packet to the game server.
        /// </summary>
        /// <param name="data">
        /// Raw byte-array containing the data to be sent.
        /// </param>
        public void SendToServer(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length <= 8)
            {
                return;
            }

            lock (_serverSendLock)
            {
                _serverSendQueue.Enqueue(data);

                if (!_isSendingToServer)
                {
                    try
                    {
                        _isSendingToServer = true;
                        _serverSendThread = new Thread(new ThreadStart(ServerSend));
                        _serverSendThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        // TODO: Log exception.
                    }
                }
            }
        }

        /// <summary>
        /// Sets the XTEA key used for encrypting/decrypting both server and client packets.
        /// </summary>
        /// <param name="key">List of unsigned integers to be used as the XTEA key.</param>
        /// <returns>Returns false if the length of the list is anything other than 4.</returns>
        internal bool SetXteaKey(List<uint> key)
        {
            if (key.Count != 4)
            {
                return false;
            }

            _xteaKey = key.ToArray();
            return true;
        }

        /// <summary>
        /// Closes any open connections between the Tibia client and game server, and stops listening for any
        /// new incoming HTTP or TCP connection requests.
        /// </summary>
        internal void Stop()
        {
            if (!_isStarted)
            {
                return;
            }

            try
            {
                _zStream.deflateEnd();
                _zStream.inflateEnd();
                _httpListener.Close();

                if (_tcpListener != null)
                {
                    _tcpListener.Stop();
                }

                if (_clientSocket != null)
                {
                    _clientSocket.Close();
                }

                if (_serverSocket != null)
                {
                    _serverSocket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }

            _isStarted = false;
            _xteaKey = null;
            ConnectionState = ConnectionState.Disconnected;
        }

        /// <summary>
        /// Closes any open connections between the Tibia client and game server, and clears
        /// any pending packets to be sent.
        /// </summary>
        private void ResetConnection()
        {
            lock (_clientSendLock)
            {
                _isSendingToClient = false;
                _clientSendQueue.Clear();
            }
            if (_clientSocket != null)
            {
                _clientSocket.Close();
            }

            lock (_serverSendLock)
            {
                _isSendingToServer = false;
                _serverSendQueue.Clear();
            }
            if (_serverSocket != null)
            {
                _serverSocket.Close();
            }

            _zStream.deflateEnd();
            _zStream.inflateEnd();
            _zStream = new ZStream();
            _zStream.deflateInit(zlibConst.Z_DEFAULT_COMPRESSION, -15);
            _zStream.inflateInit(-15);

            _clientSequenceNumber = 1;
            _serverSequenceNumber = 1;
            _xteaKey = null;
            ConnectionState = ConnectionState.ConnectingStage1;
        }

        /// <summary>
        /// Grabs the next packet in the queue and sends it ansychronously to the Tibia client.
        /// </summary>
        private void ClientSend()
        {
            if (_clientSocket == null)
            {
                return;
            }

            try
            {
                byte[] data = null;

                lock (_clientSendLock)
                {
                    if (_clientSendQueue.Count > 0)
                    {
                        data = _clientSendQueue.Dequeue();
                    }

                    if (data == null)
                    {
                        _isSendingToClient = false;
                        return;
                    }
                }

                _clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(BeginSendClientCallback), _clientSocket);
            }
            catch (SocketException)
            {
                // This exception can happen if the client, forcefully, closes the connection (e.g., killing the client process).
                ResetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Grabs the next packet in the queue and sends it ansychronously to the game server.
        /// </summary>
        private void ServerSend()
        {
            if (_serverSocket == null)
            {
                return;
            }

            try
            {
                byte[] data = null;

                lock (_serverSendLock)
                {
                    if (_serverSendQueue.Count > 0)
                    {
                        data = _serverSendQueue.Dequeue();
                    }

                    if (data == null)
                    {
                        _isSendingToServer = false;
                        return;
                    }
                }

                _serverSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(BeginSendServerCallback), _serverSocket);
            }
            catch (SocketException)
            {
                // This exception can happen if the server, forcefully, closes the connection.
                ResetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Handles a pending ansynchronous packet send to the Tibia client, and calls <see cref="ClientSend"/> to send the next one.
        /// </summary>
        /// <param name="ar">
        /// An <see cref="IAsyncResult"/> object that indicates the status of the asynchronous operation.
        /// </param>
        private void BeginSendClientCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            if (socket == null)
            {
                throw new Exception("[Connection.BeginSendClientCallback] Client socket is null.");
            }

            try
            {
                socket.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }

            ClientSend();
        }

        /// <summary>
        /// Handles a pending ansynchronous packet send to the game server, and calls <see cref="ServerSend"/> to send the next one.
        /// </summary>
        /// <param name="ar">
        /// An <see cref="IAsyncResult"/> object that indicates the status of the asynchronous operation.
        /// </param>
        private void BeginSendServerCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            if (socket == null)
            {
                throw new Exception("[Connection.BeginSendServerCallback] Server socket is null.");
            }

            try
            {
                socket.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }

            ServerSend();
        }

        /// <summary>
        /// Handles an incoming HTTP request, forwards it to CipSoft's web service, waits for a response,
        /// modifies the response if it's the character list, then forwards it to the Tibia client.
        /// </summary>
        /// <param name="ar">
        /// An <see cref="IAsyncResult"/> object that indicates the status of the asynchronous operation.
        /// </param>
        private void BeginGetContextCallback(IAsyncResult ar)
        {
            try
            {
                var httpListener = (HttpListener)ar.AsyncState;
                if (httpListener == null)
                {
                    throw new Exception("[Connection.BeginGetContextCallback] HTTP listener is null.");
                }

                var context = httpListener.EndGetContext(ar);
                var request = context.Request;
                var clientRequest = string.Empty;

                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    clientRequest = reader.ReadToEnd();
                }

                if (string.IsNullOrEmpty(clientRequest))
                {
                    throw new Exception($"[Connection.BeginGetContextCallback] Invalid HTTP request data: {clientRequest ?? "null"}");
                }

                var response = PostAsync(clientRequest).Result;
                if (string.IsNullOrEmpty(response))
                {
                    // This can happen with Open-Tibia servers where their login service doesn't handle all
                    // types of requests from the client. Best to keep listening for a proper response.
                    _httpListener.BeginGetContext(new AsyncCallback(BeginGetContextCallback), _httpListener);
                    return;
                }

                // Login data is the only thing we have to modify, everything else can be piped through.
                dynamic loginData = JsonConvert.DeserializeObject(response);
                if (loginData != null && loginData.session != null)
                {
                    // Change the address and port of each game world to that of the TCP listener so that
                    // the Tibia client connects to the TCP listener instead of a game world.
                    var address = ((IPEndPoint)_tcpListener.LocalEndpoint).Address.ToString();
                    var port = ((IPEndPoint)_tcpListener.LocalEndpoint).Port;
                    foreach (var world in loginData.playdata.worlds)
                    {
                        world.externaladdressprotected = address;
                        world.externaladdressunprotected = address;
                        world.externalportprotected = port;
                        world.externalportunprotected = port;
                    }

                    // Store the original login data so when the Tibia client tries to connect to a game world
                    // the server socket can recall the address and port to connect to.
                    _loginData = JsonConvert.DeserializeObject(response);
                    response = JsonConvert.SerializeObject(loginData);
                }

                var data = Encoding.UTF8.GetBytes(response);
                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
                context.Response.Close();

                _httpListener.BeginGetContext(new AsyncCallback(BeginGetContextCallback), _httpListener);
            }
            catch (ObjectDisposedException)
            {
                // This exception can occur if Stop() is called.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Handles an incoming TCP connection and begins receiving incoming data.
        /// </summary>
        /// <param name="ar">
        /// An <see cref="IAsyncResult"/> object that indicates the status of the asynchronous operation.
        /// </param>
        private void BeginAcceptTcpClientCallback(IAsyncResult ar)
        {
            try
            {
                var tcpListener = (TcpListener)ar.AsyncState;
                if (tcpListener == null)
                {
                    throw new Exception("[Connection.BeginAcceptTcpClientCallback] TCP client is null.");
                }

                _clientSocket = tcpListener.EndAcceptSocket(ar);
                _clientSocket.LingerState = new LingerOption(true, 2);
                _clientSocket.BeginReceive(_clientInMessage.GetBuffer(), 0, 1, SocketFlags.None, new AsyncCallback(BeginReceiveWorldNameCallback), null);

                _tcpListener.BeginAcceptSocket(new AsyncCallback(BeginAcceptTcpClientCallback), _tcpListener);
            }
            catch (ObjectDisposedException)
            {
                // This exception can occur if Stop() is called.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Handles the first packet the Tibia client sends to the game server; which is just the world name.
        /// This is the only packet that doesn't conform to the normal packet structure that Tibia uses.
        /// </summary>
        /// <param name="ar">
        /// An <see cref="IAsyncResult"/> object that indicates the status of the asynchronous operation.
        /// </param>
        private void BeginReceiveWorldNameCallback(IAsyncResult ar)
        {
            try
            {
                if (_clientSocket == null)
                {
                    return;
                }

                var count = _clientSocket.EndReceive(ar);
                if (count <= 0)
                {
                    ResetConnection();
                    return;
                }

                // The first message the client sends to the game server is the world name without a length.
                // Read from the socket one byte at a time until the end of the string (\n) is read.
                while (_clientInMessage.GetBuffer()[count - 1] != Convert.ToByte('\n'))
                {
                    var read = _clientSocket.Receive(_clientInMessage.GetBuffer(), count, 1, SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("[Connection.BeginReceiveWorldNameCallback] Client connection broken.");
                    }

                    count += read;
                }

                var worldName = Encoding.UTF8.GetString(_clientInMessage.GetBuffer(), 0, count - 1);
                foreach (var world in _loginData.playdata.worlds)
                {
                    var name = (string)world.name;
                    if (name.Equals(worldName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        _clientSocket.BeginReceive(_clientInMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveClientCallback), 0);

                        _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _serverSocket.Connect((string)world.externaladdressprotected, (int)world.externalportprotected);
                        _serverSocket.Send(_clientInMessage.GetBuffer(), 0, count, SocketFlags.None);
                        _serverSocket.BeginReceive(_serverInMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveServerCallback), 0);
                        return;
                    }
                }

                throw new Exception($"[Connection.BeginReceiveWorldNameCallback] Login data not found for world: {worldName}.");
            }
            catch (SocketException)
            {
                // This exception can happen if the client, forcefully, closes the connection (e.g., killing the client process).
                ResetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Handles all incoming data from the Tibia client (except the first packet).
        /// </summary>
        /// <param name="ar">
        /// An <see cref="IAsyncResult"/> object that indicates the status of the asynchronous operation.
        /// </param>
        private void BeginReceiveClientCallback(IAsyncResult ar)
        {
            try
            {
                if (_clientSocket == null)
                {
                    return;
                }

                var count = _clientSocket.EndReceive(ar);
                if (count <= 0)
                {
                    ResetConnection();
                    return;
                }

                _clientInMessage.Size = (uint)BitConverter.ToUInt16(_clientInMessage.GetBuffer(), 0) + 2;
                while (count < _clientInMessage.Size)
                {
                    var read = _clientSocket.Receive(_clientInMessage.GetBuffer(), count, (int)(_clientInMessage.Size - count), SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("[Connection.BeginReceiveClientCallback] Client connection broken.");
                    }

                    count += read;
                }

                var protocol = (int)ar.AsyncState;
                if (protocol == 0)
                {
                    _rsa.OpenTibiaDecrypt(_clientInMessage, 18);
                    _clientInMessage.Seek(18, SeekOrigin.Begin);
                    if (_clientInMessage.ReadByte() != 0)
                    {
                        throw new Exception("[Connection.BeginReceiveClientCallback] RSA decryption failed.");
                    }

                    OnReceivedClientMessage?.Invoke(_clientInMessage.GetData());

                    if (_isPacketParsingEnabled)
                    {
                        _clientOutMessage.Reset();
                        ParseClientMessage(_client, _clientInMessage, _clientOutMessage);
                    }
                    else
                    {
                        _xteaKey = new uint[4];
                        for (var i = 0; i < 4; ++i)
                        {
                            _xteaKey[i] = _clientInMessage.ReadUInt32();
                        }
                    }

                    if (string.IsNullOrEmpty(_loginWebService))
                    {
                        _rsa.TibiaEncrypt(_clientInMessage, 18);
                    }
                    else
                    {
                        // If the user supplied a login web service address,
                        // it's safe to assume it's an Open-Tibia server.
                        _rsa.OpenTibiaEncrypt(_clientInMessage, 18);
                    }

                    SendToServer(_clientInMessage.GetData());
                }
                else
                {
                    _clientInMessage.PrepareToParse(_xteaKey);
                    OnReceivedClientMessage?.Invoke(_clientInMessage.GetData());

                    if (_isPacketParsingEnabled)
                    {
                        _clientOutMessage.Reset();
                        _clientOutMessage.SequenceNumber = _clientInMessage.SequenceNumber;

                        ParseClientMessage(_client, _clientInMessage, _clientOutMessage);
                        SendToServer(_clientOutMessage);
                    }
                    else
                    {
                        _clientInMessage.PrepareToSend(_xteaKey);
                        SendToServer(_clientInMessage.GetData());
                    }
                }

                _clientSocket.BeginReceive(_clientInMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveClientCallback), 1);
            }
            catch (ObjectDisposedException)
            {
                // This exception can occur when the player logs out of their character (e.g., Ctrl+L).
                ResetConnection();
            }
            catch (SocketException)
            {
                // This exception can happen if the client, forcefully, closes the connection (e.g., killing the client process).
                ResetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Handles all incoming data from the game server.
        /// </summary>
        /// <param name="ar">
        /// An <see cref="IAsyncResult"/> object that indicates the status of the asynchronous operation.
        /// </param>
        private void BeginReceiveServerCallback(IAsyncResult ar)
        {
            try
            {
                if (_serverSocket == null)
                {
                    return;
                }

                var count = _serverSocket.EndReceive(ar);
                if (count <= 0)
                {
                    ResetConnection();
                    return;
                }

                _serverInMessage.Size = (uint)BitConverter.ToUInt16(_serverInMessage.GetBuffer(), 0) + 2;
                while (count < _serverInMessage.Size)
                {
                    var read = _serverSocket.Receive(_serverInMessage.GetBuffer(), count, (int)(_serverInMessage.Size - count), SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("[Connection.BeginReceiveServerCallback] Server connection broken.");
                    }

                    count += read;
                }

                _serverInMessage.PrepareToParse(_xteaKey, _zStream);
                OnReceivedServerMessage?.Invoke(_serverInMessage.GetData());

                if (_isPacketParsingEnabled)
                {
                    _serverOutMessage.Reset();
                    _serverOutMessage.SequenceNumber = _serverInMessage.SequenceNumber;

                    ParseServerMessage(_client, _serverInMessage, _serverOutMessage);
                    // TODO: Until AppendToNetworkMessage() is complete for all server
                    // packets, the original packet must be forwarded.
                    SendToClient(_serverInMessage);
                }
                else
                {
                    _serverInMessage.PrepareToSend(_xteaKey, (_recompressPackets ? _zStream : null));
                    SendToClient(_serverInMessage.GetData());
                }

                _serverSocket.BeginReceive(_serverInMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveServerCallback), 1);
            }
            catch (ObjectDisposedException)
            {
                // This exception can occur if Stop() is called.
            }
            catch (SocketException)
            {
                // This exception can happen if the server, forcefully, closes the connection.
                ResetConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Asynchronously sends an HTTP POST to CipSoft's web service.
        /// </summary>
        /// <param name="content">
        /// The JSON data to POST.
        /// </param>
        /// <returns>
        /// The response from CipSoft's web service.
        /// </returns>
        private async Task<string> PostAsync(string content)
        {
            try
            {
                var postContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient
                    .PostAsync(new Uri(GetLoginWebService()), postContent)
                    .ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
                return string.Empty;
            }
        }

        private string GetLoginWebService()
        {
            if (!string.IsNullOrEmpty(_loginWebService))
            {
                return _loginWebService;
            }

            if (_client.VersionNumber >= 11867253)
            {
                return loginWebService1186;
            }

            if (_client.VersionNumber >= 11325206)
            {
                return loginWebService1132;
            }

            return loginWebService1100;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();

                    if (_clientSocket != null)
                    {
                        _clientSocket.Dispose();
                    }

                    if (_serverSocket != null)
                    {
                        _serverSocket.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        ~Connection()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all the managed resources used by the <see cref="Connection"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

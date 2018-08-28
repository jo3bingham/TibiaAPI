using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OXGaming.TibiaAPI.Network
{
    /// <summary>
    /// The <see cref="Connection"/> class is used to create a proxy between the Tibia client and the game server.
    /// </summary>
    internal class Connection : IDisposable
    {
        private readonly object _clientSendLock = new object();
        private readonly object _serverSendLock = new object();

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly HttpListener _httpListener = new HttpListener();

        private readonly NetworkMessage _clientMessage = new NetworkMessage();
        private readonly NetworkMessage _serverMessage = new NetworkMessage();

        private readonly Queue<byte[]> _clientSendQueue = new Queue<byte[]>();
        private readonly Queue<byte[]> _serverSendQueue = new Queue<byte[]>();

        private readonly Rsa _rsa = new Rsa();

        private LoginData _loginData;

        private Socket _clientSocket;
        private Socket _serverSocket;

        private Thread _clientSendThread;
        private Thread _serverSendThread;

        private TcpListener _tcpListener;

        private Xtea _xtea;

        private bool _isStarted;
        private bool _isSendingToClient = false;
        private bool _isSendingToServer = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class that acts as a proxy
        /// between the Tibia client and the game server.
        /// </summary>
        /// <remarks>
        /// The Tibia client's web service address needs to be changed to http://127.0.0.1/ so
        /// that it will connect to this proxy. The Tibia client connects over port 80, and the proxy
        /// listens on port 80, so there's no need to specify a port. However, since the proxy listens
        /// on port 80, that means it needs to be ran in an elevated environment (i.e., root or administrator).
        /// </remarks>
        public Connection()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Loopback, 0);

                // The HTTP listener must be listening on port 80 as the
                // Tibia client sends HTTP requests over port 80.
                _httpListener.Prefixes.Add("http://127.0.0.1:80/");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Starts the <see cref="HttpListener"/> and <see cref="TcpListener"/> objects that listen for incoming
        /// connection requests from the Tibia client.
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            if (_isStarted)
            {
                return true;
            }

            try
            {
                _httpListener.Start();
                _httpListener.BeginGetContext(new AsyncCallback(BeginGetContextCallback), _httpListener);

                _tcpListener.Start();
                _tcpListener.BeginAcceptSocket(new AsyncCallback(BeginAcceptTcpClientCallback), _tcpListener);

                _isStarted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }

            return _isStarted;
        }

        /// <summary>
        /// Closes any open connections between the Tibia client and game server, and stops listening for any
        /// new incoming HTTP or TCP connection requests.
        /// </summary>
        public void Stop()
        {
            if (!_isStarted)
            {
                return;
            }

            try
            {
                _httpListener.Close();
                _tcpListener.Stop();
                _clientSocket.Close();
                _serverSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO: Log exception.
            }

            _xtea = null;
            _isStarted = false;
        }

        /// <summary>
        /// Sends a packet to the Tibia client.
        /// </summary>
        /// <param name="message">
        /// The <see cref="NetworkMessage"/> object containing the packet data to be sent.
        /// </param>
        /// <remarks>
        /// There are no sanity checks done on <paramref name="message"/>, so a malformed,
        /// or invalid, packet will cause the connection to be terminated.
        /// </remarks>
        public void SendToClient(NetworkMessage message)
        {
            lock (_clientSendLock)
            {
                _clientSendQueue.Enqueue(message.Data);

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
        /// <param name="message">
        /// The <see cref="NetworkMessage"/> object containing the packet data to be sent.
        /// </param>
        /// <remarks>
        /// There are no sanity checks done on <paramref name="message"/>, so a malformed,
        /// or invalid, packet will cause the connection to be terminated.
        /// </remarks>
        public void SendToServer(NetworkMessage message)
        {
            lock (_serverSendLock)
            {
                _serverSendQueue.Enqueue(message.Data);

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
        }

        /// <summary>
        /// Grabs the next packet in the queue and sends it ansychronously to the Tibia client.
        /// </summary>
        private void ClientSend()
        {
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
                    throw new Exception("[Connection.BeginGetContextCallback]");
                }

                // Login data is the only thing we have to modify, everything else can be piped through.
                var loginData = JsonConvert.DeserializeObject<LoginData>(response);
                if (loginData != null && loginData.Session != null)
                {
                    // Change the address and port of each game world to that of the TCP listener so that
                    // the Tibia client connects to the TCP listener instead of a game world.
                    var address = ((IPEndPoint)_tcpListener.LocalEndpoint).Address.ToString();
                    var port = ((IPEndPoint)_tcpListener.LocalEndpoint).Port;
                    foreach (var world in loginData.PlayData.Worlds)
                    {
                        world.ExternalAddressProtected = address;
                        world.ExternalAddressUnprotected = address;
                        world.ExternalPortProtected = port;
                        world.ExternalPortUnprotected = port;
                    }

                    // Store the original login data so when the Tibia client tries to connect to a game world
                    // the server socket can recall the address and port to connect to.
                    _loginData = JsonConvert.DeserializeObject<LoginData>(response);
                    response = JsonConvert.SerializeObject(loginData);
                }

                var data = Encoding.UTF8.GetBytes(response);
                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
                context.Response.Close();

                _httpListener.BeginGetContext(new AsyncCallback(BeginGetContextCallback), _httpListener);
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
                _clientSocket.BeginReceive(_clientMessage.GetBuffer(), 0, 1, SocketFlags.None, new AsyncCallback(BeginReceiveWorldNameCallback), null);

                _tcpListener.BeginAcceptSocket(new AsyncCallback(BeginAcceptTcpClientCallback), _tcpListener);
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
                while (_clientMessage.GetBuffer()[count - 1] != Convert.ToByte('\n'))
                {
                    var read = _clientSocket.Receive(_clientMessage.GetBuffer(), count, 1, SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("[Connection.BeginReceiveWorldNameCallback] Client connection broken.");
                    }

                    count += read;
                }

                var worldName = Encoding.UTF8.GetString(_clientMessage.GetBuffer(), 0, count - 1);
                var world = _loginData.PlayData.Worlds.Find(w => w.Name.Equals(worldName, StringComparison.CurrentCultureIgnoreCase));
                if (world == null)
                {
                    throw new Exception($"[Connection.BeginReceiveWorldNameCallback] Login data not found for world: {worldName}.");
                }

                _clientSocket.BeginReceive(_clientMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveClientCallback), 0);

                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Connect(world.ExternalAddressProtected, world.ExternalPortProtected);
                _serverSocket.Send(_clientMessage.GetBuffer(), 0, count, SocketFlags.None);
                _serverSocket.BeginReceive(_serverMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveServerCallback), 0);
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

                _clientMessage.Size = (uint)BitConverter.ToUInt16(_clientMessage.GetBuffer(), 0) + 2;
                while (count < _clientMessage.Size)
                {
                    var read = _clientSocket.Receive(_clientMessage.GetBuffer(), count, (int)(_clientMessage.Size - count), SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("[Connection.BeginReceiveClientCallback] Client connection broken.");
                    }

                    count += read;
                }

                var protocol = (int)ar.AsyncState;
                if (protocol == 0)
                {
                    _rsa.OpenTibiaDecrypt(_clientMessage, 18);
                    _clientMessage.Seek(18, SeekOrigin.Begin);
                    if (_clientMessage.ReadByte() != 0)
                    {
                        throw new Exception("[Connection.BeginReceiveClientCallback] RSA decryption failed.");
                    }

                    var xteaKey = new uint[4];
                    for (var i = 0; i < xteaKey.Length; ++i)
                    {
                        xteaKey[i] = _clientMessage.ReadUInt32();
                    }
                    _xtea = new Xtea(xteaKey);

                    _rsa.TibiaEncrypt(_clientMessage, 18);
                }
                else if (_xtea != null)
                {
                    if (!_xtea.Decrypt(_clientMessage))
                    {
                        Console.WriteLine("BeginReceiveClientCallback: Failed to decrypt!");
                    }
                    if (!_xtea.Encrypt(_clientMessage))
                    {
                        Console.WriteLine("BeginReceiveClientCallback: Failed to encrypt!");
                    }
                }

                SendToServer(_clientMessage);
                _clientSocket.BeginReceive(_clientMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveClientCallback), 1);
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

                _serverMessage.Size = (uint)BitConverter.ToUInt16(_serverMessage.GetBuffer(), 0) + 2;
                while (count < _serverMessage.Size)
                {
                    var read = _serverSocket.Receive(_serverMessage.GetBuffer(), count, (int)(_serverMessage.Size - count), SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("[Connection.BeginReceiveServerCallback] Server connection broken.");
                    }

                    count += read;
                }

                var protocol = (int)ar.AsyncState;
                if (protocol != 0)
                {
                    if (_xtea != null)
                    {
                        if (!_xtea.Decrypt(_serverMessage))
                        {
                            Console.WriteLine("BeginReceiveServerCallback: Failed to decrypt!");
                        }
                        if (!_xtea.Encrypt(_serverMessage))
                        {
                            Console.WriteLine("BeginReceiveServerCallback: Failed to encrypt!");
                        }
                    }
                }

                SendToClient(_serverMessage);
                _serverSocket.BeginReceive(_serverMessage.GetBuffer(), 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveServerCallback), 1);
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
                    .PostAsync(new Uri("https://secure.tibia.com/services/clientservices.php"), postContent)
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

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                    _clientSocket.Dispose();
                    _serverSocket.Dispose();
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

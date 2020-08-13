using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Network;
using OXGaming.TibiaAPI.Network.ServerPackets;
using OXGaming.TibiaAPI.Utilities;

namespace Watch
{
    class Program
    {
        const string OxWorldName = "OXGaming Recording";

        static readonly Client _client = new Client();

        static uint[] _xteaKey;

        static byte[] _clientBuffer;

        static HttpListener _httpListener;

        static Socket _clientSocket;

        static TcpListener _tcpListener;

        static Thread _clientSendThread;

        private static Logger.LogLevel _logLevel = Logger.LogLevel.Error;

        private static Logger.LogOutput _logOutput = Logger.LogOutput.Console;

        static string _recordingName;

        static int _httpPort = 7171;

        static bool _userQuit;

        static void ParseArgs(string[] args)
        {
            foreach (var arg in args)
            {
                if (!arg.Contains('=', StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                var splitArg = arg.Split('=');
                if (splitArg.Length != 2)
                {
                    continue;
                }

                switch (splitArg[0])
                {
                    case "-r":
                    case "--recording":
                        {
                            _recordingName = splitArg[1].Replace("\"", "");
                        }
                        break;
                    case "-p":
                    case "--port":
                        {
                            if (int.TryParse(splitArg[1], out var port))
                            {
                                _httpPort = port;
                            }
                        }
                        break;
                    case "--loglevel":
                        {
                            _logLevel = Logger.ConvertToLogLevel(splitArg[1]);
                        }
                        break;
                    case "--logoutput":
                        {
                            _logOutput = Logger.ConvertToLogOutput(splitArg[1]);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine($"Invalid number of arguments: {args.Length}");
                return;
            }

            ParseArgs(args);

            if (string.IsNullOrEmpty(_recordingName) || !_recordingName.EndsWith(".oxr", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine($"Invalid recording file: {_recordingName ?? "null"}");
                return;
            }

            Initialize();

            while (Console.ReadLine() != "quit") { }

            _userQuit |= _clientSendThread != null;
        }

        static void Initialize()
        {
            try
            {
                _client.Logger.Level = _logLevel;
                _client.Logger.Output = _logOutput;

                _clientBuffer = new byte[NetworkMessage.MaxMessageSize];

                if (_tcpListener == null)
                {
                    _tcpListener = new TcpListener(IPAddress.Loopback, 0);
                }

                _httpListener = new HttpListener();
                var uriPrefix = $"http://127.0.0.1:{_httpPort}/";
                if (!_httpListener.Prefixes.Contains(uriPrefix))
                {
                    // The HTTP listener must be listening on port 80 as the
                    // Tibia client sends HTTP requests over port 80.
                    _httpListener.Prefixes.Add(uriPrefix);
                }

                _httpListener.Start();
                _httpListener.BeginGetContext(new AsyncCallback(BeginGetContextCallback), _httpListener);

                _tcpListener.Start();
                _tcpListener.BeginAcceptSocket(new AsyncCallback(BeginAcceptTcpClientCallback), _tcpListener);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void Reset()
        {
            if (_clientSocket != null)
            {
                _clientSocket.Close();
            }

            _userQuit |= _clientSendThread != null || _clientSendThread.IsAlive;
            _xteaKey = null;
        }

        static void BeginGetContextCallback(IAsyncResult ar)
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

                var filename = Path.GetFileNameWithoutExtension(_recordingName);
                var address = ((IPEndPoint)_tcpListener.LocalEndpoint).Address.ToString();
                var port = ((IPEndPoint)_tcpListener.LocalEndpoint).Port;
                var response = "{\"session\":" +
                                    "{\"sessionkey\": null," +
                                    "\"lastlogintime\": 0," +
                                    "\"ispremium\": false," +
                                    "\"premiumuntil\": 0," +
                                    "\"status\": null," +
                                    "\"returnernotification\": false," +
                                    "\"showrewardnews\": false," +
                                    "\"isreturner\": false," +
                                    "\"fpstracking\": false," +
                                    "\"optiontracking\": false," +
                                    "\"tournamentticketpurchasestate\": 0," +
                                    "\"tournamentcyclephase\": 0}" +
                                ",\"playdata\":" +
                                    "{\"worlds\":" +
                                        "[{\"id\": 0," +
                                        "\"name\": \"" + OxWorldName + "\"," +
                                        "\"externaladdressprotected\": \"" + address + "\"," +
                                        "\"externalportprotected\": " + port + "," +
                                        "\"externaladdressunprotected\": \"" + address + "\"," +
                                        "\"externalportunprotected\": " + port + "," +
                                        "\"previewstate\": 0," +
                                        "\"location\": null," +
                                        "\"anticheatprotection\": false," +
                                        "\"pvptype\": 0," +
                                        "\"istournamentworld\": false," +
                                        "\"restrictedstore\": false}]" +
                                    ",\"characters\":" +
                                        "[{\"worldid\": 0," +
                                        "\"name\": \"" + filename + "\"," +
                                        "\"level\": 0," +
                                        "\"vocation\": null," +
                                        "\"ismale\": false," +
                                        "\"ishidden\": false," +
                                        "\"ismaincharacter\": false," +
                                        "\"tutorial\": false," +
                                        "\"outfitid\": 0," +
                                        "\"headcolor\": 0," +
                                        "\"torsocolor\": 0," +
                                        "\"legscolor\": 0," +
                                        "\"detailcolor\": 0," +
                                        "\"addonsflags\": 0," +
                                        "\"istournamentparticipant\": false}]" +
                                    "}" +
                                "}";

                var data = Encoding.UTF8.GetBytes(response);
                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
                context.Response.Close();

                _httpListener.BeginGetContext(new AsyncCallback(BeginGetContextCallback), _httpListener);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void BeginAcceptTcpClientCallback(IAsyncResult ar)
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
                _clientSocket.BeginReceive(_clientBuffer, 0, 1, SocketFlags.None, new AsyncCallback(BeginReceiveWorldNameCallback), null);

                _tcpListener.BeginAcceptSocket(new AsyncCallback(BeginAcceptTcpClientCallback), _tcpListener);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void BeginReceiveWorldNameCallback(IAsyncResult ar)
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
                    return;
                }

                // The first message the client sends to the game server is the world name without a length.
                // Read from the socket one byte at a time until the end of the string (\n) is read.
                while (_clientBuffer[count - 1] != Convert.ToByte('\n'))
                {
                    var read = _clientSocket.Receive(_clientBuffer, count, 1, SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("Client connection broken.");
                    }

                    count += read;
                }

                // Confirm that the client is trying to connect to us by checking the world name.
                var worldName = Encoding.UTF8.GetString(_clientBuffer, 0, count - 1);
                if (!worldName.Equals(OxWorldName, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Exception($"World name does not match `{OxWorldName}`: {worldName}");
                }

                var loginChallengeMessage = new NetworkMessage(_client)
                {
                    SequenceNumber = 0
                };

                var loginChallengePacket = new LoginChallenge(_client)
                {
                    Timestamp = (uint)DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds,
                    Random = 0xFF
                };
                loginChallengePacket.AppendToNetworkMessage(loginChallengeMessage);

                SendToClient(loginChallengeMessage);

                _clientSocket.BeginReceive(_clientBuffer, 0, 2, SocketFlags.None, new AsyncCallback(BeginReceiveClientCallback), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void BeginReceiveClientCallback(IAsyncResult ar)
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
                    return;
                }

                var size = (uint)BitConverter.ToUInt16(_clientBuffer, 0) + 2;
                while (count < size)
                {
                    var read = _clientSocket.Receive(_clientBuffer, count, (int)(size - count), SocketFlags.None);
                    if (read <= 0)
                    {
                        throw new Exception("Client connection broken.");
                    }

                    count += read;
                }

                var message = new NetworkMessage(_client);
                message.Seek(0, SeekOrigin.Begin);
                message.Write(_clientBuffer);

                var rsaStartIndex = _client.VersionNumber >= 124010030 ? 31 : 18;
                var rsa = new Rsa();
                rsa.OpenTibiaDecrypt(message, rsaStartIndex);

                message.Seek(rsaStartIndex, SeekOrigin.Begin);
                if (message.ReadByte() != 0)
                {
                    throw new Exception("RSA decryption failed.");
                }

                _xteaKey = new uint[4];
                for (var i = 0; i < 4; ++i)
                {
                    _xteaKey[i] = message.ReadUInt32();
                }

                _clientSendThread = new Thread(new ThreadStart(PlayRecording));
                _clientSendThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void PlayRecording()
        {
            try
            {
                using (var reader = new BinaryReader(File.OpenRead(_recordingName)))
                {
                    var lastTimestamp = 0L;
                    var version = reader.ReadString();
                    var sequenceNumber = 0U;
                    while (reader.BaseStream.Position < reader.BaseStream.Length && !_userQuit)
                    {
                        var packetType = (PacketType)reader.ReadByte();
                        var timestamp = reader.ReadInt64();
                        var size = reader.ReadUInt32();
                        var packetSize = reader.ReadUInt16();
                        var _sequenceNumber = reader.ReadUInt32();
                        var data = reader.ReadBytes((int)size - 6);

                        if (packetType == PacketType.Client || data[2] == (byte)ServerPacketType.LoginChallenge)
                        {
                            continue;
                        }

                        var message = new NetworkMessage(_client)
                        {
                            SequenceNumber = sequenceNumber++
                        };
                        message.Write(data, 2, (uint)(data.Length - 2));

                        Thread.Sleep((int)(timestamp - lastTimestamp));
                        lastTimestamp = timestamp;
                        SendToClient(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Reset();
            }
        }

        static void SendToClient(NetworkMessage message)
        {
            if (_clientSocket == null)
            {
                throw new NullReferenceException("Client socket is invalid.");
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (message.Size <= 8)
            {
                return;
            }

            message.PrepareToSend(_xteaKey);
            _clientSocket.Send(message.GetData(), SocketFlags.None);
        }
    }
}

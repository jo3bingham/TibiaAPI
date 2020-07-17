using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace Record
{
    class Message
    {
        public byte[] Data { get; set; }

        public long Timestamp { get; set; }

        public PacketType Type { get; set; }
    }

    class Program
    {
        static readonly ConcurrentQueue<Message> _fileWriteQueue = new ConcurrentQueue<Message>();

        static readonly Stopwatch _stopWatch = new Stopwatch();

        static BinaryWriter _binaryWriter;

        static Client _client;

        static FileStream _fileStream;

        static Thread _fileWriteThread;

        private static Logger.LogLevel _logLevel = Logger.LogLevel.Error;

        private static Logger.LogOutput _logOutput = Logger.LogOutput.Console;

        static string _loginWebService = string.Empty;
        static string _tibiaDirectory = string.Empty;

        static int _httpPort = 7171;

        static bool _isWritingToFile = false;

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
                    case "-t":
                    case "--tibiadirectory":
                        {
                            _tibiaDirectory = splitArg[1].Replace("\"", "");
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
                    case "-l":
                    case "--login":
                        {
                            _loginWebService = splitArg[1];
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
            try
            {
                ParseArgs(args);

                using (_client = new Client(_tibiaDirectory))
                {
                    var utcNow = DateTime.UtcNow;
                    var filename = $"{utcNow.Day}_{utcNow.Month}_{utcNow.Year}__{utcNow.Hour}_{utcNow.Minute}_{utcNow.Second}.oxr";
                    var recordingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recordings");
                    if (!Directory.Exists(recordingDirectory))
                    {
                        Directory.CreateDirectory(recordingDirectory);
                    }

                    Console.CancelKeyPress += Console_CancelKeyPress;

                    _fileStream = new FileStream(Path.Combine(recordingDirectory, filename), FileMode.Append);
                    _binaryWriter = new BinaryWriter(_fileStream);

                    _binaryWriter.Write(_client.Version);

                    _client.Logger.Level = _logLevel;
                    _client.Logger.Output = _logOutput;

                    _client.Connection.OnReceivedClientMessage += Proxy_OnReceivedClientMessage;
                    _client.Connection.OnReceivedServerMessage += Proxy_OnReceivedServerMessage;

                    // Disable packet parsing as we only care about the raw, decrypted packets, and speed.
                    _client.Connection.IsClientPacketParsingEnabled = false;
                    _client.Connection.IsServerPacketParsingEnabled = false;
                    _client.StartConnection(httpPort: _httpPort, loginWebService: _loginWebService);

                    while (Console.ReadLine() != "quit") { }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Shutdown();
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Shutdown();
        }

        private static void Shutdown()
        {
            if (_client != null)
            {
                _client.StopConnection();

                _client.Connection.OnReceivedClientMessage -= Proxy_OnReceivedClientMessage;
                _client.Connection.OnReceivedServerMessage -= Proxy_OnReceivedServerMessage;
            }

            if (_fileWriteThread != null)
            {
                // Block the application from shutting down until the file-write thread
                // finishes writing all incoming packets to disk. This is safe to do as
                // the proxy connection will have been stopped, no matter what, by now.
                _fileWriteThread.Join();
            }

            if (_binaryWriter != null)
            {
                _binaryWriter.Close();
            }

            if (_fileStream != null)
            {
                _fileStream.Close();
            }

            if (_stopWatch.IsRunning)
            {
                _stopWatch.Stop();
            }
        }

        private static void Proxy_OnReceivedClientMessage(byte[] data)
        {
            QueueMessage(PacketType.Client, data);
        }

        private static void Proxy_OnReceivedServerMessage(byte[] data)
        {
            QueueMessage(PacketType.Server, data);
        }

        private static void QueueMessage(PacketType packetType, byte[] data)
        {
            if (!_stopWatch.IsRunning)
            {
                _stopWatch.Start();
            }

            var packetData = new Message
            {
                Data = data,
                Timestamp = _stopWatch.ElapsedMilliseconds,
                Type = packetType
            };

            _fileWriteQueue.Enqueue(packetData);

            if (!_isWritingToFile)
            {
                try
                {
                    _isWritingToFile = true;
                    _fileWriteThread = new Thread(new ThreadStart(WriteData));
                    _fileWriteThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void WriteData()
        {
            try
            {
                while (_fileWriteQueue.TryDequeue(out var packet))
                {
                    _binaryWriter.Write((byte)packet.Type);
                    _binaryWriter.Write(packet.Timestamp);
                    _binaryWriter.Write(packet.Data.Length);
                    _binaryWriter.Write(packet.Data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _isWritingToFile = false;
            }
        }
    }
}

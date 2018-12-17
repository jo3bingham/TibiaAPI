using System;
using System.Diagnostics;
using System.IO;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;

namespace Record
{
    class Program
    {
        static readonly object _writerLock = new object();

        static readonly Stopwatch _stopWatch = new Stopwatch();

        static BinaryWriter _binaryWriter;

        static string _tibiaDirectory = string.Empty;

        static int _httpPort = 80;

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

                using (var client = new Client(_tibiaDirectory))
                {
                    var utcNow = DateTime.UtcNow;
                    var filename = $"{utcNow.Day}_{utcNow.Month}_{utcNow.Year}__{utcNow.Hour}_{utcNow.Minute}_{utcNow.Second}.oxr";
                    var recordingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recordings");
                    if (!Directory.Exists(recordingDirectory))
                    {
                        Directory.CreateDirectory(recordingDirectory);
                    }

                    _binaryWriter = new BinaryWriter(File.OpenWrite(Path.Combine(recordingDirectory, filename)));
                    _binaryWriter.Write(client.Version);

                    client.Proxy.OnReceivedClientMessage += Proxy_OnReceivedClientMessage;
                    client.Proxy.OnReceivedServerMessage += Proxy_OnReceivedServerMessage;

                    // Disable packet parsing as we only care about the raw, decrypted packets and speed.
                    client.StartProxy(enablePacketParsing: false, httpPort: _httpPort);

                    while (Console.ReadLine() != "quit")
                    {
                    }

                    client.StopProxy();

                    client.Proxy.OnReceivedClientMessage -= Proxy_OnReceivedClientMessage;
                    client.Proxy.OnReceivedServerMessage -= Proxy_OnReceivedServerMessage;

                    // Give the proxy time to quit, and any pending packets to be consumed.
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (_binaryWriter != null)
                {
                    _binaryWriter.Close();
                }

                if (_stopWatch.IsRunning)
                {
                    _stopWatch.Stop();
                }
            }
        }

        private static void Proxy_OnReceivedClientMessage(byte[] data)
        {
            WriteMessage(PacketType.Client, data);
        }

        private static void Proxy_OnReceivedServerMessage(byte[] data)
        {
            WriteMessage(PacketType.Server, data);
        }

        private static void WriteMessage(PacketType packetType, byte[] data)
        {
            if (!_stopWatch.IsRunning)
            {
                _stopWatch.Start();
            }

            var timestamp = _stopWatch.ElapsedMilliseconds;

            lock (_writerLock)
            {
                try
                {
                    _binaryWriter.Write((byte)packetType);
                    _binaryWriter.Write(timestamp);
                    _binaryWriter.Write(data.Length);
                    _binaryWriter.Write(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}

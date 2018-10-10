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

        static void Main(string[] args)
        {
            try
            {
                var tibiaDirectory = args.Length > 0 ? args[1] : string.Empty;

                using (var client = new Client(tibiaDirectory))
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
                    client.StartProxy(enablePacketParsing: false);

                    // This first loop could potentially be infinite, but only if the user never connects
                    // to a game server. If that's the case, then killing the application is fine because
                    // there's no useful data in the recording anyway.
                    while (!client.Proxy.IsConnected)
                    {
                    }

                    // Keep the application alive until the user disconnects.
                    while (client.Proxy.IsConnected)
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

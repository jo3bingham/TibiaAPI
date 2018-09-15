using System;
using System.IO;

using OXGaming.TibiaAPI;

namespace Track
{
    class Program
    {
        static BinaryWriter _binaryWriter;

        static void Main(string[] args)
        {
            try
            {
                // Use a default .dat file called Tibia11.dat in the same directory as the Track application,
                // otherwise, the user can supply the path to the .dat file on the command line.
                var tibiaDatFile = "Tibia11.dat";
                if (args.Length == 1)
                {
                    tibiaDatFile = args[0];
                }

                using (var client = new Client(tibiaDatFile))
                {
                    var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                    if (!Directory.Exists(dataDirectory))
                    {
                        Directory.CreateDirectory(dataDirectory);
                    }

                    var currentDate = DateTime.UtcNow;
                    var filename = $"{currentDate.Day}_{currentDate.Month}_{currentDate.Year}__{currentDate.Hour}_{currentDate.Minute}_{currentDate.Second}.dat";
                    _binaryWriter = new BinaryWriter(File.OpenWrite(Path.Combine(dataDirectory, filename)));

                    client.Proxy.OnReceivedServerMessage += Proxy_OnReceivedServerMessage;
                    // Disable packet parsing as we only care about the raw, decrypted packets and speed.
                    client.StartProxy(enablePacketParsing: false);

                    while (Console.ReadLine() != "quit")
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    client.Proxy.OnReceivedServerMessage -= Proxy_OnReceivedServerMessage;
                    client.StopProxy();

                    // Give the proxy time to quit, and any pending packets to be consumed.
                    System.Threading.Thread.Sleep(1000);

                    _binaryWriter.Flush();
                    _binaryWriter.Close();
                    _binaryWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void Proxy_OnReceivedServerMessage(byte[] data)
        {
            try
            {
                _binaryWriter.Write(data.Length);
                _binaryWriter.Write(data);
                _binaryWriter.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;

namespace Record
{
    class Program
    {
        static readonly object _writerLock = new object();

        static readonly Stopwatch _stopWatch = new Stopwatch();

        static BinaryWriter _binaryWriter;

        static string _appearanceDatFile;
        static string _version;

        static void Main(string[] args)
        {
            try
            {
                var tibiaDirectory = args.Length > 0 ? args[1] : string.Empty;
                if (!Initialize(tibiaDirectory))
                {
                    Console.WriteLine("Failed to initialize.");
                    return;
                }

                using (var client = new Client(_appearanceDatFile))
                {
                    var utcNow = DateTime.UtcNow;
                    var filename = $"{utcNow.Day}_{utcNow.Month}_{utcNow.Year}__{utcNow.Hour}_{utcNow.Minute}_{utcNow.Second}.oxr";
                    var recordingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recordings");
                    if (!Directory.Exists(recordingDirectory))
                    {
                        Directory.CreateDirectory(recordingDirectory);
                    }

                    _binaryWriter = new BinaryWriter(File.OpenWrite(Path.Combine(recordingDirectory, filename)));
                    _binaryWriter.Write(_version);

                    client.Proxy.OnReceivedClientMessage += Proxy_OnReceivedClientMessage;
                    client.Proxy.OnReceivedServerMessage += Proxy_OnReceivedServerMessage;

                    // Disable packet parsing as we only care about the raw, decrypted packets and speed.
                    client.StartProxy(enablePacketParsing: false);

                    // This first loop could potentially be infinite, but only if the user never connects
                    // to a game server. If that's the case, then killing the application is fine because
                    // there's no useful data in the recording anyway.
                    while (!client.Proxy.IsConnected)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    // Keep the application alive until the user disconnects.
                    while (client.Proxy.IsConnected)
                    {
                        System.Threading.Thread.Sleep(100);
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

        private static bool Initialize(string tibiaDirectory)
        {
            var platform = OSPlatform.Windows;

            if (string.IsNullOrEmpty(tibiaDirectory))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // TODO
                    platform = OSPlatform.Linux;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // TODO
                    platform = OSPlatform.OSX;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    tibiaDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tibia");
                }

                Console.WriteLine($"Tibia directory was not supplied. Attempting to use default {platform} directory: {tibiaDirectory}");
            }

            if (string.IsNullOrEmpty(tibiaDirectory) || !Directory.Exists(tibiaDirectory))
            {
                Console.WriteLine($"Directory does not exist: {tibiaDirectory}");
                return false;
            }

            var packageJsonFile = string.Empty;

            if (platform == OSPlatform.Linux)
            {
                // TODO
            }
            else if (platform == OSPlatform.OSX)
            {
                // TODO
            }
            else if (platform == OSPlatform.Windows)
            {
                var assetsDirectory = Path.Combine(new string[] { tibiaDirectory, "packages", "Tibia", "assets" });
                if (!Directory.Exists(assetsDirectory))
                {
                    Console.WriteLine($"Assets directory does not exist: {assetsDirectory}");
                    return false;
                }

                var appearanceDatFiles = Directory.GetFiles(assetsDirectory, "*appearances-*.dat");
                if (appearanceDatFiles.Length != 1)
                {
                    Console.WriteLine($"Invalid number of appearances dat files: {appearanceDatFiles.Length}");
                    return false;
                }

                _appearanceDatFile = appearanceDatFiles[0];
                packageJsonFile = Path.Combine(new string[] { tibiaDirectory, "packages", "Tibia", "package.json" });
            }

            if (string.IsNullOrEmpty(_appearanceDatFile) || !File.Exists(_appearanceDatFile))
            {
                Console.WriteLine($"Appearances .dat file does not exist: {_appearanceDatFile}");
                return false;
            }

            if (string.IsNullOrEmpty(packageJsonFile) || !File.Exists(packageJsonFile))
            {
                Console.WriteLine($"Package .json file does not exist: {packageJsonFile}");
                return false;
            }

            var packageJson = string.Empty;
            using (var reader = new StreamReader(packageJsonFile))
            {
                packageJson = reader.ReadToEnd();
                if (string.IsNullOrEmpty(packageJson))
                {
                    Console.WriteLine($"Failed to read package .json file.");
                    return false;
                }
            }

            dynamic packageData = Newtonsoft.Json.JsonConvert.DeserializeObject(packageJson);
            if (packageData == null || packageData.version == null)
            {
                Console.WriteLine("Failed to deserialize package .json file.");
                return false;
            }

            _version = packageData.version;
            if (string.IsNullOrEmpty(_version))
            {
                Console.WriteLine($"Failed to get client version: ");
                return false;
            }

            return true;
        }
    }
}

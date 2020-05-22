using System;
using System.IO;
using System.Runtime.InteropServices;

namespace OXGaming.TibiaAPI
{
    public class Client : IDisposable
    {
        private string _appearanceDatFile;

        public Appearances.AppearanceStorage AppearanceStorage { get; } = new Appearances.AppearanceStorage();

        public Creatures.CreatureStorage CreatureStorage { get; } = new Creatures.CreatureStorage();

        public WorldMap.WorldMapStorage WorldMapStorage { get; } = new WorldMap.WorldMapStorage();

        public Creatures.Creature Player { get; } = new Creatures.Creature(0, Constants.CreatureType.Player);

        public Network.Connection Connection { get; }

        public Utilities.Logger Logger { get; } = new Utilities.Logger();

        public string Version { get; private set; }

        public uint VersionNumber { get; private set; } = 0;

        public Client(string tibiaDirectory = "")
        {
            if (tibiaDirectory == null)
            {
                throw new ArgumentNullException(nameof(tibiaDirectory));
            }

            if (!Initialize(tibiaDirectory))
            {
                throw new Exception("Failed to initialize.");
            }

            using (var datFileStream = File.OpenRead(_appearanceDatFile))
            {
                AppearanceStorage.LoadAppearances(datFileStream);
            }

            Connection = new Network.Connection(this);
        }

        public bool StartConnection(int httpPort = 7171, string loginWebService = "")
        {
            return Connection.Start(httpPort, loginWebService);
        }

        public void StopConnection()
        {
            Connection.Stop();
        }

        private bool Initialize(string tibiaDirectory = "")
        {
            if (string.IsNullOrEmpty(tibiaDirectory))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    tibiaDirectory = Path.Combine(new string[] {
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "CipSoft GmbH", "Tibia", "packages", "Tibia" });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    tibiaDirectory = Path.Combine(new string[] {
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        "Library", "Application Support", "CipSoft GmbH", "Tibia", "packages", "Tibia.app" });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    tibiaDirectory = Path.Combine(new string[] {
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Tibia", "packages", "Tibia" });
                }
            }

            if (string.IsNullOrEmpty(tibiaDirectory) || !Directory.Exists(tibiaDirectory))
            {
                Logger.Error($"Directory does not exist: {tibiaDirectory}");
                return false;
            }

            var packageJsonFile = Path.Combine(tibiaDirectory, "package.json");
            if (!File.Exists(packageJsonFile))
            {
                Logger.Error($"package.json file does not exist: {packageJsonFile}");
                return false;
            }

            var packageJson = string.Empty;
            using (var reader = new StreamReader(packageJsonFile))
            {
                packageJson = reader.ReadToEnd();
                if (string.IsNullOrEmpty(packageJson))
                {
                    Logger.Error($"Failed to read package.json file.");
                    return false;
                }
            }

            dynamic packageData = Newtonsoft.Json.JsonConvert.DeserializeObject(packageJson);
            if (packageData == null || packageData.version == null)
            {
                Logger.Error("Failed to deserialize package.json file.");
                return false;
            }

            Version = packageData.version;
            if (string.IsNullOrEmpty(Version))
            {
                Logger.Error($"Failed to get client version.");
                return false;
            }
            if (uint.TryParse(Version.Replace(".", ""), out var versionNumber))
            {
                VersionNumber = versionNumber;
            }
            else
            {
                Logger.Warning($"Failed to convert the client version to a numerical value: {Version}");
            }

            var assetsDirectory = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                assetsDirectory = Path.Combine(new string[] { tibiaDirectory, "Contents", "Resources", "assets" });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                assetsDirectory = Path.Combine(tibiaDirectory, "assets");
            }

            if (!Directory.Exists(assetsDirectory))
            {
                Logger.Error($"Assets directory does not exist: {assetsDirectory}");
                return false;
            }

            var appearanceDatFiles = Directory.GetFiles(assetsDirectory, "*appearances-*.dat");
            if (appearanceDatFiles.Length != 1)
            {
                Logger.Error($"Invalid number of appearances dat files: {appearanceDatFiles.Length}");
                return false;
            }

            _appearanceDatFile = appearanceDatFiles[0];

            if (string.IsNullOrEmpty(_appearanceDatFile) || !File.Exists(_appearanceDatFile))
            {
                Logger.Error($"Appearances .dat file does not exist: {_appearanceDatFile}");
                return false;
            }

            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Connection.Dispose();
                    Logger.Dispose();
                }

                disposedValue = true;
            }
        }

        ~Client()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all the managed resources used by the <see cref="Client"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

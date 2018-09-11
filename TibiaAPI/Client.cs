using System;
using System.IO;

namespace OXGaming.TibiaAPI
{
    public class Client : IDisposable
    {
        public Appearances.AppearanceStorage AppearanceStorage { get; } = new Appearances.AppearanceStorage();

        public WorldMap.WorldMapStorage WorldMapStorage { get; } = new WorldMap.WorldMapStorage();

        public Network.Connection Proxy { get; }

        public Client(string datFileName)
        {
            if (string.IsNullOrEmpty(datFileName))
            {
                throw new ArgumentNullException(nameof(datFileName));
            }

            if (!File.Exists(datFileName))
            {
                throw new FileNotFoundException("Tibia dat file not found.", datFileName);
            }

            using (var datFile = File.OpenRead(datFileName))
            {
                AppearanceStorage.LoadAppearances(datFile);
            }

            Proxy = new Network.Connection(this);
        }

        public bool StartProxy(bool enablePacketParsing = true)
        {
            return Proxy.Start(enablePacketParsing);
        }

        public void StopProxy()
        {
            Proxy.Stop();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Proxy.Dispose();
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

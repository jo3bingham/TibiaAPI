using System;
using System.IO;

namespace OXGaming.TibiaAPI
{
    public class Client : IDisposable
    {
        private readonly Appearances.AppearanceStorage _appearanceStorage = new Appearances.AppearanceStorage();

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
                _appearanceStorage.LoadAppearances(datFile);
            }

            Proxy = new Network.Connection(_appearanceStorage);
        }

        public bool StartProxy()
        {
            return Proxy.Start();
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

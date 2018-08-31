using System;

namespace OXGaming.TibiaAPI
{
    public class Client : IDisposable
    {
        public Network.Connection Proxy { get; } = new Network.Connection();

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

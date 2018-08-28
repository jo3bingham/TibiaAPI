using System;

namespace OXGaming.TibiaAPI
{
    public class Client : IDisposable
    {
        private Network.Connection _connection;

        public bool StartProxy()
        {
            if (_connection == null)
            {
                _connection = new Network.Connection();
            }

            return _connection.Start();
        }

        public void StopProxy()
        {
            if (_connection != null)
            {
                _connection.Stop();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        _connection.Dispose();
                    }
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

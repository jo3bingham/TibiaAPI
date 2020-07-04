using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace OXGaming.TibiaAPI.Utilities
{
    public class Logger : IDisposable
    {
        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error,
            Disabled
        }

        public enum LogOutput
        {
            Console,
            File
        }

        private readonly Dictionary<LogLevel, string> _logLevelMap = new Dictionary<LogLevel, string>()
        {
            { LogLevel.Debug, "DEBUG" },
            { LogLevel.Info, "INFO" },
            { LogLevel.Warning, "WARNING" },
            { LogLevel.Error, "ERROR" },
            { LogLevel.Disabled, "DISABLED" }
        };

        private readonly ConcurrentQueue<(LogLevel, string)> _logQueue = new ConcurrentQueue<(LogLevel, string)>();

        private readonly object _logLock = new object();

        private StreamWriter _outputFile = null;

        private Thread _loggingThread;

        private LogOutput _output = LogOutput.Console;

        private bool _isLogging = false;

        public LogLevel Level { get; set; } = LogLevel.Disabled;

        public LogOutput Output
        {
            get
            {
                return _output;
            }
            set
            {
                lock (_logLock)
                {
                    if (value == LogOutput.File && _outputFile == null)
                    {
                        var utcNow = DateTime.UtcNow;
                        var filename = $"{utcNow.Day}_{utcNow.Month}_{utcNow.Year}__{utcNow.Hour}_{utcNow.Minute}_{utcNow.Second}.log";
                        _outputFile = new StreamWriter(File.OpenWrite(filename));
                    }
                    _output = value;
                }
            }
        }

        public static LogLevel ConvertToLogLevel(string level)
        {
            if (level.Equals("debug", StringComparison.CurrentCultureIgnoreCase))
            {
                return LogLevel.Debug;
            }
            else if (level.Equals("info", StringComparison.CurrentCultureIgnoreCase))
            {
                return LogLevel.Info;
            }
            else if (level.Equals("warning", StringComparison.CurrentCultureIgnoreCase))
            {
                return LogLevel.Warning;
            }
            else if (level.Equals("error", StringComparison.CurrentCultureIgnoreCase))
            {
                return LogLevel.Error;
            }
            else if (level.Equals("disabled", StringComparison.CurrentCultureIgnoreCase))
            {
                return LogLevel.Disabled;
            }
            else
            {
                throw new ArgumentException($"[Logger.ConvertToLogLevel] Invalid input: {level}");
            }
        }

        public static LogOutput ConvertToLogOutput(string output)
        {
            if (output.Equals("output", StringComparison.CurrentCultureIgnoreCase))
            {
                return LogOutput.Console;
            }
            else if (output.Equals("file", StringComparison.CurrentCultureIgnoreCase))
            {
                return LogOutput.File;
            }
            else
            {
                throw new ArgumentException($"[Logger.ConvertToLogOutput] Invalid input: {output}");
            }
        }

        private void Log(LogLevel level, string text)
        {
            if (Level == LogLevel.Disabled || level < Level)
            {
                return;
            }

            _logQueue.Enqueue((level, text));

            if (!_isLogging)
            {
                try
                {
                    _isLogging = true;
                    _loggingThread = new Thread(new ThreadStart(LogQueue));
                    _loggingThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void LogQueue()
        {
            try
            {
                while (_logQueue.TryDequeue(out var data))
                {
                    var (level, text) = data;
                    if (level == LogLevel.Disabled || string.IsNullOrEmpty(text))
                    {
                        break;
                    }

                    text = $"{_logLevelMap[level]} {text}";

                    lock (_logLock)
                    {
                        if (level == LogLevel.Error)
                        {
                            Console.WriteLine(text);
                            if (_output == LogOutput.File && _outputFile != null)
                            {
                                _outputFile.WriteLine(text);
                            }
                        }
                        else if (_output == LogOutput.Console)
                        {
                            Console.WriteLine(text);
                        }
                        else if (_output == LogOutput.File)
                        {
                            _outputFile.WriteLine(text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _isLogging = false;
            }
        }

        public void Debug(string text)
        {
            Log(LogLevel.Debug, text);
        }

        public void Info(string text)
        {
            Log(LogLevel.Info, text);
        }

        public void Warning(string text)
        {
            Log(LogLevel.Warning, text);
        }

        public void Error(string text)
        {
            Log(LogLevel.Error, text);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_loggingThread != null)
                    {
                        _loggingThread.Join();
                    }

                    if (_outputFile != null)
                    {
                        _outputFile.Close();
                    }
                }

                disposedValue = true;
            }
        }

        ~Logger()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all the managed resources used by the <see cref="Logger"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

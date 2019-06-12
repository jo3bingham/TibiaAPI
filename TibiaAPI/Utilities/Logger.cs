using System;
using System.Collections.Generic;
using System.IO;

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

        private StreamWriter _outputFile = null;

        private LogOutput _output = LogOutput.Console;

        public LogLevel Level { get; set; } = LogLevel.Disabled;

        public LogOutput Output
        {
            get
            {
                return _output;
            }
            set
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

            text = $"{_logLevelMap[level]} {text}";

            if (level == LogLevel.Error)
            {
                Console.WriteLine(text);
                if (_outputFile != null)
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

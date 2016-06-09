using System;
using System.Collections.Generic;

namespace Notebook.Logger
{
    public class Log
    {
        #region Singleton
        private static readonly LoggerAbstract _consoleInstance = new ConsoleLoggerFactory().CreateLogger();
        private static readonly LoggerAbstract _fileInstance = new FileLoggerFactory().CreateLogger();
        private Log() { }
        static Log() { }

        public static LoggerAbstract ConsoleInstance
        {
            get { return _consoleInstance; }
        }

        public static LoggerAbstract FileInstance
        {
            get { return _fileInstance; }
        }
        #endregion
    }
}

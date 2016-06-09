using System;
using System.Collections.Generic;

namespace Notebook.Logger
{
    public abstract class LoggerAbstract
    {
        public abstract void Info();
        public abstract void Info(string infoMessage);
        public abstract void Debug();
        public abstract void Warning();
        public abstract void Warning(string warnMessage);
        public abstract void Error();
        public abstract void Error(string message);
    }
}

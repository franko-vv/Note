using System;
using System.Collections.Generic;

namespace Notebook.Logger
{
    abstract class LoggerCreator
    {
        public abstract LoggerAbstract CreateLogger();
    }
}

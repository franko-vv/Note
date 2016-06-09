using System;
using System.Collections.Generic;

namespace Notebook.Logger
{
    class FileLoggerFactory : LoggerCreator
    {
        public override LoggerAbstract CreateLogger()
        {
            return new FileLogger();
        }
    }
}

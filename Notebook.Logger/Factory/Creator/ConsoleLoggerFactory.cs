using System;
using System.Collections.Generic;

namespace Notebook.Logger
{
    class ConsoleLoggerFactory : LoggerCreator
    {
        public override LoggerAbstract CreateLogger()
        {
            return new ConsoleLogger();
        }
    }
}

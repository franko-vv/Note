using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Logger
{
    class ConsoleLogger : LoggerAbstract
    {
        public override void Debug()
        {
            WriteToConsole("Debug Console Message");
        }

        public override void Error()
        {
            WriteToConsole("Error Console Message", ConsoleColor.Red);
        }

        public override void Error(string errMessage)
        {
            WriteToConsole(errMessage, ConsoleColor.Red);
        }

        public override void Info()
        {
            WriteToConsole("Info Console Message", ConsoleColor.Green);
        }

        public override void Info(string infoMessage)
        {
            WriteToConsole(infoMessage, ConsoleColor.Green);
        }

        public override void Warning()
        {
            WriteToConsole("Warning Console Message");
        }

        public override void Warning(string warnMessage)
        {
            WriteToConsole(warnMessage, ConsoleColor.DarkCyan);
        }

        private void WriteToConsole(string message)
        {
            Console.WriteLine($"{DateTime.Now} - {message}");
        }
        private void WriteToConsole(string message, ConsoleColor cl)
        {
            Console.ForegroundColor = cl;
            Console.WriteLine($"{DateTime.Now} - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

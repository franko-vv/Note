using System;
using System.IO;
using System.Reflection;

namespace Notebook.Logger
{
    class FileLogger : LoggerAbstract
    {
        private readonly string _currentPath;
        private readonly string _fileLogPath;

        public FileLogger()
        {
            try
            {
                _currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                _fileLogPath = _currentPath + "\\log.txt";
            }
            catch (PathTooLongException ex) { Console.WriteLine(ex.StackTrace + "\n" + ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace + "\n" + ex.Message); }
        }

        public override void Debug()
        {
            WriteToFile("Debug message.");
        }

        public override void Error()
        {
            WriteToFile("Error message.");
        }

        public override void Error(string errMessage)
        {
            WriteToFile(errMessage);
        }

        public override void Info()
        {
            WriteToFile("Info message.");
        }

        public override void Info(string infoMessage)
        {
            WriteToFile(infoMessage);
        }

        public override void Warning()
        {
            WriteToFile("Warning message.");
        }

        public override void Warning(string warnMessage)
        {
            WriteToFile(warnMessage);
        }

        private void WriteToFile(string message)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_fileLogPath, true))
                {
                    sw.WriteLine($"{DateTime.Now} - {message}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace + "\n" + ex.Message);
            }
        }
    }
}

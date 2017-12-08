using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common
{
    public class LogManager
    {
        public LogManager()
        {
            Loggers = new List<ILogger>();
        }

        public LogManager(IEnumerable<ILogger> loggers)
            : this()
        {
            Loggers.AddRange(loggers);
        }

        public void AddFileLogger(string path)
        {
            Loggers.Add(new FileLogger(path));
        }

        public void AddConsoleLogger()
        {
            Loggers.Add(new ConsoleLogger());
        }

        public void Add(ILogger logger)
        {
            Loggers.Add(logger);
        }

        public List<ILogger> Loggers { get; set; }

        public void WriteLine()
        {
            WriteLine(string.Empty);
        }

        public void WriteLine(string format, params object[] args)
        {
            foreach (var logger in Loggers)
            {
                logger.WriteLine(format, args);
            }
        }
    }
}

using System;

namespace DC_ConnorBot
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("Debugger: " + message);
        }
        
    }
}

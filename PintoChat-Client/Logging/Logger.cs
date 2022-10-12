using System;

namespace PintoChatNS.Logging
{
    public class Logger
    {
        public static void Log(string header, string message, bool addNewLine = true)
        {
            if (addNewLine)
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} [{header}] {message}");
            else
                Console.Write($"{DateTime.Now.ToString("HH:mm:ss")} [{header}] {message}");
        }

        public static void LogLevel(LoggerLevel level, string message, bool addNewLine = true)
        {
            switch (level) 
            {
                case LoggerLevel.WARN:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LoggerLevel.ERROR:
                case LoggerLevel.SEVERE:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LoggerLevel.FATAL:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
            }
            Log(level.ToString(), message, addNewLine);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void LogVerbose(string message, bool addNewLine = true)
        {
            LogLevel(LoggerLevel.VERBOSE, message, addNewLine);
        }

        public static void LogInfo(string message, bool addNewLine = true)
        {
            LogLevel(LoggerLevel.INFO, message, addNewLine);
        }

        public static void LogWarn(string message, bool addNewLine = true)
        {
            LogLevel(LoggerLevel.WARN, message, addNewLine);
        }

        public static void LogError(string message, bool addNewLine = true)
        {
            LogLevel(LoggerLevel.ERROR, message, addNewLine);
        }

        public static void LogSevere(string message, bool addNewLine = true)
        {
            LogLevel(LoggerLevel.SEVERE, message, addNewLine);
        }

        public static void LogFatal(string message, bool addNewLine = true)
        {
            LogLevel(LoggerLevel.FATAL, message, addNewLine);
        }
    }
}
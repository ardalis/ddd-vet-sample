using System;
using System.Linq;

namespace DomainEventsConsole
{
    public static class ConsoleWriter
    {
        public static void FromUIEventHandlers(string message, params string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        public static void FromEmailEventHandlers(string message, params string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        public static void FromRepositories(string message, params string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }
    }
}

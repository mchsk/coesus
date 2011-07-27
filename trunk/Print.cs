using System;
using System.Collections.Generic;
using System.Text;

namespace coesus
{
    static class Print
    {
        public static void _Help()
        {
            Console.WriteLine("help..........");
        }

        public static void _Version()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            String ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            String toPrint = "";
            toPrint += " version ";
            toPrint += ver;
            for (int i = ver.Length; i < 34; i++)
            {
                toPrint += " ";
            }
            Console.WriteLine(toPrint);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void _Title()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" * * * * * * * * * * * * * * * * * * * * * ");
            Console.WriteLine(" * coesus                                * ");
            Console.WriteLine(" * the framework that eats your balls!   * ");
            Console.WriteLine(" * * * * * * * * * * * * * * * * * * * * * ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

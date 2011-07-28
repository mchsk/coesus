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

        public static void _Settings()
        {
            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - ");
            Console.WriteLine(String.Concat("Starting time   : ", Settings.Server.StartingTime));
            Console.WriteLine(String.Concat("Log file        : ", Settings.Server.LogFile));
            Console.WriteLine(String.Concat("Verbosity       : ", Settings.Server.Verbosity));
            Console.WriteLine(String.Concat("MySQL address   : ", Settings.Storage.MySQLaddr));
            Console.WriteLine(String.Concat("MySQL port      : ", Settings.Storage.MySQLport));
            Console.WriteLine(String.Concat("MySQL username  : ", Settings.Storage.MySQLusername));
            Console.WriteLine(String.Concat("MySQL password  : ", Settings.Storage.MySQLpassword));
            Console.WriteLine(String.Concat("MySQL database  : ", Settings.Storage.MySQLdatabase));
            Console.WriteLine(String.Concat("MySQL table     : ", Settings.Storage.MySQLtable));
            Console.WriteLine(String.Concat("MySQL user col. : ", Settings.Storage.MySQLusernameColumn));
            Console.WriteLine(String.Concat("MySQL pass col. : ", Settings.Storage.MySQLpasswordColumn));
            Console.WriteLine(String.Concat("MySQL pass type : ", Settings.Storage.MySQLpasswordType.ToString()));
            Console.WriteLine(String.Concat("MySQL secure    : ", Settings.Storage.MySQLSecure.ToString()));
            Console.WriteLine(String.Concat("Admin username  : ", Settings.Superuser.AdminUsername));
            Console.WriteLine(String.Concat("Admin password  : ", Settings.Superuser.AdminPassword));
            Console.WriteLine(String.Concat("Listening addr  : ", Settings.Clients.ListeningAddr));
            Console.WriteLine(String.Concat("Listeting port  : ", Settings.Clients.ListeningPort));
            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - ");
        }
    }
}

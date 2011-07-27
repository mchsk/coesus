using System;
using System.Collections.Generic;
using System.Text;

namespace coesus
{
    public class Settings
    {
        public class clStorage
        {
            public int MySQLport;
            public String MySQLaddr;
            public clStorage()
            {
                MySQLaddr = "localhost";
                MySQLport = 3306;
            }
        }
        public class clSuperuser
        {
            public String AdminUsername;
            public String AdminPassword;
            public clSuperuser()
            {
                AdminPassword = "admin";
                AdminUsername = "admin";
            }
        }
        public class clServer
        {
            public int Verbosity;
            public String LogFile;
            public DateTime StartingTime;
            public clServer()
            {
                Verbosity = 1;
                LogFile = "log.txt";
                StartingTime = DateTime.Now;
            }
            public TimeSpan Runtime()
            {
                DateTime now = DateTime.Now;
                TimeSpan subtr = now.Subtract(StartingTime);
                return subtr;
            }
        }
        public class clClients
        {
            public String ListeningAddr;
            public int ListeningPort;
            public clClients()
            {
                ListeningAddr = "0.0.0.0";
                ListeningPort = 13370;
            }
        }

        public static clStorage Storage = new clStorage();
        public static clSuperuser Superuser = new clSuperuser();
        public static clServer Server = new clServer();
        public static clClients Clients = new clClients();

        // constructor
        public  Settings()
        {
        }
    }
}

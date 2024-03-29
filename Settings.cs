﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;


namespace coesus
{
    /// <summary>
    /// Main storage class.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Storage
        /// </summary>
        public class clStorage
        {
            public enum PWtypes { plain, MD5, SaltMD5, SHA1 };
            public int MySQLport;
            public String MySQLaddr;
            public String MySQLusername, MySQLpassword, MySQLdatabase, MySQLtable;
            public String MySQLusernameColumn, MySQLpasswordColumn, MySQLsaltColumn;
            //public PWtypes MySQLpasswordType;//depr
            public Boolean MySQLSecure;
            public clStorage()
            {
                MySQLaddr = "localhost";
                MySQLport = 3306;
                MySQLusername = "root";
                MySQLpassword = "13245";
                MySQLdatabase = "db";
                MySQLtable = "members";
                MySQLusernameColumn = "username";
                MySQLpasswordColumn = "password";
                MySQLsaltColumn = null;
                //MySQLpasswordType = PWtypes.MD5;//--deprecated
                MySQLSecure = false;
            }
        }
        /// <summary>
        /// SU
        /// </summary>
        public class clSuperuser
        {
            public String AdminUsername;
            public String AdminPassword;
            public clSuperuser()
            {
                AdminPassword = "12345";
                AdminUsername = "admin";
            }
        }

        /// <summary>
        /// Server
        /// </summary>
        public class clServer
        {
            public Int16 Verbosity;
            public String LogFile;
            public DateTime StartingTime;
            public Boolean PrintHelp;
            public String appDirectory;
            public String pluginsSubdirectory;
            public clServer()
            {
                appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Verbosity = 1;
                LogFile = "log.txt";
                StartingTime = DateTime.Now;
                PrintHelp = false;
                pluginsSubdirectory = "plugins";
            }
            public TimeSpan Runtime()
            {
                DateTime now = DateTime.Now;
                TimeSpan subtr = now.Subtract(StartingTime);
                return subtr;
            }
        }

        /// <summary>
        /// Client
        /// </summary>
        public class clClients
        {
            public String ListeningAddr;
            public int ListeningPort, MaxClients;
            public clClients()
            {
                ListeningAddr = "0.0.0.0";
                ListeningPort = 13370;
                MaxClients = 0;
            }
        }


        // init ->>
        public static clStorage Storage = new clStorage();
        public static clSuperuser Superuser = new clSuperuser();
        public static clServer Server = new clServer();
        public static clClients Clients = new clClients();

        // constructor
        public  Settings()
        {
        }
        public static void _DebugInit()
        {

        }
        public static void _Apply(String[] args)
        {
            InitParameters parameters = new InitParameters(args);


            
            if (parameters["help"] == null)
            {
                try
                {
                    //Log file
                    if (parameters["log"] != null)
                    {
                        Server.LogFile = parameters["log"];
                    }
                    //Verbosity
                    if (parameters["v"] != null)
                    {
                        Server.Verbosity = Int16.Parse(parameters["v"]);
                    }
                    //MySQL address
                    if (parameters["m-addr"] != null)
                    {
                        Storage.MySQLaddr = parameters["m-addr"];
                    }
                    //MySQL port
                    if (parameters["m-port"] != null)
                    {
                        Storage.MySQLport = int.Parse(parameters["m-port"]);
                    }
                    //MySQL username
                    if (parameters["m-user"] != null)
                    {
                        Storage.MySQLusername = parameters["m-user"];
                    }
                    //MySQL password
                    if (parameters["m-pass"] != null)
                    {
                        Storage.MySQLpassword = parameters["m-pass"];
                    }
                    //MySQL database
                    if (parameters["m-db"] != null)
                    {
                        Storage.MySQLdatabase = parameters["m-db"];
                    }
                    //MySQL table
                    if (parameters["m-table"] != null)
                    {
                        Storage.MySQLtable = parameters["m-table"];
                    }
                    //MySQL username column
                    if (parameters["m-ucol"] != null)
                    {
                        Storage.MySQLusernameColumn = parameters["m-ucol"];
                    }
                    //MySQL password column
                    if (parameters["m-pcol"] != null)
                    {
                        Storage.MySQLpasswordColumn = parameters["m-pcol"];
                    }
                    //MySQL salt column
                    if (parameters["m-scol"] != null)
                    {
                        Storage.MySQLsaltColumn = parameters["m-scol"];
                    }
                    //MySQL passwordtype --deprecated
                    //if (parameters["m-passtype"] != null)
                    //{
                    //    String pwtype = parameters["m-passtype"];
                    //    if (String.Compare(pwtype, "plain") == 0) { Storage.MySQLpasswordType = clStorage.PWtypes.plain; }
                    //    if (String.Compare(pwtype, "MD5") == 0) { Storage.MySQLpasswordType = clStorage.PWtypes.MD5; }
                    //    if (String.Compare(pwtype, "SaltMD5") == 0) { Storage.MySQLpasswordType = clStorage.PWtypes.SaltMD5; }
                    //    if (String.Compare(pwtype, "SHA1") == 0) { Storage.MySQLpasswordType = clStorage.PWtypes.SHA1; }
                    //}
                    //MySQL secure
                    if (parameters["m-secure"] != null)
                    {
                        if (String.Compare(parameters["m-secure"], "1") == 0) { Storage.MySQLSecure = true; }
                        if (String.Compare(parameters["m-secure"], "0") == 0) { Storage.MySQLSecure = false; }
                    }
                    //Admin username
                    if (parameters["adminuser"] != null)
                    {
                        Superuser.AdminUsername = parameters["adminuser"];
                    }
                    //Admin password
                    if (parameters["adminpass"] != null)
                    {
                        Superuser.AdminPassword = parameters["adminpass"];
                    }
                    //Listening address
                    if (parameters["addr"] != null)
                    {
                        Clients.ListeningAddr = parameters["addr"];
                    }
                    //Listening port
                    if (parameters["port"] != null)
                    {
                        Clients.ListeningPort = int.Parse(parameters["port"]);
                    }
                    //Max clients
                    if (parameters["maxclients"] != null)
                    {
                        Clients.MaxClients = int.Parse(parameters["maxclients"]);
                    }
                    //Plugins directory
                    if (parameters["plugins"] != null)
                    {
                        Server.pluginsSubdirectory=parameters["plugins"];
                    }

                }
                catch
                {
                    Server.PrintHelp = true;
                }
            }
            else
            {
                Server.PrintHelp = true;
            }

        }
    }
}

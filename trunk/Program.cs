//TODO: default salt : null
//TODO: sha1,md5,plain,sha1salt,md5salt > common verify fc
//

using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Threading;
namespace coesus
{
    class Program
    {
        public static ProgramExtension app = new ProgramExtension();
        static void Main(string[] args)
        {
            Settings._Apply(args);
            Common.FillPlugins(app);
            Print._Title();
            Print._Version();
            if (Settings.Server.PrintHelp)
            {
                Print._Help();
            }
            else
            {
                // printout
                Print._Settings();
                Print._Plugins();

                // admin login - repeat until success
                // server starts listening and admin console after
                // admin logs in successfully
                Boolean adminsuccess = false;
                while (!adminsuccess)
                {
                    Console.WriteLine("Establishing connection to the database..");
                    adminsuccess = Common.LoginSuccess(Settings.Superuser.AdminUsername,
                        Settings.Superuser.AdminPassword);
                    if (!adminsuccess)
                    {
                        // if no success try it every 5 seconds
                        Thread.Sleep(5000);
                    }
                }


                // "nice" termination
                Console.CancelKeyPress += delegate
                {
                    // TODO: some clean-up -- we have pressed CtrlC
                    Console.WriteLine();
                    Console.WriteLine("Quit by CTRL+C has been spelled.");
                };

                // starting server
                // TODO:  <-


                Console.WriteLine(String.Concat("Connection accepted by ", Settings.Storage.MySQLaddr,
                    ". Press CTRL+C to terminate."));

                // admin console (user#0)
                while (true)
                {
                    Console.Write(String.Concat(Settings.Superuser.AdminUsername, "@coesux:"));
                    Console.WriteLine(Common.ParseCommand(Settings.Superuser.AdminUsername, Console.ReadLine()));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using _interface;
using MySql.Data.MySqlClient;



namespace coesus
{
    public class Plugin : IPluginIface
    {
        public static IAppIface mainProgram;
        public static List<IPluginIface> pluginsList;
        private static List<String> COMMANDS;

        public String Name
        {
            get
            {
                return "PunBB operating plugin.";
            }
        }
        public String Version
        {
            get
            {
                return "0.1";
            }
        }
        public List<String> Commands
        {
            get
            {
                return COMMANDS;
            }
        }
        /// <summary>
        /// /////////////////////////////////////
        /// </summary>
        /// 
        //help vars
        static String punBBdbName = "";
        static int punDBuserId = -1;


        public Plugin()
        {
            pluginsList = new List<IPluginIface>();
            COMMANDS = new List<String>(new String[] { "threads", "thread", "contact" });
        }

        public static String PunDBName()
        {
            if (punBBdbName.Length == 0)
            {
                //Console.WriteLine("PunDB title is not set yet, CACHING...");
                MySqlDataReader reader;
                String newName = "";
                //getting the name of actual topic table
                reader = mainProgram.SQLReader("SHOW TABLES;");
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString().EndsWith("_topics"))
                        {
                            newName = reader[0].ToString();
                            newName = newName.Substring(0, newName.Length - 7);
                            if (newName.Contains("."))
                            {
                                newName = newName.Substring(newName.IndexOf(".") + 1);
                            }
                            //Console.WriteLine(String.Concat("PunDBName set as: ", newName));
                        }
                    }
                    punBBdbName = newName;
                }
                else
                {
                    return "";
                }
                
            }
            else
            {
                //Console.WriteLine("PunDB title is CACHED");
            }
            return punBBdbName;
        }
        public void Init(IAppIface appIface)
        {
            mainProgram = appIface;
        }
        public void AllThePlugins(IPluginIface[] plugins)
        {
            pluginsList.AddRange(plugins);
        }
        public void PrintInit()
        {
            Console.WriteLine("   threads = Thread count");
            Console.WriteLine("   thread N = Gets the name of topic by index N");
            Console.WriteLine("   contact TYPE VALUES = Sets the contact (url/jabber/icq/msn/aim/yahoo/location)");
            Console.WriteLine("   *This plugin is not affiliated with PunBB creators nor forum itself.");
            Console.WriteLine("   *Written as an example plugin.");
        }


        public String threads(String Username, String arguments)
        {
            String topiccount = "||";
            String query = String.Concat("SELECT * FROM ", PunDBName(), "_topics");
            MySqlDataReader reader;

            //Console.WriteLine(String.Concat("QUERY: ", query));
            reader = mainProgram.SQLReader(query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    topiccount = reader[0].ToString();
                }
            }
            return topiccount;
        }


        public String thread(String Username, String arguments)
        {
            String topicname = "||";
            String query = String.Concat("SELECT ut.id, ut.subject FROM ", PunDBName(), "_topics",
                " AS ut WHERE ut.id='",arguments,"'");
            MySqlDataReader reader;

            //Console.WriteLine(String.Concat("QUERY: ", query));
            reader = mainProgram.SQLReader(query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    topicname = reader["subject"].ToString();
                }
            }
            return topicname;
        }

        public String contact(String Username, String arguments)
        {
            String postres = "||";

            // we need our own user index
            String Userindex = "-1";
            MySqlDataReader readerI;
            String queryI = String.Concat("SELECT ut.id, ut.username FROM ", PunDBName(), "_users",
                " AS ut WHERE ut.username='", Username,"'");

            readerI = mainProgram.SQLReader(queryI);
            if (readerI != null)
            {
                while (readerI.Read())
                {
                    Userindex = readerI["id"].ToString();
                }
            }
            //we have userindex,username
            //now we need type and value
            String Type = "";
            String Value = "";
            if ((arguments !=null) && (arguments.Contains(" ")))
            {
                if (arguments.Split(new char[] { ' ' }).Length > 1)
                {
                    int IndexSp = arguments.IndexOf(" ");
                    Type = arguments.Substring(0, IndexSp);
                    Value = arguments.Substring(IndexSp + 1, arguments.Length - IndexSp - 1);
                }
                String query = String.Concat("UPDATE `", PunDBName(), "`.`", PunDBName(), "_users` SET `", Type,
    "` = '", Value, "' WHERE `", PunDBName(), "_users`.`id` =", Userindex);
                MySqlDataReader reader;

                try
                {
                    reader = mainProgram.SQLReader(query);
                    postres = "SET.";
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
            }

            //


            return postres;
        }

        public String CommitCommand(String Username, String Command, String arguments)
        {
            String ret = "||";
            if (String.Compare(Command, "threads") == 0)
            {
                ret = threads(Username, arguments);
            }
            if (String.Compare(Command, "thread") == 0)
            {
                ret = thread(Username, arguments);
            }
            if (String.Compare(Command, "contact") == 0)
            {
                ret = contact(Username, arguments);
            }
            return ret;
        }
        //public void DoIt()
        //{
        //    // iteration through all the plugins
        //    foreach (IPluginIface plugin in pluginsList)
        //    {
        //        //if it s not this, do the example
        //        if (plugin.Name != Name)
        //        {
        //            plugin.DoIt();
        //        }
        //    }
        //}

    }
}

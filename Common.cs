using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using _interface;
using System.Collections;

namespace coesus
{
    public class Common
    {
        public class clPlugins
        {
            public String path;
            public String error;
            public Boolean isValid;
            public IPluginIface plugin;
            public clPlugins(String path, Boolean init)
            {
                this.path = path;
            }
        }
        public static List<clPlugins> plugins = new List<clPlugins>();


        // adds the plugin
        public static void AddPlugin(String path)
        {
            if (String.Compare(Path.GetFileName(path), "_interface.dll") != 0) // we exclude iface dll
            {
                clPlugins clp = new clPlugins(path, false);
                clp.isValid = false;
                try
					{
						Assembly thisAssembly = Assembly.LoadFile(path);
                        IPluginIface plugin = (IPluginIface)thisAssembly.CreateInstance("coesus.Plugin");
						if(plugin != null)
                        {
                            clp.isValid = true;
                            clp.plugin = plugin;
							plugins.Add(clp);
                        }
					}
				catch(Exception e)
					{
                        clp.error = ".NET assembly error";
                        plugins.Add(clp);
					}
            }
        }
        public static  void FillPlugins(IAppIface app)
        {
            String dir = String.Concat(
                Settings.Server.appDirectory, "\\", Settings.Server.pluginsSubdirectory);
            if (Directory.Exists(dir))
            {
                String[] filenames = Directory.GetFiles(dir, "*.dll");
                foreach (String fname in filenames)
                {
                    AddPlugin(fname);
                }

                // init each
                foreach (clPlugins clpx in plugins)
                {
                    if (clpx.isValid)
                    {
                        clpx.plugin.Init(app);
                        // Console.WriteLine(String.Concat("INIT ", clpx.plugin.Name, " ", app.ToString()));
                    }
                }

                // passing list of the plugins to each
                IPluginIface[] pluginIfaces = new IPluginIface[plugins.Count];

                for (int i = 0; i < plugins.Count; i++)
                {
                    if (plugins[i].isValid)
                    {
                        pluginIfaces[i] = (IPluginIface)plugins[i].plugin;
                    }
                }

                foreach (clPlugins clpx in plugins)
                {
                    if (clpx.isValid)
                    {
                        clpx.plugin.AllThePlugins(pluginIfaces);
                    }
                }

            }

        }

        // returns SHA1 hash
        public static string HashSHA1(string userPassword)
        {
            return BitConverter.ToString(SHA1Managed.Create().ComputeHash(
                Encoding.Default.GetBytes(userPassword))).Replace("-", "").ToLower();
        }

        // returns the hash of plain password, eventually with salt
        // currently only SHA1/SHA1+salt //TODO:
        public static String HASH(String str, String salt)
        {
            if (salt == null)
            {
                return HashSHA1(str);
            }
            else
            {
                return HashSHA1(String.Concat(salt, HashSHA1(str)));
            }
            
        }

        public static String ParseCommand(String user, String line)
        {
            String cmd = String.Empty;
            String arguments = null;
            String res = null;
            if (line.Contains(" "))
            {
                if (line.Split(new char[] { ' ' }).Length > 1)
                {
                    int IndexSp = line.IndexOf(" ");
                    cmd = line.Substring(0, IndexSp);
                    arguments = line.Substring(IndexSp + 1, line.Length - IndexSp - 1);
                }
                else
                {
                    int IndexSp = line.IndexOf(" ");
                    cmd = line.Substring(0, IndexSp);
                }
            }
            else { cmd = line; }

            res = Common.PreProcessCommand(user, cmd, arguments);

            if (res == null)
            {
                if (line.Length > 0)
                {
                    res = "||";
                }
            }
            return res;
        }

        public static String PreProcessCommand(String username,String command, String arguments)
        {
            bool Found = false;
            int PluginID = -1;
            if ((Common.plugins.Count > 0) && (command.Length>0))
            {
                
                for (int j=0; j<Common.plugins.Count; j++)
                {
                    Common.clPlugins clp = Common.plugins[j];
                    if (clp.isValid)
                    {
                        if (clp.plugin.Commands.Count > 0)
                        {
                            for (int i = 0; i<clp.plugin.Commands.Count; i++)
                            {
                                if (String.Compare(clp.plugin.Commands[i], command) == 0)
                                {
                                    Found = true;
                                    PluginID = j;
                                }
                            }
                        }
                    }
                }
            }
            if (Found == false)
            {
                return null;
            }
            else
            {
                return Common.plugins[PluginID].plugin.CommitCommand(username, command, arguments);
            }
        }

        // tries to login the client using mysql
        public static Boolean LoginSuccess(String username, String password)
        {
            //connection string
            String connString = String.Concat("Database=", Settings.Storage.MySQLdatabase,
                ";DataSource=", Settings.Storage.MySQLaddr, ";UserId=", Settings.Storage.MySQLusername,
                ";Password=", Settings.Storage.MySQLpassword);
            //query string
            String query = String.Concat(
                "SELECT ut.", Settings.Storage.MySQLusernameColumn,
                     ", ut.", Settings.Storage.MySQLpasswordColumn,
                     ", ut.", Settings.Storage.MySQLsaltColumn,
                " FROM ", Settings.Storage.MySQLtable, " AS ut",
                " WHERE ut.", Settings.Storage.MySQLusernameColumn,
                     " = '", username, "'");

            //Console.WriteLine(query);
            MySqlConnection connection = new MySqlConnection(connString);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = query;
            //reader
            try
            {
                connection.Open();
                Reader = command.ExecuteReader();

                Boolean found = false;
                Boolean matching = false;

                //rows iteration
                while (Reader.Read())
                {
                    found = true;
                    try
                    {
                        if (String.Compare(Reader[Settings.Storage.MySQLpasswordColumn].ToString(),
                            Common.HASH(password, Reader[Settings.Storage.MySQLsaltColumn].ToString())) == 0)
                        {
                            matching = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                connection.Close();
                if ((found) && (matching))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception  ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}

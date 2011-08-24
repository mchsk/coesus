using System;
using System.Collections.Generic;
using System.Text;
using _interface;
using MySql.Data.MySqlClient;

namespace coesus
{
    public class ProgramExtension : IAppIface
    {
        public delegate void CustomEventHandler(object sender, EventArgs e);
        public event CustomEventHandler CustomEvent;
        public MySqlDataReader Reader;
        public ProgramExtension()
        {

        }

        // log function
        public void Log(String msg)
        {
            Console.WriteLine(String.Concat("Logging..: ", msg));
        }

        //

        // custom methot calling an event
        public void CustomEventCallingMethod()
        {
            OnCustomEvent(this);
        }

        protected void OnCustomEvent(object sender)
        {
            if (CustomEvent != null)
                CustomEvent(sender, new EventArgs());
        }
        public MySqlDataReader SQLReader(String query)
        {
            //Console.WriteLine(query);
            //connection string
            String connString = String.Concat("Database=", Settings.Storage.MySQLdatabase,
                ";DataSource=", Settings.Storage.MySQLaddr, ";UserId=", Settings.Storage.MySQLusername,
                ";Password=", Settings.Storage.MySQLpassword);

            MySqlConnection connection = new MySqlConnection(connString);
            MySqlCommand command = connection.CreateCommand();


            try
            {
                command.CommandText = query;
                //reader
                connection.Open();
                Reader = command.ExecuteReader();
                return Reader;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }
    }
}

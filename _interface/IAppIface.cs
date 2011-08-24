using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace _interface
{
    public interface IAppIface
    {
        void CustomEventCallingMethod();
        void Log(String msg);
        MySqlDataReader SQLReader(String query);
    }
}

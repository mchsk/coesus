using System;
using System.Collections.Generic;
using System.Text;


namespace _interface
{
    public interface IPluginIface
    {
        //stores the base instance
        void Init(IAppIface appIface);
        
        //all the plugins (WITH ERRORNEOUS)!
        void AllThePlugins(IPluginIface[] plugins);

        //void DoIt();

        //prints the init text
        void PrintInit();


        String CommitCommand(String Username, String Command, String arguments);
        //name of the plugin
        String Name
        {
            get;
        }

        //name of the plugin
        String Version
        {
            get;
        }

        //list of plugins
        List<String> Commands
        {
            get;
        }
    }
}

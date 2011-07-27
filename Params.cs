using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Collections.Specialized;


namespace coesus
{
    /// <summary>

    /// This class is responsible for the initial parameters parsing.

    /// </summary>

    public class Params{

        private StringDictionary Parameters;

        public Params(String[] initParams)
        {
            Parameters = new StringDictionary();
            Regex Spliter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase|RegexOptions.Compiled);
            Regex Remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase|RegexOptions.Compiled);

            String Parameter = null;
            String[] Parts;

            foreach (String currentParam in initParams)
            {
                Parts = Spliter.Split(currentParam,3);

                switch(Parts.Length){
                case 1:
                    if(Parameter != null)
                    {
                        if(!Parameters.ContainsKey(Parameter)) 
                        {
                            Parts[0] = 
                                Remover.Replace(Parts[0], "$1");

                            Parameters.Add(Parameter, Parts[0]);
                        }
                        Parameter=null;
                    }
                    // value only

                    break;
                case 2:
                    if(Parameter!=null)
                    {
                        if(!Parameters.ContainsKey(Parameter)) 
                            Parameters.Add(Parameter, "true");
                    }
                    Parameter=Parts[1];
                    break;
                case 3:
                    if(Parameter != null)
                    {
                        if(!Parameters.ContainsKey(Parameter)) 
                            Parameters.Add(Parameter, "true");
                    }

                    Parameter = Parts[1];
                    if(!Parameters.ContainsKey(Parameter))
                    {
                        Parts[2] = Remover.Replace(Parts[2], "$1");
                        Parameters.Add(Parameter, Parts[2]);
                    }

                    Parameter=null;
                    break;
                }
            }
            if(Parameter != null)
            {
                if(!Parameters.ContainsKey(Parameter)) 
                    Parameters.Add(Parameter, "true");
            }
        }
        public string this [string Param]
        {
            get
            {
                return(Parameters[Param]);
            }
        }
    }
}
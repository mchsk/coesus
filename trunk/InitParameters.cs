using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace coesus
{
    public class InitParameters
    {
        // a dictionary
        private StringDictionary Parameters;

        public InitParameters(String[] paramsBunch)
        {
            // vars
            Parameters = new StringDictionary();
            String Parameter = null;
            String[] Parts;

            // regural expressions
            Regex regSplit = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex regExclude = new Regex(@"^['""]?(.*?)['""]?$",  RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // parsing
            foreach (String currentParam in paramsBunch)
            {
                Parts = regSplit.Split(currentParam, 3);

                switch (Parts.Length)
                {
                    case 1:
                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                            {
                                Parts[0] =
                                    regExclude.Replace(Parts[0], "$1");

                                Parameters.Add(Parameter, Parts[0]);
                            }
                            Parameter = null;
                        }
                        break;
                    case 2:
                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                                Parameters.Add(Parameter, "true");
                        }
                        Parameter = Parts[1];
                        break;
                    case 3:
                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                                Parameters.Add(Parameter, "true");
                        }

                        Parameter = Parts[1];
                        if (!Parameters.ContainsKey(Parameter))
                        {
                            Parts[2] = regExclude.Replace(Parts[2], "$1");
                            Parameters.Add(Parameter, Parts[2]);
                        }

                        Parameter = null;
                        break;
                }
            }
            if (Parameter != null)
            {
                if (!Parameters.ContainsKey(Parameter))
                    Parameters.Add(Parameter, "true");
            }
        }

        public String this[String Param]
        {
            get
            {
                return (Parameters[Param]);
            }
        }
    }
}
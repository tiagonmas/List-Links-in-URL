using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plossum.CommandLine;

namespace ListLinks
{

    [CommandLineManager(ApplicationName = "\nList Links",
        Copyright = "Lists all links that exist in a given page")]
    class CmdLineOptions
    {
        [CommandLineOption(Description = "Displays this help text")]
        public bool Help = false;

        [CommandLineOption(Description = "Specifies the URL to look for", MinOccurs = 1)]
        public string URL
        {
            get { return mURL; }
            set
            {
                mURL = value;
            }
        }

        [CommandLineOption(Description = "The number of internal links the crawler will follow", MinOccurs = 0)]
        public int Max
        {
            get { return mMax; }
            set
            {
                mMax = value;
            }
        }
        private string mURL;
        private int mMax=1;
    }

}


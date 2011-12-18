using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plossum.CommandLine;
using System.Net;
using HtmlAgilityPack;

namespace ListLinks
{
    class Program
    {
        static int MaxLevel;  //Max levelto Crawl
        static HashSet<string> VisitedUrls = new HashSet<string>();
        static HashSet<string> FoundUrls = new HashSet<string>(); 
        static int Main(string[] args)
        {
            

            #region CommandLineArgsProcessing
            CmdLineOptions cmdLine = new CmdLineOptions();
            CommandLineParser parser = new CommandLineParser(cmdLine);
            parser.Parse();
            Console.WriteLine(parser.UsageInfo.GetHeaderAsString(78));

            if (cmdLine.Help)
            {
                Console.WriteLine(parser.UsageInfo.GetOptionsAsString(78));
                return 0;
            }
            else if (parser.HasErrors)
            {
                Console.WriteLine(parser.UsageInfo.GetErrorsAsString(78));
                return -1;
            }
            Console.WriteLine("Looking for links in {0}", cmdLine.URL);
            MaxLevel=cmdLine.Max;

            #endregion

            try
            {
                Crawl(cmdLine.URL,1);

                Console.WriteLine("\n\nDumping Found URLS:");
                foreach (string str in FoundUrls)
                {
                    Console.WriteLine(str);    
                }
            }
            catch (System.Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: {0}\n", exc.ToString());
                Console.ResetColor();
            }
            return 1;
        }

        /// <summary>
        /// Crawl a given URL if level of depth is still under max defined in cmd line
        /// </summary>
        /// <param name="url">The url to crawl</param>
        /// <param name="level">The level of depth we are in</param>
        /// <returns></returns>
        private static void Crawl(string url,int level)
        {
            if (level > MaxLevel
                || !VisitedUrls.Add(url)
                || url.StartsWith("javascript")
                || url.EndsWith("zip")
                || url.EndsWith("rar")
                || url.EndsWith("exe")
                || url.EndsWith("jpg")
                || url.EndsWith("gif")
                || url.EndsWith("png")) 
                {return;}

            Uri CurrentUrl = new Uri(url); 

            Console.Write(String.Format("CRAWLING {0} level {1}\n",url,level));
            //add url to visited urls. If false means we already crawled it
            
            WebClient client = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            
            try
            {
                string html = client.DownloadString(url);
                if (!client.ResponseHeaders["Content-Type"].StartsWith("text/html") || String.IsNullOrEmpty(html)) 
                    { return; }
                doc.LoadHtml(html);
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    
                    HtmlAttribute att = link.Attributes["href"];

                    Uri NextUrl=null;
                    if(!(Uri.TryCreate(att.Value, UriKind.Absolute, out NextUrl)))
                    {
                        if (!(Uri.TryCreate(CurrentUrl.Scheme+"://"+CurrentUrl.Host+"/"+att.Value, UriKind.Absolute, out NextUrl)))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error. Unable to open create url {0}", att.Value);
                            Console.ResetColor();
                        }
                    }



                    //If link is in same host, crawl it. If not just output link to console
                    if (CurrentUrl.Host == NextUrl.Host)
                    {
                        Crawl(NextUrl.AbsoluteUri, level + 1); 
                    }
                    else
                    {
                        //for (int i = 0; i < level; i++) Console.Write("\t");
                        if (!NextUrl.AbsoluteUri.StartsWith("javascript"))
                        {
                            FoundUrls.Add(NextUrl.AbsoluteUri);
                            Console.Write(".");
                        }
                    }

                }
            }
            catch (System.Net.WebException exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error. Unable to open site {0}", exc.Message);
                Console.ResetColor();
            }
        }


        /// <summary>
        /// Make sure a URL starts with http 
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
        private static string PrepURL(string siteUrl)
        {
            if (siteUrl.Contains("http://"))
                return siteUrl;
            else
                return "http://" + siteUrl;
        }
    }
}

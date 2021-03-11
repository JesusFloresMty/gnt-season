using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SeasonsGNT.Back
{
    public class Logger
    {
        string fileLoc = @"C:\Users\Administrator\Desktop\JIFG\SEASONS\LOG.txt";
        //string fileLoc = @"C:\Users\TPal\Desktop\PUBSS_V3\SEASON.txt";

        public void LogMessageToFile(string msg)
        {
            if (File.Exists(fileLoc))
            {
                using (StreamWriter sw = new StreamWriter(fileLoc, true))
                {
                    DateTime nyc = DateTime.Now;
                    DateTime horasSum = nyc.AddMinutes(480);
                    sw.Write(msg + " " + horasSum + "\n");
                }
            }
        }
    }
}
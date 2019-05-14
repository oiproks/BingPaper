using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingPaper
{
    class Logger
    {
        public static void WriteLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteLog(string message)
        {
            try
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteLog(string[] message)
        {
            try
            {
                if (message.Length > 0)
                {
                    StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", true);
                    sw.WriteLine(DateTime.Now.ToString() + ": ");
                    foreach (string text in message)
                        sw.WriteLine("\t" + text);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {
            }
        }
    }
}

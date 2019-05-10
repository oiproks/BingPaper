using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BingPaper
{
    class Utilities
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void CheckImagePath()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "Images";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

        public static string PrepareFileName(int fileIndex)
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "Images" + "\\wallpaper_" + fileIndex + ".bmp";
            return fileName;
        }

        public static string PrepareFileName(string multi)
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "Images" + "\\wallpaper_" + multi + ".bmp";
            return fileName;
        }

        public static void SetWallpaper (Int32 val, string fileName)
        {
            SystemParametersInfo(val, 0, fileName, 0x01 | 0x02);
        }
    }
}

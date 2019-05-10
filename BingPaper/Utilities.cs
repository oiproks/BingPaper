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

            }
        }

        public static string CheckImagePath(int fileIndex)
        {
            string fileName = string.Empty;
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "Images";
                fileName = path + "\\wallpaper_" + fileIndex + ".bmp";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                
            }
            return fileName;
        }

        public static void setWallpaper (Int32 val, string fileName)
        {
            SystemParametersInfo(val, 0, fileName, 0x01 | 0x02);
        }
    }
}

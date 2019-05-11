using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace BingPaper
{
    class Utilities
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

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

        public static void SetWallpaper (Int32 val, string fileName, Style style)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Tiled)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }

            SystemParametersInfo(val, 0, fileName, 0x01 | 0x02);
        }
    }
}

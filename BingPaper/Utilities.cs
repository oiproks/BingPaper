using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace BingPaper
{
    class Utilities
    {
        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched,
            Fill
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

        public static string PrepareFileName(bool single, string imageName = "multi")
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, "Images") + "\\";
            if (single)
            {
                string nameCode = string.Empty;
                for (int x = 0; x < imageName.Length;)
                {
                    nameCode += imageName[x];
                    x += 4;
                }
                string[] files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Images")).ToArray();
                foreach (string file in files)
                    if (file.Contains(nameCode))
                        File.Delete(file);

                fileName += DateTime.Now.ToString("yy_MM_dd") + " - " + nameCode + ".bmp";
            }
            else
            {
                fileName += "wallpaper_" + imageName + ".bmp";
            }
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
            if (style == Style.Fill)
            {
                key.SetValue(@"WallpaperStyle", 10.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            NativeMethods.SystemParametersInfo(val, 0, fileName, 0x01 | 0x02);
        }
    }
}

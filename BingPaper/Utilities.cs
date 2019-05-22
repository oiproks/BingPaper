using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
                Logger.WriteLog("Utilities", ex);
            }
        }

        public static string PrepareFileName(bool single, Bitmap bitmap, string imageName = "multi", string date = "")
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images") + "\\";
            if (single)
            {
                string nameCode = string.Empty;
                for (int x = 0; x < imageName.Length;)
                {
                    nameCode += imageName[x];
                    x += 4;
                }
                string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images")).ToArray();
                foreach (string file in files)
                    if (file.Contains(nameCode))
                        File.Delete(file);
                DateTime oDate = DateTime.ParseExact(date, "yyyyMMdd", null);
                date = date == "" ? DateTime.Now.ToString("yy_MM_dd") : oDate.ToString("yy_MM_dd");
                fileName += date + " - " + nameCode + ".bmp";
            }
            else
            {
                fileName += "wallpaper_" + imageName + ".bmp";
            }
            bitmap.Save(fileName, ImageFormat.Bmp);
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

        public static Bitmap CreateMultiScreenWall(bool save, string name, List<ScreenAndWallpaper> screenList)
        {
            Bitmap finalImage = null;
            try
            {
                int width = 0;
                int height = 0;
                foreach (ScreenAndWallpaper element in screenList)
                {
                    width += element.screen.WorkingArea.Width;
                    height = element.screen.WorkingArea.Height > height ? element.screen.WorkingArea.Height : height;
                }

                finalImage = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.Clear(Color.Black);
                    foreach (ScreenAndWallpaper element in screenList)
                    {
                        if (save)
                            PrepareFileName(true, element.image.bitmap, element.image.name, element.image.date);
                        float ratioImage = (float)element.image.bitmap.Width / (float)element.image.bitmap.Height;
                        float ratioScreen = (float)element.screen.WorkingArea.Width / (float)element.screen.WorkingArea.Height;
                        if (ratioScreen > ratioImage)
                            g.DrawImage(element.image.bitmap, new Rectangle(element.screen.Bounds.X, 0, element.image.bitmap.Width, height));
                        else
                            g.DrawImage(element.image.bitmap, new Rectangle(element.screen.Bounds.X, 0, (int)(element.screen.WorkingArea.Height * ratioImage), element.screen.WorkingArea.Height));
                    }
                }
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();
                Logger.WriteLog(name, ex);
            }
            return finalImage;
        }
    }
}

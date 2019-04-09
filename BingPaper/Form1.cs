using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class Form1 : Form
    {
        private const string bing = "http://www.bing.com";
        private string file = string.Empty;
        private int file_index = 0;
        private List<Files> files;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public Form1()
        {
            InitializeComponent();

            files = new List<Files>();

            CheckImagePath();
            Logger.WriteLog("Looking for new wallpaper");

            DownloadJSON();
        }

        private void DownloadJSON()
        {
            string url = Properties.Resources.bing_json_url_1;
            var json = new WebClient().DownloadString(url);
            JObject objects = JObject.Parse(json);
            ListBuilder(ref files, objects);

            url = Properties.Resources.bing_json_url_2;
            json = new WebClient().DownloadString(url);
            objects = JObject.Parse(json);
            ListBuilder(ref files, objects);

            string firstDate = files.OrderByDescending(o => o.date).ToList()[0].date;

            try
            {
                Parallel.ForEach(files, (file) =>
                {
                    WebRequest request = WebRequest.Create(file.url);
                    WebResponse response = request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    file.bitmap = new Bitmap(responseStream);
                    if (file.date.Equals(firstDate))
                    {
                        pctBoxWall.Image = file.bitmap;
                        lblName.Text = file.name;
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            finally
            {
                files = files.OrderByDescending(o => o.date).ToList();
            }
        }

        private void ListBuilder(ref List<Files> files, JObject objects)
        {
            for (int x = 0; x < 7; x++)
            {
                string fileUrl = bing + objects.SelectToken("images")[x].SelectToken("url").ToString();
                string name = objects.SelectToken("images")[x].SelectToken("copyright").ToString();
                string date = objects.SelectToken("images")[x].SelectToken("startdate").ToString();
                name = name.Substring(0, name.IndexOf(" ("));
                files.Add(new Files(fileUrl, Encoding.UTF8.GetString(Encoding.Default.GetBytes(name)), date, null));
            }
        }

        private void btnSetWall_Click(object sender, EventArgs e)
        {
            CheckImagePath();
            Bitmap bitmap = (Bitmap)pctBoxWall.Image;
            bitmap.Save(file, ImageFormat.Bmp);

            SystemParametersInfo(0x0014, 0, file, 0x01 | 0x02);
        }

        private void CheckImagePath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images";
            file = path + "\\wallpaper_" + file_index + ".bmp";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            //int index = fileBitmaps.IndexOf((Bitmap)pctBoxWall.Image);
            int index = files.FindIndex(f => f.bitmap == (Bitmap)pctBoxWall.Image);
            if (index < files.Count - 1)
            {
                file_index = index + 1;
                pctBoxWall.Image = files[file_index].bitmap;
                lblName.Text = files[file_index].name;
            }
            else
            {
                pctBoxWall.Image = files[0].bitmap;
                lblName.Text = files[0].name;
                file_index = 0;
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            int index = files.FindIndex(f => f.bitmap == (Bitmap)pctBoxWall.Image);
            if (index > 0)
            {
                file_index = index - 1;
                pctBoxWall.Image = files[file_index].bitmap;
                lblName.Text = files[file_index].name;
  
            }
            else
            {
                file_index = files.Count() - 1;
                pctBoxWall.Image = files[file_index].bitmap;
                lblName.Text = files[file_index].name;
            }
        }
    }

    public class Logger
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
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
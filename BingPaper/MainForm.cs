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
    public partial class MainForm : Form
    {
        private const string bing = "http://www.bing.com";
        private string fileName = string.Empty;
        private int file_index = 0;
        private List<Files> files;
        bool mouseDown = false;
        Point lastLocation;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        #region Preliminary Work
        public MainForm()
        {
            InitializeComponent();

            files = new List<Files>();

            Utilities.CheckImagePath();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.WriteLog("Looking for new wallpaper");

            DownloadJSON();

            elementOnPicture(btnSetWall);
            elementOnPicture(btnSetMulti); 
            elementOnPicture(bingPaper2);

            if (Screen.AllScreens.Count() > 1)
            {
                btnSetMulti.Visible = true;
            }
        }

        private void elementOnPicture(Control control)
        {
            var pos = this.PointToScreen(control.Location);
            pos = pctBoxWall.PointToClient(pos);
            control.Parent = pctBoxWall;
            control.Location = pos;
            control.BackColor = Color.Transparent;
        }
        #endregion

        #region Bing Magic
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
        #endregion

        #region Buttons
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
        
        private void btnSetWall_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            fileName = Utilities.CheckImagePath(file_index);
            bitmap = (Bitmap)pctBoxWall.Image;
            bitmap.Save(fileName, ImageFormat.Bmp);
            Utilities.setWallpaper(0x0014, fileName);
        }

        private void btnSetMultitWall_Click(object sender, EventArgs e)
        {
            //TODO: open multi form and pass needed variables
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            //TODO: open tab with info, link to git, paypal, etc...
        }
        #endregion

        #region Form Commands
        private void Interface_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Interface_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(
                    (Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);

                Update();
            }
        }

        private void Interface_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
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
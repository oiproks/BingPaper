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
        private string fileName = string.Empty;
        private int file_index = 0;
        private List<Files> files;
        bool mouseDown = false;
        Point lastLocation;
        Bitmap[] wallList;
        List<CheckBox> selecters = new List<CheckBox>();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        #region Preliminary Work
        public Form1()
        {
            InitializeComponent();

            files = new List<Files>();

            CheckImagePath();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wallList = new Bitmap[Screen.AllScreens.Count()];

            Logger.WriteLog("Looking for new wallpaper");

            DownloadJSON();

            //var pos = this.PointToScreen(btnSetWall.Location);
            //pos = pctBoxWall.PointToClient(pos);
            //btnSetWall.Parent = pctBoxWall;
            //btnSetWall.Location = pos;
            //btnSetWall.BackColor = Color.Transparent;
            elementOnPicture(btnSetWall);
            elementOnPicture(bingPaper2);
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
                createShowNextCheck(file_index);
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
            hidePrevCheck(index);
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
            createShowNextCheck(file_index);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            int index = files.FindIndex(f => f.bitmap == (Bitmap)pctBoxWall.Image);
            hidePrevCheck(index);
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
            createShowNextCheck(file_index);
        }
        
        private void btnSetWall_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            CheckImagePath();
            bitmap = (Bitmap)pctBoxWall.Image;
            bitmap.Save(fileName, ImageFormat.Bmp);
            SystemParametersInfo(0x0014, 0, fileName, 0x01 | 0x02);
            foreach (CheckBox checker in selecters)
            {
                checker.CheckedChanged -= addWallToList;
                checker.Checked = false;
                checker.CheckedChanged += addWallToList;
            }
            Array.Clear(wallList, 0, wallList.Length);
        }

        private void btnSetMultitWall_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            CheckImagePath();
            if (wallList.Count(x => x != null) == wallList.Length)
            {
                bitmap = CreateMultiScreenWall();
                fileName = fileName.Substring(0, fileName.IndexOf("\\wallpaper_")) + "\\wallpaper_multi.bmp";
                bitmap.Save(fileName, ImageFormat.Bmp);
                SystemParametersInfo(20, 0, fileName, 0x01 | 0x02);
            }
            else
            {
                bitmap = (Bitmap)pctBoxWall.Image;
                bitmap.Save(fileName, ImageFormat.Bmp);
                SystemParametersInfo(0x0014, 0, fileName, 0x01 | 0x02);
            }
            foreach (CheckBox checker in selecters)
            {
                checker.CheckedChanged -= addWallToList;
                checker.Checked = false;
                checker.CheckedChanged += addWallToList;
            }
            Array.Clear(wallList, 0, wallList.Length);
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            //TODO: open tab with info, link to git, paypal, etc...
        }
        #endregion

        #region Set Multiple Wallpapers
        private Bitmap CreateMultiScreenWall()
        {
            //TODO: Use screen resolution and position to resize and locate the wallpapers
            //foreach (var screen in Screen.AllScreens)
            //{
            //    Console.WriteLine("Device Name: " + screen.DeviceName);
            //    Console.WriteLine("Bounds: " + screen.Bounds.ToString());
            //    Console.WriteLine("Type: " + screen.GetType().ToString());
            //    Console.WriteLine("Working Area: " + screen.WorkingArea.ToString());
            //    Console.WriteLine("Primary Screen: " + screen.Primary.ToString());
            //}

            Bitmap finalImage = null;
            try
            {
                int width = 0;
                int height = 0;
                int x = 0;
                foreach (Bitmap wall in wallList)
                {
                    width += wall.Width;
                    height = wall.Height > height ? wall.Height : height;
                }

                finalImage = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.Clear(Color.Black);
                    int offset = 0;
                    foreach (Bitmap image in wallList)
                    {
                        g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
                        offset += image.Width;
                    }
                }
                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();
                throw ex;
            }
        }

        private void createShowNextCheck(int index)
        {
            CheckBox selecter = new CheckBox()
            {
                Location = new Point(10, 24),
                Name = "Checker" + index,
                Text = string.Empty
            };
            selecter.CheckedChanged += addWallToList;
            ;
            if (!selecters.Any(x => x.Name == ("Checker" + index)))
            {
                Controls.Add(selecter);

                var pos = PointToScreen(selecter.Location);
                pos = pctBoxWall.PointToClient(pos);
                selecter.Parent = pctBoxWall;
                selecter.Location = pos;
                selecter.BackColor = Color.Transparent;

                selecters.Add(selecter);
            }
            else
                selecter = selecters[selecters.FindIndex(x => x.Name == ("Checker" + index))];
            selecter.Visible = true;
            if (!selecter.Checked)
                selecter.Enabled = wallList.Count(x => x != null) == wallList.Length ? false : true;
        }

        private void hidePrevCheck(int index)
        {
            if (selecters.Any(x => x.Name == ("Checker" + index)))
                selecters[selecters.FindIndex(x => x.Name == ("Checker" + index))].Visible = false;
        }

        private void addWallToList(object sender, EventArgs e)
        {
            CheckBox checker = (CheckBox)sender;
            Bitmap image = (Bitmap)pctBoxWall.Image;
            if (checker.Checked)
            {
                int count = wallList.Count(x => x != null);
                if (count < wallList.Length)
                {
                    wallList[count] = image;
                }
            } else
            {
                wallList[Array.IndexOf(wallList, image)] = null;
            }
        }
        #endregion

        #region Utility
        private void CheckImagePath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images";
            fileName = path + "\\wallpaper_" + file_index + ".bmp";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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
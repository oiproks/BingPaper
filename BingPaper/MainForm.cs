﻿using BingPaper.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
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
        //https://bingdesktop.com/ Alternative with images from all the regions
        private string fileName = string.Empty;
        private int file_index = 0;
        private static List<Files> files;
        bool mouseDown = false;
        public static bool internet = false;
        Point lastLocation;
        MultiScreen multiScreen;
        Options options;
        Info info;
        LoadPast loadPast;
        public static PrivateFontCollection pfc;
        static Splash splash;
        static PictureBox myPctBoxWall;
        static Label mylblName;
        public static bool autostart = false;

        #region Preliminary Work
        public MainForm(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0 && args[0].Equals("--start-minimized"))
                WindowState = FormWindowState.Minimized;

            files = new List<Files>();
            mylblName = lblName;
            myPctBoxWall = pctBoxWall;

            Utilities.CheckImagePath();

            InitFont(Resources.Raleway_Light);
            InitFont(Resources.Raleway_Medium);

            splash = new Splash();
            Application.Run(splash);
            if (!internet)
                Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                autostart = true;
                if (Settings.Default.AUTOSTART)
                    Logger.WriteLog("Applying Wallpaper from Start-Up.");
                    if (!Settings.Default.MULTI)
                        btnSetWall_Click(sender, e);
                    else
                        btnSetMultitWall_Click(sender, e);
            } 
        }

        private void MainForm_Activate(object sender, EventArgs e)
        {
            if (autostart)
                Close();
            else
            {
                FormCollection fc = Application.OpenForms;
                if (fc.Count > 1)
                    foreach (Form frm in fc)
                    {
                        if (frm.Name.Equals("MultiScreen"))
                            multiScreen.Focus();
                        if (frm.Name.Equals("Info"))
                            info.Focus();
                        if (frm.Name.Equals("Options"))
                            options.Focus();
                        if (frm.Name.Equals("LoadPast"))
                            loadPast.Focus();
                    }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            elementOnPicture(btnSetWall);
            elementOnPicture(btnSetMulti);
            elementOnPicture(bingPaper2);
            if (Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images")).Count() >= 2)
            {
                btnPast.Visible = true;
                elementOnPicture(btnPast);
            }

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

        private void InitFont(byte[] font)
        {
            pfc = new PrivateFontCollection();
            int fontLength = font.Length;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(font, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
        }
        #endregion

        #region Bing Magic
        public static void DownloadJSON()
        {
            if (files.Count == 0)
            {
                string url = Resources.bing_json_url_1;
                var json = string.Empty;
                try
                {
                    json = new WebClient().DownloadString(url);
                    internet = true;
                    Logger.WriteLog("Searching new wallpapers");
                    JObject objects = JObject.Parse(json);
                    ListBuilder(ref files, objects);

                    url = Resources.bing_json_url_2;
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
                                myPctBoxWall.Image = file.bitmap;
                                mylblName.Text = file.name;
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("MainForm", ex);
                        internet = false;
                    }
                    files = files.OrderByDescending(o => o.date).ToList();
                    Logger.WriteLog("Downloaded " + files.Count.ToString() + " wallpapers.");
                }
                catch (WebException ex)
                {
                    Logger.WriteLog("Connection", ex);
                }
                splash.Close();
            }
        }

        private static void ListBuilder(ref List<Files> files, JObject objects)
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
            file_index = index > 0 ? index - 1 : files.Count() - 1;
            pctBoxWall.Image = files[file_index].bitmap;
            lblName.Text = files[file_index].name;
        }
        
        private void btnSetWall_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            bitmap = (Bitmap)pctBoxWall.Image;
            int index = files.FindIndex(f => f.bitmap == (Bitmap)pctBoxWall.Image);
            fileName = Utilities.PrepareFileName(true, bitmap, lblName.Text, files[index].date);

            string log = @"Applying wallpaper:"
                    + "\r\n\t- Image resolution: " + bitmap.Size.Width.ToString() + "x" + bitmap.Size.Height.ToString() + "\r\n\t"
                    + "- Image description: " + lblName.Text + "\r\n\t"
                    + "- Image date: " + DateTime.ParseExact(files.Find(x => x.bitmap == bitmap).date, "yyyyMMdd", null).ToString("yyyy-MM-dd");

            Logger.WriteLog(log);

            Utilities.SetWallpaper(0x0014, fileName, Utilities.Style.Fill);

            if (autostart)
                btnClose_Click(sender, e);

        }

        private void btnSetMultitWall_Click(object sender, EventArgs e)
        {
            multiScreen = new MultiScreen(this, files);
            multiScreen.Show();
        }

        private void btnLoadPast(object sender, EventArgs e)
        {
            loadPast = new LoadPast(this);
            loadPast.Show();
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            options = new Options(this, Screen.AllScreens.Count());
            options.Show();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            //TODO: open tab with info, link to git, paypal, etc...
            info = new Info(this);
            info.Show();
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
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class MultiScreen : Form
    {
        private List<Files> files;
        Bitmap[] wallList;
        string fileName = String.Empty;
        bool mouseDown = false;
        Point lastLocation;

        public MultiScreen()
        {
            InitializeComponent();

            wallList = new Bitmap[Screen.AllScreens.Count()];
        }

        #region Buttons
        private void btnApply_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            Utilities.CheckImagePath();
            if (wallList.Count(x => x != null) == wallList.Length)
            {
                bitmap = CreateMultiScreenWall();
                fileName = fileName.Substring(0, fileName.IndexOf("\\wallpaper_")) + "\\wallpaper_multi.bmp";
                bitmap.Save(fileName, ImageFormat.Bmp);
                Utilities.setWallpaper(20, fileName);
            }
            Array.Clear(wallList, 0, wallList.Length);
        }
        #endregion

        #region Set Multiple Wallpapers
        private Bitmap CreateMultiScreenWall()
        {
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

        private void addWallToList(object sender, EventArgs e)
        {
            CheckBox checker = (CheckBox)sender;
            Bitmap image = files[1].bitmap;
            if (checker.Checked)
            {
                int count = wallList.Count(x => x != null);
                if (count < wallList.Length)
                {
                    wallList[count] = image;
                }
            }
            else
            {
                wallList[Array.IndexOf(wallList, image)] = null;
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
        #endregion
    }
}

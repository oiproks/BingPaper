using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class MultiScreen : Form
    {
        private List<Files> files;
        Bitmap[] wallList;
        string fileName = string.Empty;
        bool mouseDown = false;
        Point lastLocation;
        int screenCount;
        Form main;
        Preview preview;

        #region Preliminary Work
        public MultiScreen(Form main, List<Files> files)
        {
            InitializeComponent();
            this.files = files;
            this.main = main;

            screenCount = Screen.AllScreens.Count();
            wallList = new Bitmap[screenCount];
        }

        private void MultiScreen_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is FlowLayoutPanel)
                    foreach (RadioButton rb in control.Controls)
                    {
                        int.TryParse(rb.Text[rb.Text.Length - 1].ToString(), out int num);
                        if (num > screenCount)
                            rb.Visible = false;
                        else
                            rb.CheckedChanged += addWallToList;
                    }
                if (control is PictureBox && control.Visible)
                {
                    int.TryParse(control.Name.Substring(9).ToString(), out int wallIndex);
                    PictureBox preview = (PictureBox)control;
                    preview.Image = files[wallIndex - 1].bitmap;
                    preview.BorderStyle = BorderStyle.None;
                }
                if (control is Label)
                {
                    int.TryParse(control.Name.Substring(5).ToString(), out int descriptionIndex);
                    Label description = (Label)control;
                    description.Text = files[descriptionIndex - 1].name;
                }
            }
        }
        #endregion

        #region Buttons
        private void btnApply_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            if (wallList.Count(x => x != null) == wallList.Length)
            {
                bitmap = CreateMultiScreenWall();
                fileName = Utilities.PrepareFileName(DateTime.Today.ToString("YYYY-MM-DD"));
                bitmap.Save(fileName, ImageFormat.Bmp);
                Utilities.SetWallpaper(20, fileName);
            }
            Array.Clear(wallList, 0, wallList.Length);

            btnClose_Click(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            main.Focus();
            Close();
        }

        private void addWallToList(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if (radio.Checked)
            {
                int.TryParse(radio.Text[radio.Text.Length - 1].ToString(), out int screenIndex);
                FlowLayoutPanel flp = radio.Parent as FlowLayoutPanel;
                int.TryParse(flp.Name.Substring(7).ToString(), out int imageIndex);
                Bitmap image = files[imageIndex - 1].bitmap;
            
                wallList[screenIndex - 1] = image;
                foreach (Control control in Controls)
                    if (control is FlowLayoutPanel && !control.Name.Equals(flp.Name))
                        foreach (RadioButton rb in control.Controls)
                        {
                            int.TryParse(rb.Text[rb.Text.Length - 1].ToString(), out int radioNum);
                            radioNum %= screenCount;
                            if (radioNum == screenIndex)
                                rb.Checked = false;
                        }
            }
        }
        #endregion

        #region Some Drawing Work
        private Bitmap CreateMultiScreenWall()
        {
            Bitmap finalImage = null;
            try
            {
                int width = 0;
                int height = 0;
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
        #endregion

        #region Preview
        private void ShowPreview(object sender, EventArgs e)
        {
            PictureBox image = (PictureBox)sender;
            preview = new Preview(image.Image, new Point(Cursor.Position.X + 5, Cursor.Position.Y + 5));
            preview.Show();
        }
        private void HidePreview(object sender, EventArgs e)
        {
            preview.Close();
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

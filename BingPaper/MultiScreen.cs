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
        List<ScreenAndWallpaper> screenList;
        string fileName = string.Empty, log = string.Empty;
        bool mouseDown = false;
        Point lastLocation;
        Form main;
        Preview preview; 

        #region Preliminary Work
        public MultiScreen(Form main, List<Files> files)
        {
            InitializeComponent();
            this.files = files;
            this.main = main;

            screenList = new List<ScreenAndWallpaper>();
            foreach (Screen screen in Screen.AllScreens)
            {
                screenList.Add(new ScreenAndWallpaper(screen, new Files()));
            }
            screenList = screenList.OrderBy(x => x.screen.Bounds.X).ToList();
        }

        private void MultiScreen_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is FlowLayoutPanel)
                    foreach (RadioButton rb in control.Controls)
                    {
                        int.TryParse(rb.Text[rb.Text.Length - 1].ToString(), out int num);
                        if (num > screenList.Count)
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
                control.Font = new Font(MainForm.pfc.Families[0], control.Font.Size);
            }

            if (MainForm.autostart)
            {
                for (int x = 0; x < screenList.Count; x++)
                    screenList[x].image = files[x];
                btnApply_Click(sender, e);
            }
        }
        #endregion

        #region Buttons
        private void btnApply_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            if (screenList.Count(x => x.image != null) == screenList.Count)
            {
                bitmap = CreateMultiScreenWall();
                fileName = Utilities.PrepareFileName(false, bitmap);
                Utilities.SetWallpaper(20, fileName, Utilities.Style.Tiled);
            }

            string log = @"Applying " + screenList.Count + " wallpapers:\r\n\t";
            foreach (ScreenAndWallpaper saw in screenList)
            {
                log += @"Number " + (screenList.FindIndex(x => x == saw) + 1).ToString() + "\r\n\t"
                    + "- Screen resolution: " + saw.screen.Bounds.Size.Width.ToString() + "x" + saw.screen.Bounds.Size.Height.ToString() + "\r\n\t"
                    + "- Image resolution: " + saw.image.bitmap.Size.Width.ToString() + "x" + saw.image.bitmap.Size.Height.ToString() + "\r\n\t"
                    + "- Image description: " + saw.image.name + "\r\n\t"
                    + "- Image date: " + DateTime.ParseExact(saw.image.date, "yyyyMMdd", null).ToString("yyyy-MM-dd") + "\r\n\t";
            }
            Logger.WriteLog(log);

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

                screenList[screenIndex - 1].image = files[imageIndex - 1];
                btnApply.Enabled = screenList.Count(x => x.image != null) == 2 ? true : false;
                foreach (Control control in Controls)
                    if (control is FlowLayoutPanel && !control.Name.Equals(flp.Name))
                        foreach (RadioButton rb in control.Controls)
                        {
                            int.TryParse(rb.Text[rb.Text.Length - 1].ToString(), out int radioNum);
                            radioNum %= screenList.Count;
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
                //foreach (Bitmap wall in wallList)
                //{
                //    width += wall.Width;
                //    height = wall.Height > height ? wall.Height : height;
                //}
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
                        Utilities.PrepareFileName(true, element.image.bitmap, element.image.name, element.image.date);
                        float ratioImage = (float)element.image.bitmap.Width / (float)element.image.bitmap.Height;
                        float ratioScreen = (float)element.screen.WorkingArea.Width / (float)element.screen.WorkingArea.Height;
                        if (ratioScreen > ratioImage)
                            g.DrawImage(element.image.bitmap, new Rectangle(element.screen.Bounds.X, 0, element.image.bitmap.Width, height));
                        else
                            g.DrawImage(element.image.bitmap, new Rectangle(element.screen.Bounds.X, 0, (int)(element.screen.WorkingArea.Height * ratioImage), element.screen.WorkingArea.Height));
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
            if (preview != null)
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class LoadPast : Form
    {
        Preview preview;
        private List<Files> files;
        List<ScreenAndWallpaper> screenList;
        Form main;

        public LoadPast(Form main)
        {
            InitializeComponent();

            this.main = main;

            files = new List<Files>();
            string[] imageFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images")).ToArray();
            foreach (string file in imageFiles)
                if (!file.Contains("multi"))
                {
                    string date = file.Substring(file.IndexOf("\\Images\\") + 8, 8);
                    DateTime oDate = DateTime.ParseExact(date, "yy_MM_dd", null);
                    files.Add(new Files("", "", oDate.ToString("yyyy MM dd"), new Bitmap(file)));
                }

            screenList = new List<ScreenAndWallpaper>();
            foreach (Screen screen in Screen.AllScreens)
            {
                screenList.Add(new ScreenAndWallpaper(screen, new Files()));
            }
            screenList = screenList.OrderBy(x => x.screen.Bounds.X).ToList();
        }

        private void LoadPast_Load(object sender, EventArgs e)
        {
            foreach(Files file in files)
            {
                //TODO: add element to flp
                Panel panel = new Panel
                {
                    Name = file.date,
                };
                panel.BackColor = Color.Transparent;

                PictureBox pictureBox = new PictureBox
                {
                    Image = file.bitmap
                };
                pictureBox.MouseHover += ShowPreview;
                pictureBox.MouseLeave += HidePreview;
                pictureBox.Size = new Size(120, 67);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.BackColor = Color.Transparent;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;

                Label label = new Label
                {
                    Text = file.date
                };
                label.AutoSize = true;
                label.Font = new Font(MainForm.pfc.Families[0], 9.75f, FontStyle.Bold);
                label.BackColor = Color.Transparent;
                label.ForeColor = ColorTranslator.FromHtml("#D4D0C8");

                FlowLayoutPanel flpRadio = new FlowLayoutPanel {
                    Name = file.date + "_flowRadio"
                };
                for( int x = 0; x < Screen.AllScreens.Count(); x++)
                {
                    RadioButton radio = new RadioButton {
                        Name = "screen_" + (x + 1),
                        Text = "Screen " + (x + 1)
                    };
                    radio.CheckedChanged += addWallToList;
                    radio.Font = new Font(MainForm.pfc.Families[0], 8.25f);
                    radio.BackColor = Color.Transparent;
                    radio.ForeColor = ColorTranslator.FromHtml("#D4D0C8");
                    flpRadio.Controls.Add(radio);
                }
                flpRadio.BackColor = Color.Transparent;

                switch (Screen.AllScreens.Count())
                {
                    case 1:
                        label.MaximumSize = new Size(110, 0);
                        flpRadio.Size = new Size(245, 24);
                        panel.Size = new Size(265, 73);
                        flpMain.Size = new Size(266, 350);
                        btnApply.Location = new Point(38, 370);
                        btnCancel.Location = new Point(131, 370);
                        Size = new Size(278, 405);
                        break;
                    case 2:
                        label.MaximumSize = new Size(190, 0);
                        flpRadio.Size = new Size(215, 24);
                        panel.Size = new Size(335, 73);
                        flpMain.Size = new Size(336, 350);
                        btnApply.Location = new Point(128, 370);
                        btnCancel.Location = new Point(221, 370);
                        Size = new Size(348, 405);
                        break;
                    case 3:
                        label.MaximumSize = new Size(270, 0);
                        flpRadio.Size = new Size(285, 24);
                        panel.Size = new Size(405, 73);
                        flpMain.Size = new Size(406, 350);
                        btnApply.Location = new Point(218, 370);
                        btnCancel.Location = new Point(311, 370);
                        Size = new Size(418, 405);
                        break;
                    case 4:
                        label.MaximumSize = new Size(350, 0);
                        flpRadio.Size = new Size(355, 24);
                        panel.Size = new Size(475, 73);
                        flpMain.Size = new Size(476, 350);
                        btnApply.Location = new Point(308, 370);
                        btnCancel.Location = new Point(401, 370);
                        Size = new Size(488, 405);
                        break;

                }

                pictureBox.Location = new Point(3,3);
                label.Location = new Point(126, 3);
                flpRadio.Location = new Point(126, 46);
                panel.Controls.Add(pictureBox);
                panel.Controls.Add(label);
                panel.Controls.Add(flpRadio);
                panel.Location = new Point(1, 1);
                flpMain.Controls.Add(panel);
            }
        }

        private void addWallToList(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if (radio.Checked)
            {
                int.TryParse(radio.Text[radio.Text.Length - 1].ToString(), out int screenIndex);
                FlowLayoutPanel flp = radio.Parent as FlowLayoutPanel;
                Panel panel = flp.Parent as Panel;
                foreach (Control control in panel.Controls)
                {
                    if (control is PictureBox pictureBox)
                        screenList[screenIndex - 1].image.bitmap = (Bitmap)pictureBox.Image;
                    if (control is FlowLayoutPanel flpRadio)
                        foreach(RadioButton screenRadio in flpRadio.Controls)
                        {
                            int.TryParse(screenRadio.Text[screenRadio.Text.Length - 1].ToString(), out int radioNum);
                            radioNum %= screenList.Count;
                            if (radioNum == screenIndex)
                                screenRadio.Checked = false;
                        }
                }
            }
        }

        #region Buttons
        private void btnApply_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            if (screenList.Count(x => x.image != null) == screenList.Count)
            {
                bitmap = Utilities.CreateMultiScreenWall(Name, screenList);
                string fileName = Utilities.PrepareFileName(false, bitmap);
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

        }
        #endregion
    }
}

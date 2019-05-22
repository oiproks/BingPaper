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
                    files.Add(new Files(file, "", oDate.ToString("yyyy MM dd"), new Bitmap(file)));
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
                    Image = file.bitmap,
                    Tag = file.url,
                    Name = file.date
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
                        label.MaximumSize = new Size(144, 0);
                        flpRadio.Size = new Size(144, 24);
                        panel.Size = new Size(268, 73);
                        flpMain.Size = new Size(294, 350);
                        btnApply.Location = new Point(120, 370);
                        btnCancel.Location = new Point(213, 370);
                        Size = new Size(300, 405);
                        break;
                    case 2:
                        label.MaximumSize = new Size(224, 0);
                        flpRadio.Size = new Size(224, 24);
                        panel.Size = new Size(348, 73);
                        flpMain.Size = new Size(374, 350);
                        btnApply.Location = new Point(200, 370);
                        btnCancel.Location = new Point(293, 370);
                        Size = new Size(380, 405);
                        break;
                    case 3:
                        label.MaximumSize = new Size(304, 0);
                        flpRadio.Size = new Size(304, 24);
                        panel.Size = new Size(428, 73);
                        flpMain.Size = new Size(454, 350);
                        btnApply.Location = new Point(280, 370);
                        btnCancel.Location = new Point(373, 370);
                        Size = new Size(460, 405);
                        break;
                    case 4:
                        label.MaximumSize = new Size(384, 0);
                        flpRadio.Size = new Size(384, 24);
                        panel.Size = new Size(508, 73);
                        flpMain.Size = new Size(534, 350);
                        btnApply.Location = new Point(360, 370);
                        btnCancel.Location = new Point(453, 370);
                        Size = new Size(540, 405);
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
                int.TryParse(radio.Name[radio.Name.Length - 1].ToString(), out int screenIndex);
                FlowLayoutPanel flp = radio.Parent as FlowLayoutPanel;
                Panel panel = flp.Parent as Panel;
                string panelName = panel.Name;
                foreach (Control control in panel.Controls)
                {
                    if (control is PictureBox pictureBox)
                    {
                        screenList[screenIndex - 1].image.bitmap = (Bitmap)pictureBox.Image;
                        screenList[screenIndex - 1].image.url = pictureBox.Tag.ToString();
                        screenList[screenIndex - 1].image.date = pictureBox.Name.ToString();
                        btnApply.Enabled = true;
                    }
                }
                // Uncheck the Radio with same screen in other Panels
                FlowLayoutPanel bigFLP = panel.Parent as FlowLayoutPanel;
                foreach (Panel control in bigFLP.Controls)
                {
                    if (panelName.Equals(control.Name))
                        continue;
                    foreach (Control panelControls in control.Controls)
                        if (panelControls is FlowLayoutPanel smallFLP)
                            foreach (RadioButton screenRadio in smallFLP.Controls)
                            {
                                int.TryParse(screenRadio.Name[screenRadio.Name.Length - 1].ToString(), out int radioNum);
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
            string log;
            int wallpapers = screenList.Count(x => x.image.bitmap != null);
            if (wallpapers == screenList.Count)
            {
                bitmap = Utilities.CreateMultiScreenWall(false, Name, screenList);
                string fileName = Utilities.PrepareFileName(false, bitmap);
                Utilities.SetWallpaper(20, fileName, Utilities.Style.Tiled);
                log = @"Applying " + wallpapers + " wallpapers:\r\n\t";
                foreach (ScreenAndWallpaper saw in screenList)
                {
                    if (saw.image.bitmap != null)
                        log += @"Number " + (screenList.FindIndex(x => x == saw) + 1).ToString() + "\r\n\t"
                            + "- Image from date: " + saw.image.date + ".\r\n\t";
                }
            } else
            {
                int index = screenList.FindIndex(x => x.image != null);
                string fileName = screenList[index].image.url;
                Utilities.SetWallpaper(0x0014, fileName, Utilities.Style.Fill);
                log = @"Applying wallpaper:"
                    + "\r\n\t- Image from date: " + screenList[index].image.date + ".";
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
            if (preview != null)
                preview.Close();
        }
        #endregion
    }
}

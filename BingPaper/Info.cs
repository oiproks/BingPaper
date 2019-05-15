using System;
using System.Drawing;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class Info : Form
    {
        bool mouseDown = false;
        Point lastLocation;
        Form main;

        public Info(Form main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Info_OnLoad(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
                control.Font = new Font(MainForm.pfc.Families[0], control.Font.Size);

            //TODO: organise the link, scrolling text, etc...
            //paypal.me/oiproks
        }

        private void Images_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            switch (control.Name)
            {
                case "pbInsta":
                    System.Diagnostics.Process.Start("https://www.instagram.com/oiproks.nemo/");
                    break;
                case "pbGit":
                    System.Diagnostics.Process.Start("https://github.com/oiproks/");
                    break;
                case "pbTwitter":
                    System.Diagnostics.Process.Start("https://twitter.com/oiproks");
                    break;
                case "pbCoffee":
                    System.Diagnostics.Process.Start("https://www.paypal.me/oiproks");
                    break;
            }
        }

        #region Form Commands
        private void btnClose_Click(object sender, EventArgs e)
        {
            main.Focus();
            Close();
        }

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

using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class Options : Form
    {
        Form main;
        int screens;
        bool mouseDown = false;
        Point lastLocation;

        public Options(Form form, int screens)
        {
            InitializeComponent();

            main = form;
            this.screens = screens;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            chkStartUp.Checked = Properties.Settings.Default.AUTOSTART;
        }

        private void ChkStartUp_CheckedChanged(object sender, EventArgs e)
        {
            if (screens > 1)
            {
                rbSingle.Enabled = true;
                rbMulti.Enabled = true;

                if (Properties.Settings.Default.MULTI)
                    rbMulti.Checked = true;
                else
                    rbSingle.Checked = true;
            }
        }

        private void SetStartup(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chkStartUp.Checked)
                rk.SetValue("BingPaper /minimized", Application.ExecutablePath + " --start-minimized");
            else
                rk.DeleteValue("BingPaper", false);

            Properties.Settings.Default.AUTOSTART = chkStartUp.Checked;
            Properties.Settings.Default.MULTI = rbMulti.Checked;
            Properties.Settings.Default.Save();

            btnClose_Click(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            main.Focus();
            Close();
        }

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
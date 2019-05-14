using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Drawing;
using System.Security.Principal;
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
            if (chkStartUp.Checked)
                SetScheduler("BingPaper");
            else
                GetAndDeleteTask("BingPaper");
            Properties.Settings.Default.AUTOSTART = chkStartUp.Checked;
            Properties.Settings.Default.MULTI = rbMulti.Checked;
            Properties.Settings.Default.Save();

            btnClose_Click(sender, e);
        }

        private void SetScheduler(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Sets a new wallpaper every day.";

                LogonTrigger lTrigger = td.Triggers.Add(new LogonTrigger());
                lTrigger.Delay = TimeSpan.FromSeconds(20);

                td.Actions.Add(new ExecAction(Application.ExecutablePath, "--start-minimized", null));

                td.Settings.RunOnlyIfNetworkAvailable = true;
                td.Settings.Enabled = true;
                td.Settings.StartWhenAvailable = true;
                td.Settings.DisallowStartIfOnBatteries = false;

                td.Principal.RunLevel = TaskRunLevel.Highest;

                ts.RootFolder.RegisterTaskDefinition(taskName, td);
            }
        }

        void GetAndDeleteTask(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                Task td = ts.GetTask(taskName);

                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                    throw new Exception($"Cannot delete task with your current identity '{identity.Name}' permissions level." +
                    "You likely need to run this application 'as administrator' even if you are using an administrator account.");

                ts.RootFolder.DeleteTask(taskName);
            }
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
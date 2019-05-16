using System;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class Splash : Form
    {

        Timer t1 = new Timer();

        public Splash()
        {
            InitializeComponent();

            Opacity = 0;

            t1.Interval = 20;
            t1.Tick += new EventHandler(fadeIn);
            t1.Start();
        }

        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity == 1)
            {
                t1.Stop();
                MainForm.DownloadJSON();
            }
            else
                Opacity += 0.05;
        }

        private void Splash_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            t1.Tick -= fadeIn;
            t1.Tick += new EventHandler(fadeOut);
            t1.Start();

            if (Opacity == 0)
                e.Cancel = false;
        }

        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity == 0)
            {
                t1.Stop();
                Close();
            }
            else
                Opacity -= 0.05;
        }
    }
}

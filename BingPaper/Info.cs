using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //TODO: organise the link, scrolling text, etc...
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

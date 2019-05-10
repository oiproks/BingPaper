using System;
using System.Drawing;
using System.Windows.Forms;

namespace BingPaper
{
    public partial class Preview : Form
    {
        Image image;

        public Preview(Image image, Point location)
        {
            InitializeComponent();
            this.image = image;
            Location = location;
        }

        private void Preview_OnLoad(object sender, EventArgs e)
        {
            pbPreview.Image = image;
        }
    }
}

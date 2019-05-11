using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingPaper
{
    class ScreenAndWallpaper
    {
        public Screen screen;
        public Files image;

        public ScreenAndWallpaper() { }

        public ScreenAndWallpaper(Screen screen, Files image)
        {
            this.screen = screen;
            this.image = image;
        }
    }
}

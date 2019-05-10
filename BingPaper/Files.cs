using System.Drawing;

namespace BingPaper
{
    public class Files
    {
        public string url { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public Bitmap bitmap { get; set; }

        public  Files (){}

        public Files (string url, string name, string date, Bitmap bitmap)
        {
            this.url = url;
            this.name = name;
            this.date = date;
            this.bitmap = bitmap;
        }

    }
}

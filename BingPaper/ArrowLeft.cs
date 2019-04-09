using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BingPaper
{
    public class ArrowLeft : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            //grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            grPath.AddPolygon(new System.Drawing.Point[3] { new System.Drawing.Point(0, ClientSize.Height/2), new System.Drawing.Point(ClientSize.Width, 0), new System.Drawing.Point(ClientSize.Width, ClientSize.Height) });
            Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
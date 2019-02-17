using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace NJAuction
{
    public partial class Meso : Form
    {
        [DllImport("user32.dll")]

        public static extern int FindWindow(string lpClassName, string lpWindowName);
        
        [DllImport("user32.dll", SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        
        [DllImport("user32.dll", SetLastError = true)]

        static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);
        
        [DllImport("gdi32.dll")]

        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        public Meso()
        {
            InitializeComponent();
        }

        public IntPtr Client_Connection(string clientname)
        {
            IntPtr iResulthwnd = new IntPtr();
            int inthwnd = FindWindow(null, clientname);
            if (inthwnd == 0)
            {
                iResulthwnd = new IntPtr(inthwnd);
            }
            else
            {
                iResulthwnd = new IntPtr(inthwnd);
            }
            return iResulthwnd;
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
             IntPtr Clienthwnd = Client_Connection("MapleStory");

             if (Clienthwnd.Equals(0))
             {
                 MessageBox.Show("클라이언트 검색 실패");
                 return;
             }

             Bitmap res = PrintWindow(Clienthwnd);
             PB1.Image = res;
        }
        
        private static Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            Bitmap nb = new Bitmap(cropArea.Width, cropArea.Height);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(img, -cropArea.X, -cropArea.Y);
            return nb;
        }

        public static Bitmap PrintWindow(IntPtr hwnd)
        {

            Rectangle rc = Rectangle.Empty;

            Graphics gfxWin = Graphics.FromHwnd(hwnd);

            rc = Rectangle.Round(gfxWin.VisibleClipBounds);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);

            Graphics gfxBmp = Graphics.FromImage(bmp);

            IntPtr hdcBitmap = gfxBmp.GetHdc();

            bool succeeded = PrintWindow(hwnd, hdcBitmap, 1);

            gfxBmp.ReleaseHdc(hdcBitmap);

            if (!succeeded)
            {
                gfxBmp.FillRectangle(
                    new SolidBrush(Color.Gray),
                    new Rectangle(Point.Empty, bmp.Size));
            }

            IntPtr hRgn = CreateRectRgn(0, 0, 0, 0);
            GetWindowRgn(hwnd, hRgn);
            Region region = Region.FromHrgn(hRgn);
            if (!region.IsEmpty(gfxBmp))
            {
                gfxBmp.ExcludeClip(region);
                gfxBmp.Clear(Color.Transparent);
            }

            gfxBmp.Dispose();            

            Bitmap nb = cropImage(bmp, new Rectangle(850, 50, 100, 20));

            return nb;
        }
    }
}

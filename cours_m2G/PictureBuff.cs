using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace cours_m2G
{
    static class PictureBuff
    {
        public static int[] rgb;
        public static Semaphore sem;
        static Graphics g;
        public static Bitmap bmp;
        static Size screen;
        static Refresher r;
        static bool filled;
       public static object locker = new();
        static public RenderType Creator { get; set; } = RenderType.NOCUTTER;
        public static bool Filled { get { return filled; } set { filled = value; if (value) r.Invoke();} } 
        public static void Init(int W, int H,Refresher r)
        {
            
          
            PictureBuff.screen = new Size(W, H);
            bmp = new Bitmap(screen.Width, screen.Height);
            g = Graphics.FromImage(bmp);
            rgb = new int[screen.Width * screen.Height];
            for (int x = 0; x < screen.Width; x++)
                for (int y = 0; y < screen.Height; y++)
                    rgb[x+ y*screen.Width] = -1;
            PictureBuff.r = r;
            filled = false;
        
        }

        public static void SetPixel(int x, int y, int color)
        {
            rgb[x + y * screen.Width] = color;
        }
        public static void SetLine(int x1, int y1, int x2, int y2,Color color)
        {
            Pen pen = new Pen(color, 4);
            lock (locker)
                try
                {
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
                catch { }

        }
        public static void SetPoint(MatrixCoord3D p1, int HitRadius,Color color)
        {
            Pen pen = new Pen(color);
            lock (locker)
                g.DrawEllipse(pen, (int)(p1.X - HitRadius), (int)(p1.Y - HitRadius), HitRadius * 2, HitRadius * 2);
           
        }

        public static void SetText(MatrixCoord3D p1, string s, string s1)
        {
            g.DrawString(s, new Font("Arial", 8), new SolidBrush(Color.Black), (int)p1.X, (int)p1.Y);
            g.DrawString(s1, new Font("Arial", 8), new SolidBrush(Color.Blue), (int)p1.X, (int)p1.Y + 11);

          
        }


        public static Bitmap GetBitmap()
        {
            
            if (Creator != RenderType.NOCUTTER)
            {
                for (int x = 0; x < screen.Width; x++)
                    for (int y = 0; y < screen.Height; y++)
                        bmp.SetPixel(x, y, Color.FromArgb(rgb[x + y * screen.Width])); 
            }
            return bmp;
        }
        public static void Clear()
        {
            if (Creator != RenderType.NOCUTTER)
            {
                rgb = new int[screen.Width* screen.Height];
                for (int x = 0; x < screen.Width; x++)
                    for (int y = 0; y < screen.Height; y++)
                        rgb[x + y * screen.Width] = -1;
            }
            else
            {
               g.Clear(Color.White);
            }
        }
    }
}

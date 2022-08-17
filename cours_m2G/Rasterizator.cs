using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;

using System.Drawing.Imaging;
using System.Linq;

namespace cours_m2G
{
   static class Rasterizator
    {
        public static List<double> Interpolate(double i0, double d0, double i1, double d1)
        {
            if (i0 == i1)
            {
                return new List<double> { d0 };
            }
            List<double> values = new List<double>();
            double a = (d1 - d0) / (i1 - i0);
            double d = d0;
            for (double i = i0; i <= i1; i++) {
                values.Add(d);
                d = d + a;
            }
            return values;
        }

        public static void DrawLine(PointComponent P0, PointComponent P1, Graphics g)
        {
            if (Math.Abs(P1.X - P0.X) > Math.Abs(P1.Y - P0.Y))
            {
                // Прямая ближе к горизонтальной
                // Проверяем, что x0 < x1
                if (P0.X > P1.X)
                {
                    PointComponent P = P0;
                    P0 = P1;
                    P1 = P;
                }
                List<double> ys = Interpolate(P0.X, P0.Y, P1.X, P1.Y);
                for (double x = P0.X; x < P1.X; x++)
                {
                    g.DrawRectangle(new Pen(Color.Black,2), (int)x, (int)ys[Convert.ToInt32(x - P0.X)], 1, 1);
                }
            }
            else
            {
                //Прямая ближе к вертикальной
                //Проверяем, что y0 < y1
                if (P0.Y > P1.Y) {
                    PointComponent P = P0;
                    P0 = P1;
                    P1 = P;
                }
                List<double> xs = Interpolate(P0.Y, P0.X, P1.Y, P1.X);
                for (double y = P0.Y; y < P1.Y; y++)
                {
                    g.DrawRectangle(new Pen(Color.Black,2), (int)xs[Convert.ToInt32(y - P0.Y)], (int)y, 1, 1);

                }
            }
        }
    }


    class R1
    {
        ZBuffer zBuffer;
        public PaintEventArgs e;
        Bitmap bmp;
        Size s;
        bool drawFill;
        public Bitmap GetResult() { this.zBuffer.Down(); Bitmap b = bmp; bmp = new Bitmap(s.Width, s.Height); return b; }
        public R1(Bitmap bmp, Size windowSize, bool drawFill, PaintEventArgs e)
        {
            s = windowSize;
            this.bmp = bmp;
            zBuffer = new ZBuffer(windowSize);
            this.drawFill = drawFill;
            this.e = e;
        }

        public void drawTriangleFill(List<PointComponent> vertices, Color color)
        {
            var points = new List<PointComponent> { vertices[0], vertices[1], vertices[2] };

            foreach (var p in Fill.FillTriangle(points))
                drawPoint(p, color);
        }

        public void drawLine(List<PointComponent> vertices, Color color)
        {
            List<PointComponent> pp = Line.GetPoints(vertices[0], vertices[1]);
            foreach (var p in pp)
                drawPoint(p, color);
        }

        void drawPoint(PointComponent point, Color color)
        {
            var p2D = (PointComponent)point;

            if (zBuffer[point.X, point.Y] <= point.Z-1)
                return;

            zBuffer[point.X, point.Y] = point.Z;
          //  e.Graphics.DrawRectangle(new Pen(color, 2), (int)p2D.X, (int)p2D.Y, 1, 1);
          if(color!=Color.White)
         bmp.SetPixelFast((int)p2D.X, (int)p2D.Y, color);
        }
    }

     class ZBuffer
    {
        // minVal is at the top of the stack
        double[,] zBufferMap;
        Size w;

        public ZBuffer(Size windowSize)
        {
            w = windowSize;
            zBufferMap = new double[windowSize.Width, windowSize.Height];
            for (int i = 0; i < windowSize.Width * windowSize.Height; i++)
                zBufferMap[i % windowSize.Width, Ut.F(i / windowSize.Width)] = double.MaxValue;
        }
        public void Down()
        {
            zBufferMap = new double[w.Width, w.Height];
            for (int i = 0; i < w.Width * w.Height; i++)
                zBufferMap[i % w.Width, Ut.F(i / w.Width)] = double.MaxValue;
        }
        // get: boolean if z is higher or equal to point (=> true = can draw)
        // set: z at point
        public double this[double x, double y]
        {
            get
            {
                x = Ut.F(x) ;
                y = Ut.F(y);
                if (x > w.Width - 1 || x < 0 || y < 0 || y > w.Height - 1) return double.MinValue;
                return zBufferMap[Ut.F(x), Ut.F(y)];
            }
            set
            {
                zBufferMap[Ut.F(x), Ut.F(y) ] = (double)value;
            }
        }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();

        //    for (int i = 0; i < w.Width; i++)
        //    {
        //        for (int j = 0; j < w.Height; j++)
        //        {
        //            if (zBufferMap[i, j] == double.MinValue)
        //                sb.Append(",");
        //            else
        //                sb.Append(zBufferMap[i, j]);
        //            sb.Append(" ");
        //        }
        //        sb.AppendLine();
        //    }

        //    return sb.ToString();
        //}
    }
     static class Line
    {
        // Bresenham's Line in 3D
        // Source https://www.geeksforgeeks.org/bresenhams-algorithm-for-3-d-line-drawing/
        public static List<PointComponent> GetPoints(PointComponent p1, PointComponent p2)
        {
            double dE = 1;

            List<PointComponent> points = new List<PointComponent>() { p1 };

            double dx = Math.Abs(p2.X - p1.X);
            double dy = Math.Abs(p2.Y - p1.Y);
            double dz = Math.Abs(p2.Z - p1.Z);

            double xs = p2.X > p1.X ? 1 : -1;
            double ys = p2.Y > p1.Y ? 1 : -1;
            double zs = p2.Z > p1.Z ? 1 : -1;

            double
                d1,
                d2;
            double
                x1 = p1.X,
                y1 = p1.Y,
                z1 = p1.Z;

            if (dx >= dy && dx >= dz)
            {
                d1 = 2 * dy - dx;
                d2 = 2 * dz - dx;
                while (Math.Abs(Ut.F(x1) - Ut.F(p2.X)) > dE)
                {
                    x1 += xs;
                    if (d1 >= 0)
                    {
                        y1 += ys;
                        d1 -= 2 * dx;
                    }
                    if (d2 >= 0)
                    {
                        z1 += zs;
                        d2 -= 2 * dx;
                    }
                    d1 += 2 * dy;
                    d2 += 2 * dz;
                    points.Add(new PointComponent(x1, y1, z1));
                }
            }

            else if (dy >= dx && dy >= dz)
            {
                d1 = 2 * dx - dy;
                d2 = 2 * dz - dy;
                while (Math.Abs(Ut.F(y1) - Ut.F(p2.Y)) > dE)
                {
                    y1 += ys;
                    if (d1 >= 0)
                    {
                        x1 += xs;
                        d1 -= 2 * dy;
                    }
                    if (d2 >= 0)
                    {
                        z1 += zs;
                        d2 -= 2 * dy;
                    }
                    d1 += 2 * dx;
                    d2 += 2 * dz;
                    points.Add(new PointComponent(x1, y1, z1));
                }
            }

            else
            {
                d1 = 2 * dy - dz;
                d2 = 2 * dz - dz;
                while (Math.Abs(Ut.F(z1) - Ut.F(p2.Z)) > dE)
                {
                    z1 += zs;
                    if (d1 >= 0)
                    {
                        y1 += ys;
                        d1 -= 2 * dz;
                    }
                    if (d2 >= 0)
                    {
                        x1 += xs;
                        d2 -= 2 * dz;
                    }
                    d1 += 2 * dy;
                    d2 += 2 * dx;
                    points.Add(new PointComponent(x1, y1, z1));
                }
            }

            points.Add(p2);

            return points;
        }

        public static List<PointComponent> OutlineTriangle(List<PointComponent> vertices)
        {
            var points = new List<PointComponent>();

            var p1 = vertices[0];
            var p2 = vertices[1];
            var p3 = vertices[2];

            // Sorting the points in order to always have this order on screen p1, p2 & p3
            // with p1 always up (thus having the Y the lowest possible to be near the top screen)
            // then p2 between p1 & p3
            if (p1.Y > p2.Y)
            {
                var temp = p2;
                p2 = p1;
                p1 = temp;
            }

            if (p2.Y > p3.Y)
            {
                var temp = p2;
                p2 = p3;
                p3 = temp;
            }

            if (p1.Y > p2.Y)
            {
                var temp = p2;
                p2 = p1;
                p1 = temp;
            }

            // inverse slopes
            double dP1P2, dP1P3;

            // http://en.wikipedia.org/wiki/Slope
            // Computing inverse slopes
            if (p2.Y - p1.Y > 0)
                dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
            else
                dP1P2 = 0;

            if (p3.Y - p1.Y > 0)
                dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
            else
                dP1P3 = 0;

            // First case where triangles are like that:
            // P1
            // -
            // -- 
            // - -
            // -  -
            // -   - P2
            // -  -
            // - -
            // -
            // P3
            if (dP1P2 > dP1P3)
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    if (y < p2.Y)
                    {
                        points.AddRange(ProcessScanLine(y, p1, p3, p1, p2));
                    }
                    else
                    {
                        points.AddRange(ProcessScanLine(y, p1, p3, p2, p3));
                    }
                }
            }
            // First case where triangles are like that:
            //       P1
            //        -
            //       -- 
            //      - -
            //     -  -
            // P2 -   - 
            //     -  -
            //      - -
            //        -
            //       P3
            else
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    if (y < p2.Y)
                    {
                        points.AddRange(ProcessScanLine(y, p1, p2, p1, p3));
                    }
                    else
                    {
                        points.AddRange(ProcessScanLine(y, p2, p3, p1, p3));
                    }
                }
            }

            return points;
        }

        static double Clamp(double value, double min = 0, double max = 1)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        static double Interpolate(double min, double max, double gradient)
        {
            return min + (max - min) * Clamp(gradient);
        }

        static List<PointComponent> ProcessScanLine(int y, PointComponent pa, PointComponent pb, PointComponent pc, PointComponent pd)
        {
            var points = new List<PointComponent>();

            var gradient1 = pa.Y != pb.Y ? (y - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (y - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)Interpolate(pc.X, pd.X, gradient2);

            double z1 = Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = Interpolate(pc.Z, pd.Z, gradient2);

            var x = sx;

            float gradient = (x - sx) / (float)(ex - sx);

            var z = Interpolate(z1, z2, gradient);
            points.Add(new PointComponent(x, y, z));

            for (x = sx + 1; x < ex; x++)
            {
                gradient = (x - sx) / (float)(ex - sx);

                z = Interpolate(z1, z2, gradient);
            }

            points.Add(new PointComponent(x, y, z));

            return points;
        }
    }
    static class Fill
    // Source https://www.davrous.com/2013/06/21/tutorial-part-4-learning-how-to-write-a-3d-software-engine-in-c-ts-or-js-rasterization-z-buffering/
    {
        public static IEnumerable<PointComponent> FillTriangle(List<PointComponent> vertices)
        {
            vertices.Sort((x, y) => Ut.F(x.Y - y.Y));

            var p1 = vertices[0];
            var p2 = vertices[1];
            var p3 = vertices[2];

            double dP1P2, dP1P3;

            if (p2.Y - p1.Y > 0)
                dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
            else
                dP1P2 = 0;

            if (p3.Y - p1.Y > 0)
                dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
            else
                dP1P3 = 0;

            if (dP1P2 > dP1P3)
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    if (y < p2.Y)
                        foreach (var p in ProcessScanLine(y, p1, p3, p1, p2))
                            yield return p;
                    else
                        foreach (var p in ProcessScanLine(y, p1, p3, p2, p3))
                            yield return p;
                }
            }
            else
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    if (y < p2.Y)
                        foreach (var p in ProcessScanLine(y, p1, p2, p1, p3))
                            yield return p;
                    else
                        foreach (var p in ProcessScanLine(y, p2, p3, p1, p3))
                            yield return p;
                }
            }
        }

        static double Clamp(double value, double min = 0, double max = 1)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        static double Interpolate(double min, double max, double gradient)
        {
            return min + (max - min) * Clamp(gradient);
        }

        static IEnumerable<PointComponent> ProcessScanLine(int y, PointComponent pa, PointComponent pb, PointComponent pc, PointComponent pd)
        {
            var gradient1 = pa.Y != pb.Y ? (y - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (y - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)Interpolate(pc.X, pd.X, gradient2);

            double z1 = Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = Interpolate(pc.Z, pd.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                float gradient = (x - sx) / (float)(ex - sx);

                var z = Interpolate(z1, z2, gradient);
                yield return new PointComponent(x, y, z);
            }

            for (var x = ex; x < sx; x++)
            {
                float gradient = (x - sx) / (float)(ex - sx);

                var z = Interpolate(z1, z2, gradient);
                yield return new PointComponent(x, y, z);
            }
        }
    }
    static class Ut
    {
        public static int F(double a)
        {
            return (int)Math.Floor(a);
        }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self.Select((item, index) => (item, index));
    }

    public static class BitmapExtension
    {
        public static void SetPixelFast(this Bitmap bmp, int x, int y, Color color)
        {
            var newValues = new byte[] { color.B, color.G, color.R, 255 };

            BitmapData data = bmp.LockBits(
                new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb
                );

            if (
                data.Stride * y + 4 * x < data.Stride * data.Height
                && data.Stride * y + 4 * x >= 0
                && x * 4 < data.Stride
                && y < data.Height
                && x > 0
                )
                unsafe
                {
                    byte* ptr = (byte*)data.Scan0;

                    for (int i = 0; i < 4; i++)
                        ptr[data.Stride * y + 4 * x + i] = newValues[i];
                }

            bmp.UnlockBits(data);
        }
    }

}

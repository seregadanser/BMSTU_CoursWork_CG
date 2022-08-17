using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

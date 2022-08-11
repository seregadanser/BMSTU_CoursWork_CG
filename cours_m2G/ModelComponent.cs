using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    interface ModelComponent : IObjects
    {
       
        public bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir);
    }

    class PointComponent : ModelComponent
    {
        private MatrixCoord3D coords;
        private double hit_radius = 10;
        public double HitRadius { get { return hit_radius; } set { hit_radius = value; } }
        public MatrixCoord3D Coords { get { return coords; } set { coords = value; } }
        public double X { get { return coords.X; } }
        public double Y { get { return coords.Y; } }
        public double Z { get { return coords.Z; } }

        public Color Color { get; set; } = Color.Black;

        public PointComponent(MatrixCoord3D coords)
        {
            this.coords = coords;
            
        }
        public PointComponent(double x, double y, double z)
        {
            coords = new MatrixCoord3D(x, y, z);
        }

        public void action(IVisitor visitor)
        {
            visitor.visit(this);
        }
        //public bool IsGet(MatrixCoord3D mouse) 
        //{
        //    if (Math.Pow(mouse.X - coords.X,2)+Math.Pow(mouse.Y - coords.Y, 2)<=Math.Pow(hit_radius, 2))
        //        return true;
        //    return false;
        //}

        public bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir)

        {

            //a == 1; // because rdir must be normalized

            MatrixCoord3D k = ray_pos - coords;

            double b = MatrixCoord3D.scalar(k, ray_dir);

            double c = MatrixCoord3D.scalar(k, k) - hit_radius * hit_radius;

            double d = b * b - c;



            if (d >= 0)

            {

                //float sqrtfd = sqrtf(d);

                //// t, a == 1

                //float t1 = -b + sqrtfd;

                //float t2 = -b - sqrtfd;



                //float min_t = min(t1, t2);

                //float max_t = max(t1, t2);



                //float t = (min_t >= 0) ? min_t : max_t;

                //tResult = t;

                //return (t > 0);
                return true;

            }
            return false;

        }
        public bool IsGet(PointComponent mouse)
        {
            if (Math.Pow(mouse.X - coords.X, 2) + Math.Pow(mouse.Y - coords.Y, 2) <= Math.Pow(hit_radius, 2))
                return true;
            return false;
        }
        public static PointComponent operator *(PointComponent point, MatrixTransformation3D transform)
        {
            return new PointComponent(point.Coords * transform);
        }

        public static bool operator ==(PointComponent p1, PointComponent p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z)
                return true;
            return false;
        }

        public static bool operator !=(PointComponent p1, PointComponent p2)
        {
            if (p1.X != p2.X || p1.Y != p2.Y || p1.Z != p2.Z)
                return true;
            return false;
        }
    }

    class LineComponent : ModelComponent
    {
        Color color = Color.Black;
        public Color Color
        {
            get
            { return color; }
            set
            {
                color = value;
                point1.Color = value;
                point2.Color = value;
            }
        }
        private PointComponent point1, point2;
        public PointComponent Point1 { get { return point1; } set { point1.Coords = value.Coords; } }
        public PointComponent Point2 { get { return point2; } set { point2.Coords = value.Coords; } }
        public LineComponent(PointComponent point1, PointComponent point2)
        {
            this.point1 = point1;
            this.point2 = point2;
      
        }

        public void action(IVisitor visitor)
        {
            //point1.action(visitor);
            //point2.action(visitor);
            visitor.visit(this);
        }
        public double Len()
        {
            MatrixCoord3D diff = point1.Coords - point2.Coords;
            return Math.Sqrt(diff.X*diff.X + diff.Y*diff.Y + diff.Z*diff.Z);
        }
        public bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir)
        {
            return false;
        }
        public static LineComponent operator *(LineComponent line, MatrixTransformation3D transform)
        {
            return new LineComponent(line.point1 * transform, line.point2*transform);
        }

        public static bool operator ==(LineComponent p1, LineComponent p2)
        {
            if (p1.point1 == p2.point1 && p1.point2 == p2.point2)
                return true;
            return false;
        }

        public static bool operator !=(LineComponent p1, LineComponent p2)
        {
            if (p1.point1 != p2.point1 || p1.point2 != p2.point2)
                return true;
            return false;
        }
    }

    class PolygonComponent : ModelComponent
    {
        Color color = Color.Black;
        public Color Color
        {
            get
            { return color; }
            set
            {
                foreach (LineComponent l in lines)
                {
                    l.Color = value;
                }
            }
        }

        private List<PointComponent> points;
        private List<LineComponent> lines;

        public int NumberOfLines { get { return lines.Count; } }
       
        public List<PointComponent> Points { get { return points; } set { points = value; }}
        public List<LineComponent> Lines { get { return lines; } set { lines = value; } }
        public PolygonComponent ()
        {
            points = new List<PointComponent>();
            lines = new List<LineComponent>();
        }
        public PolygonComponent(params LineComponent[] inlines)
        {
            points = new List<PointComponent>();
            lines = new List<LineComponent>();
      
            for (int i = 0; i < inlines.Length; i++)
                AddLine(inlines[i]);
        }

        public void AddLine(LineComponent line)
        {
            bool inlist = true;
            foreach(LineComponent l in lines)
            {
                if (l == line)
                    inlist = false;
            }
            if(inlist)
            {
                lines.Add(line);

                bool pi1 = false, pi2 = false;
                foreach (PointComponent p in points)
                {
                    if (p == line.Point1)
                        pi1 = true;
                    if (p == line.Point2)
                        pi2 = true;
                }
                if (!pi1)
                    points.Add(line.Point1);
                if (!pi2)
                    points.Add(line.Point2);
            }
        }

        public void DelitLine(LineComponent line)
        {
            points.Remove(line.Point1);
            points.Remove(line.Point2);
            lines.Remove(line);

        }

        public void action(IVisitor visitor)
        {
           visitor.visit(this);
        }

        public bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir)
        {
            return false;
        }
    }
}

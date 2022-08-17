using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    abstract class ModelComponent : IObjects
    {
        public abstract Color Color { get; set; }
        protected Color color = Color.Black;
        protected Id id;
        public abstract Id Id { get; set; }
        public abstract bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir);
        public abstract void action(IVisitor visitor);
    }

    class PointComponent : ModelComponent
    {
        public override Id Id { get { return id; } set { id = value; } }
        private MatrixCoord3D coords;
        private double hit_radius = 10;
        public double HitRadius { get { return hit_radius; } set { hit_radius = value; } }
        public MatrixCoord3D Coords { get { return coords; } set { coords = value; } }
        public double X { get { return coords.X; } }
        public double Y { get { return coords.Y; } }
        public double Z { get { return coords.Z; } }

        public override Color Color { get { return color; } set { color = value; } } 

        public PointComponent(MatrixCoord3D coords)
        {
            this.coords = coords;
            id = new Id("Point", "-1");
        }
        public PointComponent(double x, double y, double z)
        {
            coords = new MatrixCoord3D(x, y, z);
            id = new Id("Point", "-1");
        }
        public PointComponent(double x, double y, double z, Id id ) : this(x, y, z)
        {
            this.id = id;
        }
        public PointComponent(MatrixCoord3D coords, Id id) : this(coords)
        {
            this.id = id;
        }
        public override void action(IVisitor visitor)
        {
            visitor.visit(this);
        }
 

        public override bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir)

        {

            //a == 1; // because rdir must be normalized

            MatrixCoord3D k = ray_pos - coords;

            double b = MatrixCoord3D.scalar(k, ray_dir);

            double c = MatrixCoord3D.scalar(k, k) - hit_radius * hit_radius;

            double d = b * b - c;



            if (d >= 0)

            {
                return true;

            }
            return false;

        }
      

        public static PointComponent operator *(PointComponent point, MatrixTransformation3D transform)
        {
            return new PointComponent(point.Coords * transform);
        }

        public static bool operator ==(PointComponent p1, PointComponent p2)
        {
            if (p1.id == p2.id)
                return true;
            return false;
        }

        public static bool operator !=(PointComponent p1, PointComponent p2)
        {
            if (p1.id!=p2.id)
                return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            PointComponent p = (PointComponent)obj;
            if (p == this)
                return true;
            return false;
        }
    }

    class LineComponent : ModelComponent
    {
        public override Id Id { get { return id; } set { id = value; } }
      
        public override Color Color
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
            string iddesc = "";
            if (Convert.ToInt32(point1.Id.Description) < Convert.ToInt32(point2.Id.Description))
                iddesc = point1.Id.Description + point2.Id.Description;
            else
                iddesc = point2.Id.Description + point1.Id.Description;
            id = new Id("Line", iddesc);
        }
        public LineComponent(PointComponent point1, PointComponent point2, Id id) : this(point1, point2)
        {
            this.id = id;
        }

        public override void action(IVisitor visitor)
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
        public override bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir)
        {
            return false;
        }

        public void ReplacePoint(PointComponent whatline, PointComponent whichline)
        {
            if (point1 == whatline)
                point1 = whichline;
            if (point2 == whatline)
                point2 = whichline;
        }


        public static LineComponent operator *(LineComponent line, MatrixTransformation3D transform)
        {
            return new LineComponent(line.point1 * transform, line.point2*transform);
        }

        public static bool operator ==(LineComponent p1, LineComponent p2)
        {
            if (p1.id == p2.id)
                return true;
            return false;
        }

        public static bool operator !=(LineComponent p1, LineComponent p2)
        {
            if (p1.id != p2.id)
                return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            LineComponent p = (LineComponent)obj;
            if (p == this)
                return true;
            return false;
        }
    }

    class PolygonComponent : ModelComponent
    {
        public override Id Id { get { return id; } set { id = value; } }
       
        public override Color Color
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

        private PointComponent[] points;
        private LineComponent[] lines;
        public PointComponent[] Points { get { return points; } set { points = value; } }
        public LineComponent[] Lines { get { return lines; } set { lines = value; } }


        public PolygonComponent(PointComponent p1, PointComponent p2, PointComponent p3)
        {
            color = Color.White;
            points = new PointComponent[3];
            lines = new LineComponent[3];
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;

            lines[0] = new LineComponent(p1, p2);
            lines[1] = new LineComponent(p2, p3);
            lines[2] = new LineComponent(p3, p1);

            string iddesc = "";

            if (Convert.ToInt32(p1.Id.Description) < Convert.ToInt32(p2.Id.Description) && Convert.ToInt32(p1.Id.Description) < Convert.ToInt32(p3.Id.Description))
            {
                if (Convert.ToInt32(p2.Id.Description) < Convert.ToInt32(p3.Id.Description))
                    iddesc = p1.Id.Description + p2.Id.Description + p3.Id.Description;
                else
                    iddesc = p1.Id.Description + p3.Id.Description + p2.Id.Description;
            }
            if (Convert.ToInt32(p2.Id.Description) < Convert.ToInt32(p1.Id.Description) && Convert.ToInt32(p2.Id.Description) < Convert.ToInt32(p3.Id.Description))
            {
                if (Convert.ToInt32(p1.Id.Description) < Convert.ToInt32(p3.Id.Description))
                    iddesc = p2.Id.Description + p1.Id.Description + p3.Id.Description;
                else
                    iddesc = p2.Id.Description + p3.Id.Description + p1.Id.Description;
            }
            if (Convert.ToInt32(p3.Id.Description) < Convert.ToInt32(p1.Id.Description) && Convert.ToInt32(p3.Id.Description) < Convert.ToInt32(p2.Id.Description))
            {
                if (Convert.ToInt32(p1.Id.Description) < Convert.ToInt32(p2.Id.Description))
                    iddesc = p3.Id.Description + p1.Id.Description + p2.Id.Description;
                else
                    iddesc = p3.Id.Description + p2.Id.Description + p1.Id.Description;
            }
            id = new Id("Polygon", iddesc);
        }
        public PolygonComponent(PointComponent p1, PointComponent p2, PointComponent p3, Id id) : this(p1, p2, p3)
        {
            this.id = id;
        }

        public void ReplaceLine(LineComponent whatline, LineComponent whichline)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == whatline)
                {
                    lines[i] = whichline;
                  //  ReplacePoint();
                    break;
                }
            }
        }

        public void ReplacePoint(PointComponent whatline, PointComponent whichline)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == whatline)
                {
                    points[i] = whichline;
                    break;
                }
            }
        }

        public override void action(IVisitor visitor)
        {
            visitor.visit(this);
        }
        public override bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir)
        {
            return false;
        }

        public static bool operator ==(PolygonComponent p1, PolygonComponent p2)
        {
            if (p1.id == p2.id)
                return true;
            return false;
        }

        public static bool operator !=(PolygonComponent p1, PolygonComponent p2)
        {
            if (p1.id != p2.id)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            PolygonComponent p = (PolygonComponent)obj;
            if (p == this)
                return true;
            return false;
        }

    }

    class PolygonComponentLines : ModelComponent
    {
        public override Id Id { get { return id; } set { id = value; } }
        Color color = Color.Black;
        public override Color Color
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
        //public PolygonComponent ()
        //{
        //    points = new List<PointComponent>();
        //    lines = new List<LineComponent>();
        //}
        //public PolygonComponent(params LineComponent[] inlines)
        //{
        //    points = new List<PointComponent>();
        //    lines = new List<LineComponent>();
      
        //    for (int i = 0; i < inlines.Length; i++)
        //        AddLine(inlines[i]);
        //}

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

        public override void action(IVisitor visitor)
        {
          // visitor.visit(this);
        }

        public override bool IsGet(MatrixCoord3D ray_pos, MatrixCoord3D ray_dir)
        {
            return false;
        }
    }
}

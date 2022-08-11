using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    interface IObjects
    {
        public abstract Color Color { get; set; }
        public abstract void action(IVisitor visitor);
    }

    class Model : IObjects
    {
        public Color Color { get; set; } = Color.Black;
        protected List<PointComponent> points;
        protected List<LineComponent> lines;
        protected List<PolygonComponent> polygons;

        public List<PointComponent> Points { get { return points; } }
        public List<LineComponent> Lines { get { return lines; } }
        public List <PolygonComponent> Polygons { get { return polygons; } }

        public int NumberPoints { get { return points.Count; } }
        public int NumberLines { get { return lines.Count; } }
        public int NumberPolygons { get { return polygons.Count; } }

        IObjects curobject;
        
        public Model()
        {
            points = new List<PointComponent>();
            lines = new List<LineComponent>();
            polygons = new List<PolygonComponent>();
            curobject = this;
        }

        public void action(IVisitor visitor)
        {
            if (visitor.type == TypeVisitor.Drawer)
            { visitor.visit(this); return; }

            if (curobject != this)
                curobject.action(visitor);
            else
            {
                visitor.visit(this);
            }
        }

        public void SetCurComponent(string what, int what1)
        {
            if (curobject == this)
                Console.WriteLine("dsdsd");
            curobject.Color = Color.Black;
            switch(what)
            {
                case "Point":
                    curobject = points[what1 - 1];
                    Console.WriteLine("set POI");
                    break;
                case "Line":
                    curobject = lines[what1 - 1];
                    Console.WriteLine("set L");
                    break;
                case "Polygon":
                    curobject = polygons[what1 - 1];
                    Console.WriteLine("set POL");
                    break;
                case "Model":
                    curobject = this;
                    Console.WriteLine("set M");
                    break;
            }
            curobject.Color = Color.Green;

        }

     
        public void AddPoint(PointComponent point)
        {
            points.Add(point);
        }

        public  void AddLine(LineComponent line)
        {
            bool inlist =true;
            foreach (LineComponent l in lines)
            {
                if (l == line)
                    inlist = false;
            }
            if (inlist)
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
        public void AddLineByPoints(PointComponent p1, PointComponent p2)
        {
            LineComponent ln = new LineComponent(p1, p2);

            bool inlist =true;
            foreach (LineComponent l in lines)
            {
                if (l == ln)
                { inlist = false; }
            }
            if (inlist)
            {
                lines.Add(ln);

                bool pi1 = false, pi2 = false;
                foreach (PointComponent p in points)
                {
                    if (p == ln.Point1)
                        pi1 = true;
                    if (p == ln.Point2)
                        pi2 = true;
                }
                if (!pi1)
                    points.Add(ln.Point1);
                if (!pi2)
                    points.Add(ln.Point2);
            }
        }
        public void AddPolygons(PolygonComponent polygon)
        {
            foreach (PointComponent p in polygon.Points)
            {
                bool pi = false;
                foreach (PointComponent p1 in points)
                {
                    if (p == p1)
                    {
                        pi = true;
                        break;
                    }

                }
                if (!pi)
                    points.Add(p);
            }

            foreach (LineComponent p in polygon.Lines)
            {
                bool pi = false;
                foreach (LineComponent p1 in lines)
                {
                    if (p == p1)
                    {
                        pi = true;
                        break;
                    }

                }
                if (!pi)
                    lines.Add(p);
            }
        }
    }

    class Cub : Model
    {
        public Cub(PointComponent center, int side) : base()
        {
            points.Add(new PointComponent(center.X - side/2, center.Y +side/2, center.Z+side/2));
            points.Add(new PointComponent(center.X - side / 2, center.Y + side / 2, center.Z - side / 2));
            points.Add(new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z - side / 2));
            points.Add(new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z + side / 2));
            points.Add(new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z + side / 2));
            points.Add(new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z - side / 2));
            points.Add(new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z - side / 2));
            points.Add(new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z + side / 2));

            lines.Add(new LineComponent(points[0], points[1]));
            lines.Add(new LineComponent(points[1], points[2]));
            lines.Add(new LineComponent(points[2], points[3]));
            lines.Add(new LineComponent(points[3], points[0]));
            lines.Add(new LineComponent(points[4], points[5]));
            lines.Add(new LineComponent(points[5], points[6]));
            lines.Add(new LineComponent(points[6], points[7]));
            lines.Add(new LineComponent(points[7], points[4]));
            lines.Add(new LineComponent(points[0], points[4]));
            lines.Add(new LineComponent(points[1], points[5]));
            lines.Add(new LineComponent(points[2], points[6]));
            lines.Add(new LineComponent(points[3], points[7]));

            polygons.Add(new PolygonComponent(lines[0], lines[9], lines[4], lines[8] ));
            polygons.Add(new PolygonComponent(lines[1], lines[10], lines[5], lines[9]));
            polygons.Add(new PolygonComponent(lines[10], lines[6], lines[11], lines[2]));
            polygons.Add(new PolygonComponent(lines[3], lines[11], lines[7], lines[8]));
            polygons.Add(new PolygonComponent(lines[0], lines[1], lines[2], lines[3]));
            polygons.Add(new PolygonComponent(lines[4], lines[5], lines[6], lines[7]));
        }
    }
}

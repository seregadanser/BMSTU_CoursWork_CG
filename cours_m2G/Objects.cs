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
        Color color = Color.Black;
        public Color Color
        {
            get
            { return color; }
            set
            {
                foreach (PolygonComponent l in polygons)
                {
                    l.Color = value;
                }
            }
        }
        protected List<PointComponent> points;
        protected List<LineComponent> lines;
        protected List<PolygonComponent> polygons;

        public List<PointComponent> Points { get { return points; } }
        public List<LineComponent> Lines { get { return lines; } }
        public List <PolygonComponent> Polygons { get { return polygons; } }

        public int NumberPoints { get { return points.Count; } }
        public int NumberLines { get { return lines.Count; } }
        public int NumberPolygons { get { return polygons.Count; } }

        List<IObjects> activeComponents;
        List<PointComponent> activePoints;
        List<int> activePointsCount;
        
        public Model()
        {
            points = new List<PointComponent>();
            lines = new List<LineComponent>();
            polygons = new List<PolygonComponent>();
            activeComponents = new List<IObjects>();
            activePoints = new List<PointComponent>();
            activePointsCount = new List<int>();
       
        }

        public void action(IVisitor visitor)
        {
            if (visitor.type == TypeVisitor.Drawer)
            {
                visitor.visit(this);
                return;
            }

            if (activeComponents.Count>0 && activeComponents[0] != this)
                foreach(PointComponent i in activePoints)
                 i.action(visitor);
            else
            {
                visitor.visit(this);
            }
        }

        public void AddActiveComponent(string what, int what1)
        {
            if (activeComponents.Count > 0)
                if (activeComponents[0] == this)
                    DeliteActive();
            int i = -1;
            switch (what)
            {
                case "Point":
                  i= IsPointInList(points[what1 - 1], activePoints);
                    if (i == -1)
                    {
                        activeComponents.Add(points[what1 - 1]);
                        activePoints.Add(points[what1 - 1]);
                        activePointsCount.Add(1);
                    }
                    else
                    {
                        activePointsCount[i] = activePointsCount[i] + 1;
                    }
                    points[what1 - 1].Color = Color.Green;
                    break;
                case "Line":
                    i = IsObjectInList(lines[what1 - 1], activeComponents);
                    if(i ==-1)
                    {
                        activeComponents.Add(lines[what1 - 1]);
                        i = IsPointInList(lines[what1 - 1].Point1, activePoints);
                        if (i == -1)
                        {
                            activePoints.Add(lines[what1 - 1].Point1);
                            activePointsCount.Add(1);
                        }
                        else
                        {
                            activePointsCount[i] = activePointsCount[i] + 1;
                        }
                        i = IsPointInList(lines[what1 - 1].Point2, activePoints);
                        if (i == -1)
                        {
                            activePoints.Add(lines[what1 - 1].Point2);
                            activePointsCount.Add(1);
                        }
                        else
                        {
                            activePointsCount[i] = activePointsCount[i] + 1;
                        }
                    }
                    lines[what1 - 1].Color = Color.Green;
                    break;
                case "Polygon":
                    i = IsObjectInList(polygons[what1 - 1], activeComponents);
                    if(i==-1)
                    {
                        activeComponents.Add(polygons[what1 - 1]);
                        foreach (PointComponent p in polygons[what1 - 1].Points)
                        {
                            i = IsPointInList(p, activePoints);
                            if (i == -1)
                            {
                                activePoints.Add(p);
                                activePointsCount.Add(1);
                            }
                            else
                            {
                                activePointsCount[i] = activePointsCount[i] + 1;
                            }
                        }
                    }
               
                    polygons[what1 - 1].Color = Color.Green;
                    break;
                case "Model":
                    DeliteActive();
                    activeComponents.Add(this);
                    this.Color = Color.Green;
                    break;
            }
        }

        protected int IsPointInList(PointComponent p, List<PointComponent> list)
        {
            for(int i = 0; i <list.Count; i++)
            {
                if (p == list[i])
                    return i;
            }
            return -1;
        }
        protected int IsObjectInList(IObjects c, List<IObjects> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (c == list[i])
                    return i;
            }
            return -1;
        }

        public void DeliteActive()
        {
            foreach (IObjects i in activeComponents)
                i.Color = Color.Black;
            activeComponents.Clear();
            activePoints.Clear();
            activePointsCount.Clear();
        }
        public void DeliteActive(int what)
        {
            IObjects cur = activeComponents[what];
            cur.Color = Color.Black;
            activeComponents.Remove(cur);
        }

        public void DelitePoint(PointComponent p)
        {
            points.Remove(p);
            
        }

        public void AddPoint(PointComponent point)
        {
            bool pi = true;
            foreach (PointComponent p in points)
            {
                if (p == point)
                    pi = false;
            }
            if (pi)
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
                        polygon.ReplacePoint(p, p1);
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
                        polygon.ReplaceLine(p, p1);
                        break;
                    }

                }
                if (!pi)
                    lines.Add(p);
            }
            bool pi3 = false;
            foreach (PolygonComponent poly in polygons)
            {
                if (polygon == poly)
                    pi3 = true;
            }
            if (!pi3)
                polygons.Add(polygon);
        }
    }

    class Cub : Model
    {
        public Cub(PointComponent center, int side) : base()
        {

            //AddPoint(new PointComponent(center.X - side / 2, center.Y + side / 2, center.Z + side / 2));
            //AddPoint(new PointComponent(center.X - side / 2, center.Y + side / 2, center.Z - side / 2));
            //AddPoint(new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z - side / 2));
            //AddPoint(new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z + side / 2));
            //AddPoint(new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z + side / 2));
            //AddPoint(new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z - side / 2));
            //AddPoint(new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z - side / 2));
            //AddPoint(new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z + side / 2));

            //AddLine(new LineComponent(points[0], points[1]));
            //AddLine(new LineComponent(points[1], points[2]));
            //AddLine(new LineComponent(points[2], points[3]));
            //AddLine(new LineComponent(points[3], points[0]));
            //AddLine(new LineComponent(points[4], points[5]));
            //AddLine(new LineComponent(points[5], points[6]));
            //AddLine(new LineComponent(points[6], points[7]));
            //AddLine(new LineComponent(points[7], points[4]));
            //AddLine(new LineComponent(points[0], points[4]));
            //AddLine(new LineComponent(points[1], points[5]));
            //AddLine(new LineComponent(points[2], points[6]));
            //AddLine(new LineComponent(points[3], points[7]));

            PointComponent p1 = new PointComponent(center.X - side / 2, center.Y + side / 2, center.Z + side / 2);
            PointComponent p2 = new PointComponent(center.X - side / 2, center.Y + side / 2, center.Z - side / 2);
            PointComponent p3 = new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z - side / 2);
            PointComponent p4 = new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z + side / 2);
            PointComponent p5 = new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z + side / 2);
            PointComponent p6 = new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z - side / 2);
            PointComponent p7 = new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z - side / 2);
            PointComponent p8 = new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z + side / 2);
            AddLine(new LineComponent(p1, p2));
            AddLine(new LineComponent(p2, p3));
            AddLine(new LineComponent(p3, p4));
            AddLine(new LineComponent(p4, p1));
            AddLine(new LineComponent(p5, p6));
            AddLine(new LineComponent(p6, p7));
            AddLine(new LineComponent(p7, p8));
            AddLine(new LineComponent(p8, p5));
            AddLine(new LineComponent(p1, p5));
            AddLine(new LineComponent(p2, p6));
            AddLine(new LineComponent(p3, p7));
            AddLine(new LineComponent(p4, p8));


            AddPolygons(new PolygonComponent(p1, p2,p6));
            AddPolygons(new PolygonComponent(p1,p6,p5));
            AddPolygons(new PolygonComponent(p4,p3,p7));
            AddPolygons(new PolygonComponent(p8,p7,p4));
            AddPolygons(new PolygonComponent(p1,p4,p8));
            AddPolygons(new PolygonComponent(p1,p5,p8));
            AddPolygons(new PolygonComponent(p1,p2,p3));
            AddPolygons(new PolygonComponent(p1,p3,p4));
            AddPolygons(new PolygonComponent(p5,p8,p7));
            AddPolygons(new PolygonComponent(p5,p6,p7));
            AddPolygons(new PolygonComponent(p2,p3,p7));
            AddPolygons(new PolygonComponent(p2,p7,p6));
        }
    }
}

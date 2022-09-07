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
        public Id Id { get; set; }
        public abstract void action(IVisitor visitor);
    }

    class Model : IObjects
    {
        Color color = Color.Black;
        public Id Id { get; set; }
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
        protected Container<PointComponent> points;
        protected Container<LineComponent> lines;
        protected Container<PolygonComponent> polygons;

        public Container<PointComponent> Points { get { return points; } }
        public Container<LineComponent> Lines { get { return lines; } }
        public Container<PolygonComponent> Polygons { get { return polygons; } }
        public int NumberPoints { get { return points.Count; } }
        public int NumberLines { get { return lines.Count; } }
        public int NumberPolygons { get { return polygons.Count; } }

        Container<PointComponent> activeComponents;
        List<IObjects> activeComponentsParents;
        List<Id> activeComponentsId;

        public List<Id> ActiveComponentsId { get { return activeComponentsId; } }

        public Model()
        {
            points = new Container<PointComponent>();
            lines = new Container<LineComponent>();
            polygons = new Container<PolygonComponent>();
            activeComponents = new Container<PointComponent>();
            activeComponentsParents = new List<IObjects>();
            activeComponentsId = new List<Id>();
        }

        public void action(IVisitor visitor)
        {
            if (visitor.type == TypeVisitor.Drawer || visitor.type == TypeVisitor.Reader)
            {
                visitor.visit(this);
                return;
            }

            if (activeComponents.Count > 0 )
                foreach (PointComponent i in activeComponents)
                    i.action(visitor);
            else
            {
               visitor.visit(this);
            }
        }

        public void AddActiveComponent(string what, int what1)
        { 
            int i = -1;
            switch (what)
            {
                case "Point":
                    activeComponents.Add(points[what1 - 1], points[what1 - 1].Id, points[what1 - 1].Id);
                    activeComponentsParents.Add(points[what1 - 1]);
                    activeComponentsId.Add(points[what1 - 1].Id);
                    points[what1 - 1].Color = Color.Red;
                    break;
                case "Line":
                    activeComponents.Add(lines[what1 - 1].Point1, lines[what1 - 1].Point1.Id, lines[what1 - 1].Id);
                    activeComponents.Add(lines[what1 - 1].Point2, lines[what1 - 1].Point2.Id,lines[what1 - 1].Id);
                    activeComponentsParents.Add(lines[what1 - 1]);
                    activeComponentsId.Add(lines[what1 - 1].Id);
                    lines[what1 - 1].Color = Color.Red;
                    break;
                case "Polygon":
                    activeComponents.Add(polygons[what1 - 1].Points[0], polygons[what1 - 1].Points[0].Id, polygons[what1 - 1].Id);
                    activeComponents.Add(polygons[what1 - 1].Points[1], polygons[what1 - 1].Points[1].Id, polygons[what1 - 1].Id);
                    activeComponents.Add(polygons[what1 - 1].Points[2], polygons[what1 - 1].Points[2].Id, polygons[what1 - 1].Id);
                    activeComponentsParents.Add(polygons[what1 - 1]);
                    activeComponentsId.Add(polygons[what1 - 1].Id);
                    polygons[what1 - 1].Color = Color.Red;
                    break;
            }
        }

        public void AddActiveComponent(Id id)
        {
            int i = -1;
            switch (id.Name)
            {
                case "Point":
                    activeComponents.Add(points[id], points[id].Id, points[id].Id);
                    activeComponentsParents.Add(points[id]);
                    activeComponentsId.Add(points[id].Id);
                    points[id].Color = Color.Red;
                    break;
                case "Line":
                    activeComponents.Add(lines[id].Point1, lines[id].Point1.Id, lines[id].Id);
                    activeComponents.Add(lines[id].Point2, lines[id].Point2.Id, lines[id].Id);
                    activeComponentsParents.Add(lines[id]);
                    activeComponentsId.Add(lines[id].Id);
                    lines[id].Color = Color.Red;
                    break;
                case "Polygon":
                    activeComponents.Add(polygons[id].Points[0], polygons[id].Points[0].Id, polygons[id].Id);
                    activeComponents.Add(polygons[id].Points[1], polygons[id].Points[1].Id, polygons[id].Id);
                    activeComponents.Add(polygons[id].Points[2], polygons[id].Points[2].Id, polygons[id].Id);
                    activeComponentsParents.Add(polygons[id]);
                    activeComponentsId.Add(polygons[id].Id);
                    polygons[id].Color = Color.Red;
                    break;
            }
        }
        public void DeliteActive()
        {
           for (int j = activeComponents.Count-1; j >= 0; j--)
            {
               Tuple<List<Id>,List<Id> > w = activeComponents.Remove(j);
            }
            foreach (IObjects i in activeComponentsParents)
                i.Color = Color.Black;
            activeComponentsParents.Clear();
            activeComponentsId.Clear();
        }
        public void DeliteActive(Id id)
        {
            List<Id> r = activeComponents.RemoveParent(id);
            foreach (Id i in r)
            {
                activeComponents.Remove(i);
            }
            activeComponentsId.Remove(id);

            for (int i = 0; i < activeComponentsParents.Count; i++)
            {
                if (activeComponentsParents[i].Id == id)
                {
                    activeComponentsParents.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemovePoint(Id id)
        {
            int delindex = -1;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Id == id)
                { delindex = i; break; }
            }
            if (delindex != -1)
            {
                List<Id> parents = points.Remove(delindex).Item1;
                List<Id> rrl = lines.RemoveChildren(id);
                for (int i = 0; i < rrl.Count; i++)
                {
                    RemoveLine(rrl[i]);
                }
                List<Id> rrp = polygons.RemoveChildren(id);
                for (int i = 0; i < rrp.Count; i++)
                {
                    RemovePolygon(rrp[i]);
                }
            }
        }

        public void RemoveLine(Id id)
        {
            int delindex = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Id == id)
                { delindex = i; break; }
            }
            if (delindex != -1)
            {
                lines.Remove(delindex);
                List<Id> rrl = points.RemoveParent(id);
                for (int i = 0; i < rrl.Count; i++)
                {
                    RemovePoint(rrl[i]);
                }
                List<Id> rrp = polygons.RemoveChildren(id);
                for (int i = 0; i < rrp.Count; i++)
                {
                    RemovePolygon(rrp[i]);
                }
            }
        }

        public void RemovePolygon(Id id)
        {
            int delindex = -1;
            for(int i= 0; i < polygons.Count; i++)
            {
                if (polygons[i].Id == id)
                { delindex = i;break; }
            }
            if (delindex != -1)
            {
                List<Id> children = polygons.Remove(delindex).Item2;
                List<Id> rrl = lines.RemoveParent(id);
                for (int i = 0; i < rrl.Count; i++)
                {
                    RemoveLine(rrl[i]);
                }
                List<Id> rrp = points.RemoveParent(id);
                for (int i = 0; i < rrp.Count; i++)
                {
                    RemovePoint(rrp[i]);
                }
            }
        }

        public void AddPoint(PointComponent point)
        {
            points.Add(point, point.Id);
        }

        public void AddLine(LineComponent line)
        {
           int k = lines.Add(line, line.Id,0 ,line.Point1.Id, line.Point2.Id);
            if(k ==-1)
            {
               k= points.Add(line.Point1, line.Point1.Id, line.Id);
                if(k!=-1)
                {
                    line.ReplacePoint(line.Point1, points[k]);
                }
                k= points.Add(line.Point2, line.Point2.Id, line.Id);
                if (k != -1)
                {
                    line.ReplacePoint(line.Point2, points[k]);
                }
            }
        }

        public void AddPolygons(PolygonComponent polygon)
        {
            int k = polygons.Add(polygon,polygon.Id ,0, polygon.Points[0].Id, polygon.Points[1].Id, polygon.Points[2].Id, polygon.Lines[0].Id, polygon.Lines[1].Id, polygon.Lines[2].Id);
            if(k == -1)
            {
                for(int i = 0; i < polygon.Lines.Length;i++)
                {
                    k = lines.Add(polygon.Lines[i], polygon.Lines[i].Id, polygon.Id, 0,polygon.Lines[i].Point1.Id, polygon.Lines[i].Point2.Id);
                    if(k!=-1)
                    { 
                        polygon.ReplaceLine(polygon.Lines[i], lines[k]);
                    }
                    k = points.Add(polygon.Lines[i].Point1, polygon.Lines[i].Point1.Id, polygon.Id, polygon.Lines[i].Id);
                    if (k != -1)
                        polygon.ReplacePoint(polygon.Lines[i].Point1, points[k]);
                    k= points.Add(polygon.Lines[i].Point2, polygon.Lines[i].Point2.Id, polygon.Id, polygon.Lines[i].Id);
                    if (k != -1)
                        polygon.ReplacePoint(polygon.Lines[i].Point2, points[k]);
                }
            }
        }
    }

    //class Model : IObjects
    //{
    //    Color color = Color.Black;
    //    public Id Id { get; set; }
    //    public Color Color
    //    {
    //        get
    //        { return color; }
    //        set
    //        {
    //            foreach (PolygonComponent l in polygons)
    //            {
    //                l.Color = value;
    //            }
    //        }
    //    }
    //    protected List<PointComponent> points;
    //    protected List<LineComponent> lines;
    //    protected List<PolygonComponent> polygons;

    //    public List<PointComponent> Points { get { return points; } }
    //    public List<LineComponent> Lines { get { return lines; } }
    //    public List<PolygonComponent> Polygons { get { return polygons; } }

    //    public int NumberPoints { get { return points.Count; } }
    //    public int NumberLines { get { return lines.Count; } }
    //    public int NumberPolygons { get { return polygons.Count; } }

    //    List<IObjects> activeComponents;
    //    List<PointComponent> activePoints;
    //    List<int> activePointsCount;

    //    public Model()
    //    {
    //        points = new List<PointComponent>();
    //        lines = new List<LineComponent>();
    //        polygons = new List<PolygonComponent>();
    //        activeComponents = new List<IObjects>();
    //        activePoints = new List<PointComponent>();
    //        activePointsCount = new List<int>();

    //    }

    //    public void action(IVisitor visitor)
    //    {
    //        if (visitor.type == TypeVisitor.Drawer)
    //        {
    //            visitor.visit(this);
    //            return;
    //        }

    //        if (activeComponents.Count > 0 && activeComponents[0] != this)
    //            foreach (PointComponent i in activePoints)
    //                i.action(visitor);
    //        else
    //        {
    //            visitor.visit(this);
    //        }
    //    }

    //    public void AddActiveComponent(string what, int what1)
    //    {
    //        if (activeComponents.Count > 0)
    //            if (activeComponents[0] == this)
    //                DeliteActive();
    //        int i = -1;
    //        switch (what)
    //        {
    //            case "Point":
    //                i = IsPointInList(points[what1 - 1], activePoints);
    //                if (i == -1)
    //                {
    //                    activeComponents.Add(points[what1 - 1]);
    //                    activePoints.Add(points[what1 - 1]);
    //                    activePointsCount.Add(1);
    //                }
    //                else
    //                {
    //                    activePointsCount[i] = activePointsCount[i] + 1;
    //                }
    //                points[what1 - 1].Color = Color.Green;
    //                break;
    //            case "Line":
    //                i = IsObjectInList(lines[what1 - 1], activeComponents);
    //                if (i == -1)
    //                {
    //                    activeComponents.Add(lines[what1 - 1]);
    //                    i = IsPointInList(lines[what1 - 1].Point1, activePoints);
    //                    if (i == -1)
    //                    {
    //                        activePoints.Add(lines[what1 - 1].Point1);
    //                        activePointsCount.Add(1);
    //                    }
    //                    else
    //                    {
    //                        activePointsCount[i] = activePointsCount[i] + 1;
    //                    }
    //                    i = IsPointInList(lines[what1 - 1].Point2, activePoints);
    //                    if (i == -1)
    //                    {
    //                        activePoints.Add(lines[what1 - 1].Point2);
    //                        activePointsCount.Add(1);
    //                    }
    //                    else
    //                    {
    //                        activePointsCount[i] = activePointsCount[i] + 1;
    //                    }
    //                }
    //                lines[what1 - 1].Color = Color.Green;
    //                break;
    //            case "Polygon":
    //                i = IsObjectInList(polygons[what1 - 1], activeComponents);
    //                if (i == -1)
    //                {
    //                    activeComponents.Add(polygons[what1 - 1]);
    //                    foreach (PointComponent p in polygons[what1 - 1].Points)
    //                    {
    //                        i = IsPointInList(p, activePoints);
    //                        if (i == -1)
    //                        {
    //                            activePoints.Add(p);
    //                            activePointsCount.Add(1);
    //                        }
    //                        else
    //                        {
    //                            activePointsCount[i] = activePointsCount[i] + 1;
    //                        }
    //                    }
    //                }

    //                polygons[what1 - 1].Color = Color.Green;
    //                break;
    //            case "Model":
    //                DeliteActive();
    //                activeComponents.Add(this);
    //                this.Color = Color.Green;
    //                break;
    //        }
    //    }

    //    protected int IsPointInList(PointComponent p, List<PointComponent> list)
    //    {
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            if (p == list[i])
    //                return i;
    //        }
    //        return -1;
    //    }
    //    protected int IsObjectInList(IObjects c, List<IObjects> list)
    //    {
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            if (c == list[i])
    //                return i;
    //        }
    //        return -1;
    //    }

    //    public void DeliteActive()
    //    {
    //        foreach (IObjects i in activeComponents)
    //            i.Color = Color.Black;
    //        activeComponents.Clear();
    //        activePoints.Clear();
    //        activePointsCount.Clear();
    //    }
    //    public void DeliteActive(int what)
    //    {
    //        IObjects cur = activeComponents[what];
    //        cur.Color = Color.Black;
    //        activeComponents.Remove(cur);
    //    }

    //    public void DelitePoint(PointComponent p)
    //    {
    //        points.Remove(p);

    //    }

    //    public void AddPoint(PointComponent point)
    //    {
    //        bool pi = true;
    //        foreach (PointComponent p in points)
    //        {
    //            if (p.Id == point.Id)
    //                pi = false;
    //        }
    //        if (pi)
    //            points.Add(point);
    //    }

    //    public void AddLine(LineComponent line)
    //    {
    //        bool inlist = true;
    //        foreach (LineComponent l in lines)
    //        {
    //            if (l.Id == line.Id)
    //                inlist = false;
    //        }
    //        if (inlist)
    //        {
    //            lines.Add(line);

    //            AddPoint(line.Point1);
    //            AddPoint(line.Point2);
    //        }
    //    }

    //    public void AddPolygons(PolygonComponent polygon)
    //    {
    //        foreach (PointComponent p in polygon.Points)
    //        {
    //            bool pi = false;
    //            foreach (PointComponent p1 in points)
    //            {
    //                if (p.Id == p1.Id)
    //                {
    //                    pi = true;
    //                    polygon.ReplacePoint(p, p1);
    //                    break;
    //                }

    //            }
    //            if (!pi)
    //                points.Add(p);
    //        }

    //        foreach (LineComponent p in polygon.Lines)
    //        {
    //            bool pi = false;
    //            foreach (LineComponent p1 in lines)
    //            {
    //                if (p.Id == p1.Id)
    //                {
    //                    pi = true;
    //                    polygon.ReplaceLine(p, p1);
    //                    break;
    //                }

    //            }
    //            if (!pi)
    //                lines.Add(p);
    //        }
    //        bool pi3 = false;
    //        foreach (PolygonComponent poly in polygons)
    //        {
    //            if (polygon.Id == poly.Id)
    //                pi3 = true;
    //        }
    //        if (!pi3)
    //            polygons.Add(polygon);
    //    }
    //}

    class Cub : Model
    {
        public Cub(PointComponent center, int side) : base()
        {
            PointComponent p1 = new PointComponent(center.X - side / 2, center.Y + side / 2, center.Z + side / 2, new Id("Point", "1"));
            PointComponent p2 = new PointComponent(center.X - side / 2, center.Y + side / 2, center.Z - side / 2, new Id("Point", "2"));
            PointComponent p3 = new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z - side / 2, new Id("Point", "3"));
            PointComponent p4 = new PointComponent(center.X + side / 2, center.Y + side / 2, center.Z + side / 2, new Id("Point", "4"));
            PointComponent p5 = new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z + side / 2, new Id("Point", "5"));
            PointComponent p6 = new PointComponent(center.X - side / 2, center.Y - side / 2, center.Z - side / 2, new Id("Point", "6"));
            PointComponent p7 = new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z - side / 2, new Id("Point", "7"));
            PointComponent p8 = new PointComponent(center.X + side / 2, center.Y - side / 2, center.Z + side / 2, new Id("Point", "8"));

            AddPoint(p1);
            AddPoint(p2);
            AddPoint(p3);
            AddPoint(p4);
            AddPoint(p5);
            AddPoint(p6);
            AddPoint(p7);
            AddPoint(p8);

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


            AddPolygons(new PolygonComponent(p1,p2,p6));
            AddPolygons(new PolygonComponent(p1,p6,p5));
            AddPolygons(new PolygonComponent(p3,p4,p7));
            AddPolygons(new PolygonComponent(p8,p7,p4));
            AddPolygons(new PolygonComponent(p4,p1,p8));
            AddPolygons(new PolygonComponent(p1,p5,p8));
            AddPolygons(new PolygonComponent(p1,p3,p2));
            AddPolygons(new PolygonComponent(p1,p4,p3));
            AddPolygons(new PolygonComponent(p5,p7,p8));
            AddPolygons(new PolygonComponent(p5,p6,p7));
            AddPolygons(new PolygonComponent(p2,p3,p7));
            AddPolygons(new PolygonComponent(p2,p7,p6));
            polygons[0].ColorF = Color.Green;
            polygons[1].ColorF = Color.Aqua;
            polygons[2].ColorF = Color.Beige;
            polygons[3].ColorF = Color.Crimson;
            polygons[4].ColorF = Color.DarkOrange;
            polygons[5].ColorF = Color.Indigo;
            polygons[6].ColorF = Color.Ivory;
            polygons[7].ColorF = Color.Lime;
            polygons[8].ColorF = Color.Navy;
            polygons[9].ColorF = Color.Snow;
            polygons[10].ColorF = Color.Olive;
            polygons[11].ColorF = Color.Orange;
            //   RemovePoint(new Id("Point", "4"));
            //RemovePolygon(new Id("Polygon", "126"));
            //RemovePolygon(new Id("Polygon", "156"));
        }
    }
}

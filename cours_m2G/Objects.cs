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

    class ActiveElements : IObjects
    {
        Color color = Color.Black;
        public Id Id { get; set; }
        public Color Color
        {
            get
            { return color; }
            set
            {
                color = value;
            }
        }

        List<IObjects> activeComponents;
        List<Id> activeComponentsId;
        List<List<Id>> parents, childeren;

        public ActiveElements()
        {
            activeComponents = new List<IObjects>();
            activeComponentsId = new List<Id>();
            parents = new List<List<Id>>();
            childeren = new List<List<Id>>();
        }

        public void action(IVisitor visitor)
        {
            foreach (IObjects i in activeComponents)
                i.action(visitor);
        }

        public bool AddElement(IObjects element, List<Id> parent, List<Id> child)
        {
            Id eid = element.Id;
            foreach (Id i in activeComponentsId)
                if (eid == i)
                    return false;
            for (int i = 0; i < activeComponents.Count; i++)
                foreach (Id id in childeren[i])
                    if (id == eid)
                        return false;
            for (int i = 0; i < activeComponents.Count; i++)
                foreach (Id id in parents[i])
                    if (id == eid)
                    {
                        RemoveElement(activeComponentsId[i]);
                    }
            element.Color = Color.Red;
            activeComponents.Add(element);
            activeComponentsId.Add(element.Id);
            parents.Add(parent);
            childeren.Add(child);
            return true;
        }
        public bool RemoveElement(Id id)
        {
            int pos = -1;
            for (int i = 0; i < activeComponents.Count; i++)
                if (activeComponentsId[i] == id)
                { pos = i; break; }
            if(pos!=-1)
            {
                activeComponents[pos].Color = Color.Black;
                activeComponents.RemoveAt(pos);
                activeComponentsId.RemoveAt(pos);
                parents.RemoveAt(pos);
                childeren.RemoveAt(pos);
                return true;
            }
            return false;
        }
        public void ClearElements()
        {
            foreach (IObjects i in activeComponents)
                i.Color = Color.Black;
            activeComponents.Clear();
            activeComponentsId.Clear();
            parents.Clear();
            childeren.Clear();
        }
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
                    activeComponentsParents[i].Color = Color.Black;
                    activeComponentsParents.RemoveAt(i);
                    return;
                }
            }
        }

        public List<Id> GetConnectedElements(Id id)
        {
            switch (id.Name)
            {
                case "Point":
                   return points.GetConnectionObjects(id);
                case "Line":
                    return lines.GetConnectionObjects(id);
                case "Polygon":
                   return polygons.GetConnectionObjects(id);
                default:
                    return null;
            }
        }

        public void RemovebyId(Id id)
        {
            switch(id.Name)
            {
                case "Point":
                    RemovePoint(id);
                    break;
                case "Line":
                    RemoveLine(id);
                    break;
                case "Polygon":
                    RemovePolygon(id);
                    break;
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
                lines[delindex].Color = Color.Black;
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
                polygons[delindex].Color = Color.Black;
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
        }
    }
    class Pyramide1 : Model
    {
        public Pyramide1(PointComponent center,double height, double radius) : base()
        {

        }
    }
}

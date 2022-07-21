using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    interface ModelComponent
    {
        public void action(IVisitor visitor);
        public bool IsGet(MatrixCoord3D mouse);
        public bool IsGet(PointComponent mouse);
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
        public bool IsGet(MatrixCoord3D mouse) 
        {
            if (Math.Pow(mouse.X - coords.X,2)+Math.Pow(mouse.Y - coords.Y, 2)<=Math.Pow(hit_radius, 2))
                return true;
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
    }

    class LineComponent : ModelComponent
    {
        public Color Color { get; set; } = Color.Black;
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
        public bool IsGet(MatrixCoord3D mouse)
        {
            return false;
        }
        public bool IsGet(PointComponent mouse)
        {
            return false;
        }
        public static LineComponent operator *(LineComponent line, MatrixTransformation3D transform)
        {
            return new LineComponent(line.point1 * transform, line.point2*transform);
        }
    }
}

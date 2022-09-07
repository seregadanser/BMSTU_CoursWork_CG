using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;


namespace cours_m2G
{
   
     interface IVisitor
    {
        public TypeVisitor type { get; }
        abstract public void visit(PointComponent point);
        abstract public void visit(LineComponent line);
        abstract public void visit(PolygonComponent polygon);
        abstract public void visit(Model model);
    }

    abstract class ScreenVisitor : IVisitor
    {
         protected int scale;
         protected Size screen;
        
        public virtual int Scale { get { return scale; } set { if(value>0) scale = value; } }
        public Size Screen { get { return screen; } set { screen = value; } }

        public abstract TypeVisitor type { get; }

        public abstract void visit(PointComponent p);
        public abstract void visit(LineComponent l);
        public abstract void visit(PolygonComponent polygon);
        abstract public void visit(Model model);

    }

    class DrawVisitor : ScreenVisitor
    {
        public override TypeVisitor type { get; } = TypeVisitor.Drawer;
        public virtual Bitmap Bmp { get { return raster.Bmp; } }
        public override int Scale { get { return scale; } set { if (value > 0) { scale = value; raster.Scale = value; } } }
        public bool PrintText { get; set; } = true;
        protected Rasterizator raster;

        public virtual int SetRaster
        {
            set
            {
                if(value==0)
                    raster = new RasterizatorNoCutter(scale, screen);
                if (value == 1)
                    raster = new RasterizatorNoText(scale, screen);
                if(value == 2)
                    raster = new RasterizatorNoPoints(scale, screen);
                if(value == 3)
                    raster = new RasterizatorCutter(scale, screen);
            }
        }


        public DrawVisitor(Size screen, int scale)
        {
            this.screen = screen;
            this.scale = scale;
            raster = new RasterizatorNoCutter(scale,screen);
        }

        public override void visit(PointComponent point)
        {
            raster.DrawPoint(point);
        }
        public override void visit(LineComponent line)
        {
            raster.DrawLine(line);
            line.Point1.action(this);
            line.Point2.action(this);
        }
        int k = 0;
        public override void visit(PolygonComponent polygon)
        {
            raster.DrawPolygon(polygon);
            foreach (LineComponent l in polygon.Lines)
            { l.action(this);
         }
        }

        public override void visit(Model model)
        {
            PictureBuff.Filled = false;
            PictureBuff.Creator = raster.Type;
            PictureBuff.Clear();
            foreach (PolygonComponent l in model.Polygons)
            {
                //if(MatrixCoord3D.scalar(l.Normal, ))
                l.action(this);
             
            }
            //foreach (LineComponent l in model.Lines)
            //{
            //    l.action(this);
            //}
            PictureBuff.Filled = true;
        }
    }

    class DrawVisitorCamera : DrawVisitor
    {
        Camera cam;
        public Camera Cam { get { return cam; } set { cam = value; } }
        public override int SetRaster
        {
            set
            {
                if (value == 0)
                    raster = new RasterizatorNoCutter(cam, scale, screen);
                if (value == 1)
                    raster = new RasterizatorNoText(cam,scale, screen);
                if (value == 2)
                    raster = new RasterizatorNoPoints(cam,scale, screen);
                if (value == 3)
                    raster = new RasterizatorCutter(cam, scale, screen);
            }
        }
        public DrawVisitorCamera(Size screen, int scale, Camera cam) : base(screen, scale)
        {
            this.cam = cam;
            raster = new RasterizatorNoCutter(cam, scale, screen);
        }
        int k = 0;
        public override void visit(Model model)
        {
            PictureBuff.Creator = raster.Type;
            PictureBuff.Clear();
            PictureBuff.Filled = false;
            string s = "file" + Convert.ToInt32(k) + ".png";
          k++;

          //lock (PictureBuff.locker)
          //     PictureBuff.bmp.Save(s, ImageFormat.Png);
            foreach (PolygonComponent l in model.Polygons)
            {
              // if(MatrixCoord3D.scalar(l.Normal,cam.Direction)>0)
                l.action(this);
                s = "file" + Convert.ToInt32(k) + ".png";
              //k++;
              //  lock (PictureBuff.locker)
              //     PictureBuff.bmp.Save(s, ImageFormat.Png);
            }
           // s = "file" + Convert.ToInt32(k) + ".png";
           // k++;
           //lock (PictureBuff.locker)
           //    PictureBuff.bmp.Save(s, ImageFormat.Png);
            PictureBuff.Filled = true;
        }
    }

    class DrawVisitorR : DrawVisitor
    {
        Camera cam;
        public override Bitmap Bmp { get { return PictureBuff.GetBitmap(); } }
        public DrawVisitorR(Size screen, int scale, Camera cam) : base(screen, scale)
        {
            this.cam = cam;
        }
        public override void visit(PointComponent point)
        {
   
        }
        public override void visit(LineComponent line)
        {

        }

        public override void visit(PolygonComponent polygon)
        {

        }
        private Tuple<MatrixCoord3D, double> FoundCenter(Container<PointComponent> points)
        {
            double minX, maxX, minY, maxY, minZ, maxZ;
            minX = points[0].X;
            maxX = points[0].X;
            minY = points[0].Y;
            maxY = points[0].Y;
            minZ = points[0].Z;
            maxZ = points[0].Z;

            foreach (PointComponent p in points)
            {
                if (p.X < minX)
                    minX = p.X;
                if (p.X > maxX)
                    maxX = p.X;
                if (p.Y < minY)
                    minY = p.Y;
                if (p.Y > maxY)
                    maxY = p.Y;
                if (p.Z < minZ)
                    minZ = p.Z;
                if (p.Z > maxZ)
                    maxZ = p.Z;
            }

            // Геометрический центр между всеми объектами.
            MatrixCoord3D position = new MatrixCoord3D((minX + maxX) / 2f, (minY + maxY) / 2f, (minZ + maxZ) / 2f);
            double r = 0;
            foreach (PointComponent p in points)
            {
                LineComponent minx = new LineComponent(new PointComponent(position), p);
                r = Math.Max(minx.Len(), r);
            }
       
            return new Tuple<MatrixCoord3D,double>( position,r);
        }
    public async override void visit(Model model)
        {
            int x1 = 0;
            string gg = "";
            Console.WriteLine(Thread.CurrentThread.Name);
            Tuple<MatrixCoord3D, double> sphere = FoundCenter(model.Points);
            Console.WriteLine("0");
            MatrixCoord3D CamPosition = cam.Position.Coords;
            MatrixCoord3D CamDirection = cam.Direction;
            MatrixTransformation3D RotateMatrix = cam.RotateMatrix;
            PictureBuff.Filled = false;
            PictureBuff.Creator = RenderType.RAY;
            Parallel.For(0, screen.Width, x =>
              {
                  for (int y = 0; y < screen.Height; y++)
                  {
                      MatrixCoord3D D = CanvasToVieport(x, y) * RotateMatrix.InversedMatrix();// new MatrixCoord3D(cam.RotateMatrix.Coeff[0,2], cam.RotateMatrix.Coeff[1, 2], cam.RotateMatrix.Coeff[2, 2]);
                      D.Normalise();
                      Color c = Color.White;
                      gg = "-";
                      if (RaySphereIntersection(CamPosition, D, sphere.Item1, sphere.Item2) != double.MaxValue)
                      { c = RayT(model, D, CamPosition);
                          gg = "+";
                      
                      }
                      PictureBuff.SetPixel(x, y, c.ToArgb());
                    //  lock(PictureBuff.locker)
                      {
                          x1++;
                        //  Console.WriteLine(Convert.ToString(x1)+" "+gg);
                      }

                  }
              });
            Console.WriteLine("100");
            PictureBuff.Filled = true;
        }

        private Color RayT(Model model, MatrixCoord3D D, MatrixCoord3D position)
        {

            PolygonComponent closest = null;
            double closest_t = double.MaxValue;
        //  Parallel.ForEach<PolygonComponent>(model.Polygons, p=> { 
           foreach (PolygonComponent p in model.Polygons)
            {
             //   if (MatrixCoord3D.scalar(p.Normal, cam.Direction) > 0)
               {
                    MatrixCoord3D tt = GetTimeAndUvCoord(position, D, p.Points[0].Coords, p.Points[1].Coords, p.Points[2].Coords);
                    if (tt != null)
                    {
                        if (tt.X < closest_t && tt.X > 1)
                        {
                            closest_t = tt.X;
                            closest = p;
                        }
                    }
                }
            }
            
            if (closest == null)
                return Color.White;
            return closest.ColorF;
        }

        double RaySphereIntersection(MatrixCoord3D rayOrigin, MatrixCoord3D rayDirection, MatrixCoord3D spos, double r)
        {
            double t = Double.MaxValue;
            //a == 1; // because rdir must be normalized
            MatrixCoord3D k = rayOrigin - spos;
            double b = MatrixCoord3D.scalar(k, rayDirection);
            double c = MatrixCoord3D.scalar(k, k) - r * r;
            double d = b * b - c;
            if (d >= 0)
            {
                double sqrtfd = Math.Sqrt(d);
                // t, a == 1
                double t1 = -b + sqrtfd;
                double t2 = -b - sqrtfd;
                double min_t = Math.Min(t1, t2);
                double max_t = Math.Max(t1, t2);
                t = (min_t >= 0) ? min_t : max_t;
            }
            return t;
        }

        private const double Epsilon = 0.000001d;

        public static MatrixCoord3D? GetTimeAndUvCoord(MatrixCoord3D rayOrigin, MatrixCoord3D rayDirection, MatrixCoord3D vert0, MatrixCoord3D vert1, MatrixCoord3D vert2)
        {
            var edge1 = vert1 - vert0;
            var edge2 = vert2 - vert0;

            var pvec = (rayDirection* edge2);

            var det = MatrixCoord3D.scalar(edge1, pvec);

            if (det > -Epsilon && det < Epsilon)
            {
                return null;
            }

            var invDet = 1d / det;

            var tvec = rayOrigin - vert0;

            var u = MatrixCoord3D.scalar(tvec, pvec) * invDet;

            if (u < 0 || u > 1)
            {
                return null;
            }

            var qvec = (tvec* edge1);

            var v = MatrixCoord3D.scalar(rayDirection, qvec) * invDet;

            if (v < 0 || u + v > 1)
            {
                return null;
            }

            var t = MatrixCoord3D.scalar(edge2, qvec) * invDet;

            return new MatrixCoord3D(t, u, v);
        }
     

        public static MatrixCoord3D GetTrilinearCoordinateOfTheHit(float t, MatrixCoord3D rayOrigin, MatrixCoord3D rayDirection)
        {
            return rayDirection * t + rayOrigin;
        }

        private MatrixCoord3D CanvasToVieport(int x, int y)
        {
            double aspectRatio =screen.Width / screen.Height;
            double fieldOfView = Math.Tan(cam.Fovy / 2 * Math.PI / 180.0f);

            double fx = aspectRatio * fieldOfView * (2 * ((x + 0.5f) / screen.Width) - 1);
            double fy = (1 - (2 * (y + 0.5f) / screen.Height)) * fieldOfView;

            return new MatrixCoord3D(fx/scale, fy/scale, -1);
        }
    }

    class ReadVisitor : ScreenVisitor
    {
        public override TypeVisitor type { get; } = TypeVisitor.Reader;
        public ReadVisitor(Size screen, int scale)
        {
            this.screen = screen;
            this.scale = scale;
        }

        public override void visit(PointComponent p)
        {
 
        }
        public override void visit(LineComponent l)
        {
     
        }

        public override void visit(PolygonComponent polygon)
        {

        }

        public override void visit(Model model)
        {

        }

    }

    class ReadVisitorCamera : ReadVisitor
    {
        readonly Camera cam;
        public Point InPoint { get; set; }
        private ModelComponent find;
        public ModelComponent Find{get{return find;} }

        public ReadVisitorCamera(Camera cam, Size screen, int scale) : base(screen, scale)
        {
            this.cam = cam;
        }

        public async override void visit(Model model)
        {
            Console.WriteLine(Thread.CurrentThread.Name);
      
            MatrixCoord3D CamPosition = cam.Position.Coords;
            MatrixTransformation3D RotateMatrix = cam.RotateMatrix;
            PictureBuff.Creator = RenderType.RAY;

            MatrixCoord3D D = CanvasToVieport(InPoint.X, InPoint.Y) * RotateMatrix.InversedMatrix();// new MatrixCoord3D(cam.RotateMatrix.Coeff[0,2], cam.RotateMatrix.Coeff[1, 2], cam.RotateMatrix.Coeff[2, 2]);
            D.Normalise();
            find = RayT(model, D, CamPosition);
        }

        private ModelComponent RayT(Model model, MatrixCoord3D D, MatrixCoord3D position)
        {
            ModelComponent closest = null;
            double closest_t = double.MaxValue-1;
            //  Parallel.ForEach<PolygonComponent>(model.Polygons, p=> { 
            foreach (PolygonComponent p in model.Polygons)
            {

                MatrixCoord3D tt = GetTimeAndUvCoord(position, D, p.Points[0].Coords, p.Points[1].Coords, p.Points[2].Coords);
                if (tt != null)
                {
                    if (tt.X < closest_t && tt.X > 1)
                    {
                        closest_t = tt.X;
                        closest = p;
                    }
                }
            }
            if(closest == null)
        
                //MatrixCoord3D f =  GetTrilinearCoordinateOfTheHit(closest_t, position, D);
            foreach(PointComponent p in model.Points)
            {
                double tt = RaySphereIntersection(position, D, p.Coords,1); 
                if(tt!=null)
                {
                    if(tt<=closest_t && tt>1)
                    {
                        closest_t = tt;
                        closest = p;
                    }
                }
            }
            return closest;
        }

        private const double Epsilon = 0.000001d;

        double RaySphereIntersection(MatrixCoord3D rayOrigin, MatrixCoord3D rayDirection, MatrixCoord3D spos, double r)
        {
            double t = Double.MaxValue;
            //a == 1; // because rdir must be normalized
            MatrixCoord3D k = rayOrigin - spos;
            double b = MatrixCoord3D.scalar(k, rayDirection);
            double c = MatrixCoord3D.scalar(k, k) - r * r;
            double d = b * b - c;
            if (d >= 0)
            {
                double sqrtfd = Math.Sqrt(d);
                // t, a == 1
                double t1 = -b + sqrtfd;
                double t2 = -b - sqrtfd;
                double min_t = Math.Min(t1, t2);
                double max_t = Math.Max(t1, t2);
                t = (min_t >= 0) ? min_t : max_t;
            }
            return t;
        }

        public static MatrixCoord3D? GetTimeAndUvCoord(MatrixCoord3D rayOrigin, MatrixCoord3D rayDirection, MatrixCoord3D vert0, MatrixCoord3D vert1, MatrixCoord3D vert2)
        {
            var edge1 = vert1 - vert0;
            var edge2 = vert2 - vert0;

            var pvec = (rayDirection * edge2);

            var det = MatrixCoord3D.scalar(edge1, pvec);

            if (det > -Epsilon && det < Epsilon)
            {
                return null;
            }

            var invDet = 1d / det;

            var tvec = rayOrigin - vert0;

            var u = MatrixCoord3D.scalar(tvec, pvec) * invDet;

            if (u < 0 || u > 1)
            {
                return null;
            }

            var qvec = (tvec * edge1);

            var v = MatrixCoord3D.scalar(rayDirection, qvec) * invDet;

            if (v < 0 || u + v > 1)
            {
                return null;
            }

            var t = MatrixCoord3D.scalar(edge2, qvec) * invDet;

            return new MatrixCoord3D(t, u, v);
        }


        public static MatrixCoord3D GetTrilinearCoordinateOfTheHit(double t, MatrixCoord3D rayOrigin, MatrixCoord3D rayDirection)
        {
            return rayDirection * t + rayOrigin;
        }

        private MatrixCoord3D CanvasToVieport(int x, int y)
        {
            double aspectRatio = screen.Width / screen.Height;
            double fieldOfView = Math.Tan(cam.Fovy / 2 * Math.PI / 180.0f);

            double fx = aspectRatio * fieldOfView * (2 * ((x + 0.5f) / screen.Width) - 1);
            double fy = (1 - (2 * (y + 0.5f) / screen.Height)) * fieldOfView;

            return new MatrixCoord3D(fx / scale, fy / scale, -1);
        }
    }

    class EasyTransformVisitor : IVisitor
    {
        public TypeVisitor type { get; } = TypeVisitor.Transform;
        MatrixTransformation3D TransformMatrix;
        public EasyTransformVisitor(MatrixTransformation3D transformMatrix)
        {
            TransformMatrix = transformMatrix;
        }

        public void visit(PointComponent point)
        {
            point.Coords = point.Coords * TransformMatrix;
        }
        public void visit(LineComponent line)
        {
            line.Point1 = line.Point1 * TransformMatrix;
            line.Point2 = line.Point2 * TransformMatrix;
        }

        public void visit(PolygonComponent polygon)
        {
            foreach(PointComponent p in polygon.Points)
            {
                p.action(this);
            }
        }

        public void visit(Model model)
        {
            foreach (PointComponent p in model.Points)
            {
                p.action(this);
            }
        }
    }
    class HardTransformVisitor : IVisitor
    {
        public TypeVisitor type { get; } = TypeVisitor.Transform;
        MatrixTransformation3D TransformMatrix;
        PointComponent pointp;
        public HardTransformVisitor(MatrixTransformation3D transformMatrix, PointComponent point)
        {
            TransformMatrix = transformMatrix;
            this.pointp = point;
        }

        public void visit(PointComponent point)
        {
            MatrixTransformation3D transfer = new MatrixTransformationTransfer3D(-pointp.X, -pointp.Y, -pointp.Z);
            point.Coords = point.Coords * transfer;
            point.Coords = point.Coords * TransformMatrix;
            transfer = new MatrixTransformationTransfer3D(pointp.X, pointp.Y, pointp.Z);
            point.Coords = point.Coords * transfer;
        }
        public void visit(LineComponent line)
        {
            line.Point1.action(this);
            line.Point2.action(this);
        }

        public void visit(PolygonComponent polygon)
        {
            foreach (PointComponent p in polygon.Points)
            {
                p.action(this);
            }
        }

        public void visit(Model model)
        {
            foreach (PointComponent p in model.Points)
            {
                p.action(this);
            }
        }
    }

  
}

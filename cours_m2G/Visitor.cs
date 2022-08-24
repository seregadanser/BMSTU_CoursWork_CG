using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        protected Bitmap bmp;
        public Bitmap Bmp { get { return raster.Bmp; } }
        public override int Scale { get { return scale; } set { if (value > 0) { scale = value; raster.Scale = value; } } }
        public bool PrintText { get; set; } = true;
        protected Rasterizator raster;

        public virtual int SetRaster
        {
            set
            {
                if(value==0)
                    raster = new RasterizatorNoCutter(scale, screen, bmp);
                if (value == 1)
                    raster = new RasterizatorNoText(scale, screen, bmp);
                if(value == 2)
                    raster = new RasterizatorNoPoints(scale, screen, bmp);
            }
        }


        public DrawVisitor(Size screen, int scale, Bitmap bmp)
        {
            this.bmp = bmp;
            this.screen = screen;
            this.scale = scale;
            raster = new RasterizatorNoCutter(scale,screen,bmp);
        }

        public void Clear()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
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

        public override void visit(PolygonComponent polygon)
        {
            raster.DrawPolygon(polygon);
        }

        public override void visit(Model model)
        {
            foreach (PolygonComponent l in model.Polygons)
            {
                l.action(this);
            }
            foreach (LineComponent l in model.Lines)
            {
                l.action(this);
            }
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
                    raster = new RasterizatorNoCutter(cam, scale, screen, bmp);
                if (value == 1)
                    raster = new RasterizatorNoText(cam,scale, screen, bmp);
                if (value == 2)
                    raster = new RasterizatorNoPoints(cam,scale, screen, bmp);
            }
        }
        public DrawVisitorCamera(Size screen, int scale, Bitmap bmp, Camera cam) : base(screen, scale, bmp)
        {
            this.cam = cam;
            raster = new RasterizatorNoCutter(cam, scale, screen, bmp);
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
            p.Coords.X -= screen.Width / 2;
            p.Coords.Y = -(p.Coords.Y - screen.Height / 2);
            MatrixTransformation3D sc = new MatrixTransformationScale3D((double)scale, (double)scale, (double)scale);
            sc = sc.InversedMatrix();
            p.Coords = p.Coords * sc;
        }
        public override void visit(LineComponent l)
        {
            throw new NotImplementedException();
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
      
        public ReadVisitorCamera(Camera cam, Size screen, int scale) : base(screen, scale)
        {
            this.cam = cam;
        }

        public override void visit(PointComponent p)
        {
            MatrixTransformation3D sc = new MatrixTransformationScale3D(scale, scale, scale);
            MatrixCoord3D pNear = new MatrixCoord3D();
            pNear = new MatrixCoord3D((2.0 * p.X / screen.Width) - 1.0, 1.0 - (2.0 * p.Y / screen.Height), -1);
            //pNear.X = p.X;
            //pNear.Y = p.Y;
            pNear *= sc.InversedMatrix();
    //        pNear.Z = 0;
          //  pNear.Normalise();
            //pNear.X -= screen.Width / 2;
            //pNear.Y = -(pNear.Y - screen.Height / 2);

//            MatrixCoord3D pNear = new MatrixCoord3D((2.0*p.X/screen.Width)-1.0, 1.0 - (2.0*p.Y/screen.Height), -1);
//pNear *= sc.InversedMatrix();
            //pNear.X /= screen.Width / 2;
            //pNear.Y /= screen.Height / 2;
       //     pNear.Normalise();
            pNear.Z = -1;
          
           
            pNear *= cam.Projection.InversedMatrix();
            pNear.Z = -1;
            pNear.W = 0;
            pNear *= cam.LookAt.InversedMatrix();
            pNear.Normalise();
            p.Coords = pNear;


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

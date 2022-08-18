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
        
        public int Scale { get { return scale; } set { if(value>0) scale = value; } }
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
        protected PaintEventArgs e;
        public PaintEventArgs E { set { e = value; } }
        public bool PrintText { get; set; } = true;
        public DrawVisitor(PaintEventArgs e, Size screen, int scale)
        {
            this.e = e;
            this.screen = screen;
            this.scale = scale;
        }

        public override void visit(PointComponent point)
        {
            Pen pen = new Pen(point.Color);
            MatrixCoord3D p1 = actions(point);
            if (p1 != null)
                try
                {
                    if (PrintText)
                    {
                        string s = "{" + Convert.ToString(point.X) + " " + Convert.ToString(point.Y) + " " + Convert.ToString(point.Z) + "}";
                      //  e.Graphics.DrawString(s, new Font("Arial", 8), new SolidBrush(Color.Black), (int)p1.X, (int)p1.Y);
                     //   s = "[ "+point.Id.Description +" ]";
                    //    e.Graphics.DrawString(s, new Font("Arial", 8), new SolidBrush(Color.Blue), (int)p1.X, (int)p1.Y+11);
                    }
                  //  e.Graphics.DrawEllipse(pen, (int)(p1.X - point.HitRadius), (int)(p1.Y - point.HitRadius), (int)point.HitRadius * 2, (int)point.HitRadius * 2);
                }
                catch { }
        }
        public override void visit(LineComponent line)
        {
            Pen pen = new Pen(line.Color, 4);

            MatrixCoord3D p1 = actions(line.Point1);
            MatrixCoord3D p2 = actions(line.Point2);
            try
            {
                e.Graphics.DrawLine(pen, (int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);
            }
            catch { }

            //line.Point1.action(this);
            //line.Point2.action(this);
        }

        public override void visit(PolygonComponent polygon)
        {

        }

        public override void visit(Model model)
        {

        }

        protected virtual MatrixCoord3D actions(PointComponent point)
        {
            MatrixCoord3D? p1 = point.Coords;
            MatrixTransformation3D sc = new MatrixTransformationScale3D(scale, scale, scale);

            p1 *= sc;
            p1.X += screen.Width / 2;
            p1.Y = -(p1.Y - screen.Height / 2);
            return p1;
        }
    }

    class DrawVisitorCamera : DrawVisitor
    {
        Camera cam;
        public Camera Cam { get { return cam; } set { cam = value; } }
        R1 raster;
        public Bitmap GetResult() { return raster.GetResult(); }
        public PaintEventArgs E { set { raster.e = value; e = value; } }
        public DrawVisitorCamera(PaintEventArgs e, Camera cam, Size screen, int scale, Bitmap bmp) : base(e, screen, scale)
        {
            this.cam = cam;
            raster = new R1(bmp, screen, true,e);
        }

        public override void visit(LineComponent line)
        {
            Pen pen = new Pen(line.Color, 4);
            //if (cam.IsVieved(line.Point1.Coords) && cam.IsVieved(line.Point2.Coords))
            {
                MatrixCoord3D p1 = actions(line.Point1);
                MatrixCoord3D p2 = actions(line.Point2);
                if (p1 != null && p2 != null)
                    raster.drawLine(new List<PointComponent> { new PointComponent(p1), new PointComponent(p2) }, line.Color);
                    //Rasterizator.DrawLine(new PointComponent(p1), new PointComponent(p2), e.Graphics);
                //try
                //{
                //    e.Graphics.DrawLine(pen, (int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);
                //}
                //catch { }

                line.Point1.action(this);
                line.Point2.action(this);
            }
        }


        public override void visit(PolygonComponent polygon)
        {
            //foreach (LineComponent l in polygon.Lines)
            //{
            //    l.action(this);
            //}
            MatrixCoord3D p1 = actions(polygon.Points[0]);
            MatrixCoord3D p2 = actions(polygon.Points[1]);
            MatrixCoord3D p3 = actions(polygon.Points[2]);
            if (p1 != null && p2 != null && p3 != null)
             raster.drawTriangleFill(new List<PointComponent> { new PointComponent(p1), new PointComponent(p2), new PointComponent(p3) }, polygon.ColorF);
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

        protected override MatrixCoord3D actions(PointComponent point)
        {
            MatrixCoord3D? p1 = point.Coords;
            MatrixTransformation3D sc = new MatrixTransformationScale3D(scale, scale, scale);
            p1 = p1 * cam.LookAt;
            //MatrixTransformation3D projection = cam.LookAt * cam.Projection;
            //p1.W = 1;
            p1 = p1 * cam.Projection;
            if (p1.X < p1.W && p1.Y < p1.W && p1.Z < p1.W)
            {
                //p1.W = 1 / p1.W;
                p1.X = p1.X / p1.W;
                p1.Y = p1.Y / p1.W;
                p1.Z = p1.W;
            }
            else
            {
                return null;
            }

            p1.X *= screen.Width / 2;
            p1.Y *= screen.Height / 2;
            p1 *= sc;
            p1.X += screen.Width / 2;
            p1.Y = -(p1.Y - screen.Height / 2);


            return p1;
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

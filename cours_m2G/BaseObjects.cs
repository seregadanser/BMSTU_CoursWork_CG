namespace cours_m2G
{
    interface IBaseObjects
    {
        public void action(IVisitor visitor);
    }

    class Axes : IBaseObjects
    {
        readonly LineComponent XAxis, YAxis, ZAxis;
        public Axes()
        {
            PointComponent[] point = new PointComponent[6];

            point[0] = new PointComponent(0, 100, 0);
            point[1] = new PointComponent(0, -100, 0);
            point[2] = new PointComponent(100, 0, 0);
            point[3] = new PointComponent(-100, 0, 0);
            point[4] = new PointComponent(0, 0, 100);
            point[5] = new PointComponent(0, 0, -100);

            XAxis = new LineComponent(point[2], point[3]);
            YAxis = new LineComponent(point[0], point[1]);
            ZAxis = new LineComponent(point[4], point[5]);
            XAxis.Color = Color.Red;
            YAxis.Color = Color.Green;
            ZAxis.Color = Color.Blue;
        }
        public void action(IVisitor visitor)
        {
            XAxis.action(visitor);
            YAxis.action(visitor);
            ZAxis.action(visitor);
        }
    }

    class Camera : IBaseObjects
    {
        public PointComponent Position;
        public MatrixCoord3D Target;
        public MatrixCoord3D Direction;
        protected MatrixCoord3D Up;
        protected MatrixCoord3D Right;
        private MatrixTransformation3D RotationMatrix;
        public MatrixTransformation3D? LookAt { get; set; }

        private MatrixTransformation3D projection;
        private double fovy;
        private double aspect;
        public MatrixTransformation3D Projection { get { return projection; } set { projection = value; } }
        public double Fovy
        {
            get { return fovy; }
            set
            {
                fovy = value;
                if (projection.Type == MatrixType.Perspective)
                    projection = new MatrixPerspectiveProjection(fovy, aspect, 1, 1000);
            }
        }
        public double Aspect
        {
            get { return aspect; }
            set
            {
                aspect = value;
                if (projection.Type == MatrixType.Perspective)
                    projection = new MatrixPerspectiveProjection(fovy, aspect, 1, 1000);
            }
        }



        public Camera(PointComponent position, MatrixCoord3D Target, MatrixCoord3D Up)
        {
            this.Position = position;
            this.Target = Target;
            this.Up = Up;

            Direction = Position.Coords - Target;
            Direction.Normalise();
            Right = Up * Direction;
            Right.Normalise();
            Up = (Direction * Right);
            Up.Normalise();
            RotationMatrix = new MatrixAuxiliary(Right, Up, Direction);
            CountLookAt();
            aspect = 2;
            fovy = 90;
            projection = new MatrixPerspectiveProjection(fovy, Aspect, 1, 1000);

        }

        public Camera(PointComponent position, MatrixCoord3D Target, MatrixCoord3D Up, MatrixTransformation3D projection)
        {
            this.Position = position;
            this.Target = Target;
            this.Up = Up;

            Direction = Position.Coords - Target;
            Direction.Normalise();
            Right = Up * Direction;
            Right.Normalise();
            Up = (Direction * Right);
            Up.Normalise();
            RotationMatrix = new MatrixAuxiliary(Right, Up, Direction);
            CountLookAt();
            aspect = 2;
            fovy = 90;
            this.projection = projection;

        }

        public void action(IVisitor visitor)
        {
            Position.action(visitor);
            PointComponent D = new PointComponent(Position.Coords + Direction * 30);

            PointComponent R = new PointComponent(Position.Coords + Right * 30);

            PointComponent U = new PointComponent(Position.Coords + Up * 30);
            LineComponent LD = new LineComponent(Position, D);
            LD.Color = Color.Orange;
            LineComponent LR = new LineComponent(Position, R);
            LR.Color = Color.DarkGreen;
            LineComponent LU = new LineComponent(Position, U);
            LU.Color = Color.BlueViolet;
            LD.action(visitor);
            LR.action(visitor);
            LU.action(visitor);
            //CountParams();
        }

        public bool IsVieved(MatrixCoord3D point)
        {
            // if(point.Len()> Position.Coords.Len())
            {
                if (MatrixCoord3D.scalar(Position.Coords - point, Direction) < 0)
                    return false;
            }
            return true;
        }

        public void Move(int direction, double speed)
        {
            MatrixTransformation3D rotation;
            switch (direction)
            {
                case 0:
                    break;
                case 1:
                    Position.Coords += (Direction * speed);
                    break;
                case 2:
                    Position.Coords += (Right * (speed));
                    break;
                case 3:
                    Position.Coords += (Up * (speed));
                    break;

                case 4:
                    //вращение вокруг своей оси по Y
                    rotation = new MatrixTransformationRotateVec3D(Up,(int)speed);
                    Direction *= rotation;
                    Right *= rotation;
                    RotationMatrix = new MatrixAuxiliary(Right, Up, Direction);
                    break;
                case 5:
                    //вращение вокруг своей оси по X
                    rotation = new MatrixTransformationRotateVec3D(Right,(int)speed);
                    Direction *= rotation;
                    Up *= rotation;
                    RotationMatrix = new MatrixAuxiliary(Right, Up, Direction);
                    break;
                case 6:

                    break;
                case 7:



                    break;
                case 8:

                    break;

                case 9:
                    ////вращение по вокруг y относительно  0 0 0
                    MatrixTransformation3D r = new MatrixTransformationRotateY3D((int)speed);
                    Position.Coords = Position.Coords * r;
                    Direction = Direction * r;
                    Right = Right * r;
                    Up *= r;
                    RotationMatrix = new MatrixAuxiliary(Right, Up, Direction);
                    break;

                case 10:
                    ////вращение по вокруг y относительно  задаваемой точки
                    //MatrixTransformation3D transfer3 = new MatrixTransformationTransfer3D(-100, -0, -0);
                    //MatrixTransformation3D transfer4 = new MatrixTransformationTransfer3D(100, 0, 0);
                    //MatrixTransformation3D r1t = new MatrixTransformationRotateY3D(-(int)speed);
                    //Position.Coords = Position.Coords * transfer3;
                    //Position.Coords = Position.Coords * r1t;
                    //Position.Coords = Position.Coords * transfer4;

                    //Direction = Direction * r1t;
                    //Right = Right * r1t;

                    //RotationMatrix = new MatrixAuxiliary(Right, Up, Direction);


                    break;

            }
            CountLookAt();
        }
        private void CountLookAt()
        {

            LookAt = RotationMatrix * new MatrixAuxiliary(Position.Coords, Right, Up, Direction);
        }

    }


    class Pyramide : IBaseObjects
    {
        LineComponent[] lines;
        public Pyramide()
        {
            PointComponent[] point = new PointComponent[8];
            lines = new LineComponent[12];
            point[0] = new PointComponent(-5, 10, 5);
            point[1] = new PointComponent(-5, 10, -5);
            point[2] = new PointComponent(5, 10, -5);
            point[3] = new PointComponent(5, 10, 5);
            point[4] = new PointComponent(-10, -10, 10);
            point[5] = new PointComponent(-10, -10, -10);
            point[6] = new PointComponent(10, -10, -10);
            point[7] = new PointComponent(10, -10, 10);

            lines[0] = new LineComponent(point[0], point[1]);
            lines[1] = new LineComponent(point[1], point[2]);
            lines[2] = new LineComponent(point[2], point[3]);
            lines[3] = new LineComponent(point[3], point[0]);

            lines[4] = new LineComponent(point[4], point[5]);
            lines[5] = new LineComponent(point[5], point[6]);
            lines[6] = new LineComponent(point[6], point[7]);
            lines[7] = new LineComponent(point[7], point[4]);

            lines[8] = new LineComponent(point[0], point[4]);
            lines[9] = new LineComponent(point[1], point[5]);
            lines[10] = new LineComponent(point[2], point[6]);
            lines[11] = new LineComponent(point[3], point[7]);

        }
        public void action(IVisitor visitor)
        {
            foreach (LineComponent line in lines)
            {
                line.action(visitor);
            }
        }
    }
}

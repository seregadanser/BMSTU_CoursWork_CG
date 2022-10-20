using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cours_m2G
{
    class Scene
    {
        DrawVisitor Drawer;
        ReadVisitor Reader;
        Model model;
        Camera camera;

        Thread renderThread;
        CancellationTokenSource cancelTokenSource;
        PictureBox picture;

        public Scene(PictureBox picture)
        {
            camera = new Camera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 1, 0), new MatrixPerspectiveProjection(90, picture.Size.Width / (double)picture.Size.Height, 1, 1000));
            Drawer = new DrawVisitorCamera(picture.Size, 1, camera);
            Reader = new ReadVisitorCamera(camera, picture.Size, 1);
            model = new Cub(new PointComponent(0, 0, 0), 20);

            this.picture = picture;
            cancelTokenSource = new CancellationTokenSource();
            renderThread = new Thread(new ParameterizedThreadStart(RenderLoop));
            renderThread.Name = "drawing";
            renderThread.IsBackground = true;
            renderThread.Start(cancelTokenSource.Token);
        }
        public Scene(PictureBox picture, ObjReader re) : this(picture)  
        {
              model = re.ReadModel();    
        }

        #region Render
        Stopwatch strender = new Stopwatch();
        Stopwatch strefresh = new Stopwatch();
        public void RenderLoop(object boxedToken)
        {
            var cancellationToken = (CancellationToken)boxedToken;
            while (!cancellationToken.IsCancellationRequested)
            {
                strender.Start();
                model.action(Drawer);
                strender.Stop();
           
                TimeSpan ts = strender.Elapsed;
           
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RenderTime " + elapsedTime);
                strender.Reset();

                strefresh.Start();
                Refresh();
                strefresh.Stop();
                ts = strefresh.Elapsed;

                 elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                //Console.WriteLine("RefreshTime " + elapsedTime);
                strefresh.Reset();
      
            }
           

        }
        public void Refresh()
        {
            Bitmap LocalBMP = new Bitmap(picture.Size.Width,picture.Size.Height);
            if (PictureBuff.Filled)
                LocalBMP = Drawer.Bmp;
            if (picture.InvokeRequired)
            {
                picture.Invoke(new MethodInvoker(delegate
                {
                            picture.Image = LocalBMP;
                }));
            }
            Thread.Sleep(10);
        }

        public void ChangeRender(int rt)
        {
            switch (rt) {
                case 0:
                Drawer.SetRaster = 0;
                    break;
                case 1:
                Drawer.SetRaster = 1;
                    break;
                case 2:
                Drawer.SetRaster = 2;
                    break;
                case 3:
                Drawer.SetRaster = 3;
                    break;
                case 4:
                Drawer = new DrawVisitorR(picture.Size, 1, camera);
                    break;
                case 5:
                Drawer = new DrawVisitorCamera(picture.Size, 1, camera);
                    break;
            }
        }

        public void StartThread()
        {
            cancelTokenSource = new CancellationTokenSource();
            renderThread = new Thread(new ParameterizedThreadStart(RenderLoop));
            renderThread.Name = "drawing";
            renderThread.IsBackground = true;
            renderThread.Start(cancelTokenSource.Token);
        }
        public void StopThread()
        {
            cancelTokenSource.Cancel();
        }
       public Tuple<List<Id>, Id, PointComponent> Read(Point point)
        {
            Reader.InPoint = point;
            model.action(Reader);
            ModelComponent io = Reader.Find;
            PointComponent p = Reader.Findpoint;
            Tuple<List<Id>, Id, PointComponent> r;
            if (io != null)
                r = new Tuple<List<Id>, Id, PointComponent>(model.GetConnectedElements(io.Id), io.Id, p);
            else
                r = new Tuple<List<Id>, Id, PointComponent>(null, null, null);
            return r;
        }
        #endregion

        #region model
        public void AddComponent(IObjects objects)
        {
            model.AddComponent(objects);
        }
        public void AddComponent(Id LineId, PointComponent point)
        {
            model.AddPointToLine(LineId, point);
        }
        public void RemoveComponent(Id id)
        {
            StopThread();
            RemoveActiveComponent(id);
            model.RemovebyId(id);
            StartThread();
        }

        public bool AddComponentToActive(Id id)
        {
           return model.AddActiveComponent(id);
        }

        public void RemoveActiveComponent(Id id)
        {
            model.DeliteActive(id);
        }
        public void RemoveActiveComponent()
        {
            model.DeliteActive();
        }

        public void ModelAction(IVisitor action)
        {
            model.action(action);
        }

        public void ActiveMovement(string direction)
        {
            EasyTransformVisitor tr;
            MatrixTransformation3D trans = new MatrixTransformationTransfer3D(0, 0, 0);
            switch (direction)
            {
                case "south":
                    trans = new MatrixTransformationTransfer3D(-camera.Up.X, -camera.Up.Y, -camera.Up.Z);
                    break;
                case "north":
                    trans = new MatrixTransformationTransfer3D(camera.Up.X, camera.Up.Y, camera.Up.Z);
                    break;
                case "east":
                    trans = new MatrixTransformationTransfer3D(camera.Right.X, camera.Right.Y, camera.Right.Z);
                    break;
                case "west":
                    trans = new MatrixTransformationTransfer3D(-camera.Right.X, -camera.Right.Y, -camera.Right.Z);
                    break;
            }
            tr = new EasyTransformVisitor(trans);
            ModelAction(tr);
        }
        #endregion
        #region camera
        public void Scale(int delta)
        {
            if (delta > 0)
                Drawer.Scale++;
            else
                Drawer.Scale--;
        }

        public void Move(int dir)
        {
            switch(dir)
            {
                case 'W':
                    camera.Move(CameraDirection.FORWARD, 5);
                    break;
                case 'S':
                    camera.Move(CameraDirection.BACKWARD, 5);
                    break;
                case 'A':
                    camera.Move(CameraDirection.LEFT, 5);
                    break;
                case 'D':
                    camera.Move(CameraDirection.RIGHT, 5);
                    break;
                case 'Q':
                    camera.Move(CameraDirection.UP, 5);
                    break;
                case 'E':
                    camera.Move(CameraDirection.DOWN, 5);
                    break;
                case 'C':
                    camera.Move(CameraDirection.YAW, 1);
                    break;
                case 'V':
                    camera.Move(CameraDirection.YAW, -1);
                    break;
                case 'G':
                    camera.Move(CameraDirection.PICH, 1);
                    break;
                case 'F':
                    camera.Move(CameraDirection.PICH, -1);
                    break;
                case 'Z':
                    camera.Move(CameraDirection.ROTATIONY, 2);
                    break;
                case 'X':
                    camera.Move(CameraDirection.ROTATIONY, -2);
                    break;

            }
        }

        #endregion


    }
}

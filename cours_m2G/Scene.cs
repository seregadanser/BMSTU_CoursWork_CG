using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    public class Scene
    {
        Axes axes;
        Camera cam;
        IVisitor visitor;

        public Scene(PictureBox sc)
        {
            axes = new Axes();
            cam = new Camera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 1, 0));
            visitor = new DrawVisitorCamera(null, cam, sc.Size , 1);
        }

        public void movement(CameraDirection dir, int speed)
        {

            if (dir == CameraDirection.ROTATIONY)
                cam.Move(dir, speed);


        }
    }
}

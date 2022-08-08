namespace cours_m2G
{
    public partial class Form1 : Form
    {

        Camera[] cams;
        Camera curcam;
        Axes axes;
        Pyramide pyramide;
        DrawVisitor Drawer;
        ReadVisitor Reader;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            DoubleBuffered = true;
            pictureBox2.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);
            //cam = new DynamicCamera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 1, 0), -90, 0);
            cams = new Camera[3];
            cams[0] = new Camera(new PointComponent(0, 400, 0), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 0, 1));
            cams[2] = new Camera(new PointComponent(300, 300, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(-1, 1, -1));
            cams[1] = new Camera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 1, 0));//, new MatrixOrtoProjection(-300,300, -300, 300, 1, 1000));
            cams[1].Aspect = pictureBox2.Size.Width / pictureBox2.Size.Height;
           
            curcam = cams[1];
            axes = new Axes();
            pyramide = new Pyramide();
            Drawer = new DrawVisitorCamera(null, cams[1], pictureBox2.Size, 1);
            Reader = new ReadVisitorCamera(cams[1], pictureBox2.Size, 1);
            // MatrixTransformation3D ry = new MatrixTransformationScale3D(2, 2,2);
            //IVisitor v = new EasyTransformVisitor(ry);
            //pyramide.action(v);

            // Refresh();
        }

        bool flag = true;


        private void button1_Click(object sender, EventArgs e)
        {
            ////IVisitor vm = new HardTransformVisitor(new MatrixTransformationRotateX3D(1), new PointComponent(300,400,300));
            //IVisitor vm = new EasyTransformVisitor(new MatrixTransformationTransfer3D(1, 0, 0));
            //IVisitor vmm = new HardTransformVisitor(new MatrixTransformationRotateX3D(-1), new PointComponent(300, 400, 300));
            //IVisitor vn = new HardTransformVisitor(new MatrixTransformationRotateY3D(1), new PointComponent(300, 400, 300));
            //IVisitor vnn = new HardTransformVisitor(new MatrixTransformationRotateY3D(-1), new PointComponent(300, 400, 300));
            //int i = 0;
            ////while (true)
            //{
            //    if (i == 90)
            //        flag = false;
            //    if (i == 0)
            //        flag = true;
            //    if (flag)
            //    {
            //        i++;
            //        cam.action(vm);
            //        // cam.action(vn);
            //        //foreach (LineComponent line in line)
            //        //{ line.action(vm); line.action(vn); }
            //    }
            //    else
            //    {
            //        i--;
            //        cam.action(vmm);
            //        //cam.action(vnn);
            //        //foreach (LineComponent line in line)
            //        //{ line.action(vmm); line.action(vnn); }
            //    }
            //    Refresh();
            //    //pictureBox1.Refresh();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            // IVisitor v = new DrawVisitor(e, cams[0], this.Size, trackBar1.Value);
            //IVisitor v = new DrawVisitor(e, curcam, this.Size, trackBar1.Value);
            //axes.action(v);
            //pyramide.action(v);
            //foreach (Camera cam in cams)
            //{
            //    if (cam != curcam)
            //        cam.action(v);
            //}
            //cam.action(v);
            //curcam.action(v);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        int io = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //федоров алексей борисович
            if (e.KeyValue == 'I')
            {
                EasyTransformVisitor vvv = new EasyTransformVisitor(new MatrixTransformationTransfer3D(1, 0, 0));
                pyramide.action(vvv);
            }
            if (e.KeyValue == 'O')
            {
                EasyTransformVisitor vvv = new EasyTransformVisitor(new MatrixTransformationTransfer3D(-1, 0, 0));
                pyramide.action(vvv);
            }

            if (e.KeyValue == 'W')
            {
                curcam.Move(CameraDirection.FORWARD, 5);
            }
            if (e.KeyValue == 'S')
            {
                curcam.Move(CameraDirection.BACKWARD, 5);
            }
            if (e.KeyValue == 'A')
            {
                curcam.Move(CameraDirection.LEFT, 5);
            }
            if (e.KeyValue == 'D')
            {
                curcam.Move(CameraDirection.RIGHT, 5);
            }
            if (e.KeyValue == 'Q')
            {
                curcam.Move(CameraDirection.UP, 5);
            }
            if (e.KeyValue == 'E')
            {
                curcam.Move(CameraDirection.DOWN, 5);
            }

            if (e.KeyValue == 'Z')
            {
                curcam.Move(CameraDirection.ROTATIONY, 1);
            }
            if (e.KeyValue == 'X')
            {
                curcam.Move(CameraDirection.ROTATIONY, -1);
            }
            if (e.KeyValue == 'C')
            {
                curcam.Move(CameraDirection.YAW, 1);
            }
            if (e.KeyValue == 'V')
            {
                curcam.Move(CameraDirection.YAW, -1);
            }
            if (e.KeyValue == 'G')
            {
                curcam.Move(CameraDirection.PICH, 1);
            }
            if (e.KeyValue == 'F')
            {
                curcam.Move(CameraDirection.PICH, -1);
            }


            //Refresh();
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Refresh();
            cams[1].Fovy = trackBar1.Value;
            pictureBox2.Refresh();
        }
        bool firstMouse = true;

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            curcam = cams[0];

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            curcam = cams[1];

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            curcam = cams[2];

        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            IVisitor v = new DrawVisitorCamera(e, cams[0], pictureBox1.Size, trackBar3.Value);
            axes.action(v);
            pyramide.action(v);
            curcam.action(v);
            //foreach (PointComponent ppp in pp)
            //    ppp.action(Drawer);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

           
            Drawer.E = e;
            axes.action(Drawer);
            //foreach (PointComponent ppp in pp)
            //    ppp.action(Drawer);
           pyramide.action(Drawer);
        
        }
        List<PointComponent> pp = new List<PointComponent>();
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Text = Convert.ToString(e.X);
            label2.Text = Convert.ToString(e.Y);
            
            PointComponent p1 = new PointComponent(e.X, e.Y, 0);
            // p1 *= sc; 
            p1.action(Reader);
            label4.Text = Convert.ToString(p1.X);
            label5.Text = Convert.ToString(p1.Y);
            pp.Add(p1);
            for(int i = 0; i < 400; i++)
            {
                PointComponent po = new PointComponent(0,0,0);
                po.Coords =cams[1].Position.Coords + (p1.Coords * i);
                pp.Add(po);
            }
            p1.Color = Color.DarkRed;
     
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            cams[1].Projection = new MatrixOrtoProjection(-300, 300, -300, 300, trackBar4.Value, trackBar5.Value);
            pictureBox2.Refresh();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            cams[1].Projection = new MatrixOrtoProjection(-300, 300, -300, 300, trackBar4.Value, trackBar5.Value);
            pictureBox2.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
                if (e.Delta > 0)
                {
                    Drawer.Scale++;
                    Reader.Scale++;
                }
                else
                {
                    Drawer.Scale--;
                    Reader.Scale--;
                }
            pictureBox2.Refresh();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox2.Refresh();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pp.Clear();
        }
    }
}
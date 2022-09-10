using System.Diagnostics;

namespace cours_m2G
{
    delegate void Refresher();

    public partial class Form1 : Form
    {
        Camera curcam;
        DrawVisitor Drawer;
        Model cub;
        FormTransform f = new FormTransform();
       // Bitmap bmp;
        Thread renderThread;

        private CancellationTokenSource _cancellation;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            panel2.Visible = false;
            DoubleBuffered = true;
            pictureBox2.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);

            curcam = new Camera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 1, 0), new MatrixPerspectiveProjection(90, pictureBox2.Size.Width / pictureBox2.Size.Height, 1, 1000));//, new MatrixOrtoProjection(-300,300, -300, 300, 1, 1000));
          //  bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            Drawer = new DrawVisitorCamera(pictureBox2.Size, 1, curcam);
        
            cub = new Cub(new PointComponent(0, 0, 0), 20);
            reader = new ReadVisitorCamera(curcam, pictureBox2.Size, 1);
            ObjReader er = new ObjReader(@"D:\1.obj");
            //   cub = er.ReadModel();
        
            PictureBuff.Init(pictureBox2.Size, new Refresher(RefreshP));
            renderThread = new Thread(RenderLoop);
            renderThread.Name = "drawing";
            renderThread.IsBackground = true;
           renderThread.Start();

        }
        Stopwatch st = new Stopwatch();

        public void RenderLoop(/*object boxedToken*/)
        {
           // var cancellationToken = (CancellationToken)boxedToken;
            int k = 0;
             while (true)//!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine(Thread.CurrentThread.Name);
                st.Start();
                cub.action(Drawer);


                TimeSpan ts = st.Elapsed;
                st.Reset();
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RenderTime " + elapsedTime);
                k++;
             
            }
        }

        private void RefreshP()
        {
            st.Stop();
            st.Reset();
            st.Start();
            Bitmap LocalBMP = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            if (PictureBuff.Filled)
                lock (PictureBuff.locker)
                    LocalBMP = Drawer.Bmp;
            //  LocalBMP.Save("ss.png", ImageFormat.Png);
            if (pictureBox2.InvokeRequired)
            {
                pictureBox2.Invoke(new MethodInvoker(delegate
                {
                    {
                    
                 // lock (PictureBuff.locker)
                        pictureBox2.Image = LocalBMP;
                   
                    }
                }));
            }
            st.Stop();
            TimeSpan ts = st.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                   ts.Hours, ts.Minutes, ts.Seconds,
                   ts.Milliseconds / 10);
            Console.WriteLine("RefreshTime " + elapsedTime);
            st.Reset();
            Thread.Sleep(10);
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


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

            label1.Text = Convert.ToString(curcam.Position.Z);
            label2.Text = Convert.ToString(curcam.Direction.Z);
            //Refresh();
            pictureBox1.Refresh();
            pictureBox_Paint();
            //  pictureBox2.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox_Paint()
        {

            //muljanov@mail.ru - анализ алгоритмов

        }
        ReadVisitorCamera reader;
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

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

                }
                else
                {
                    Drawer.Scale--;

                }
            pictureBox_Paint();
            //  pictureBox2.Refresh();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //   pictureBox2.Refresh();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            int max = 0;
            switch (comboBox1.SelectedItem)
            {
                case "Point":
                    max = cub.NumberPoints;
                    break;
                case "Line":
                    max = cub.NumberLines;
                    break;
                case "Polygon":
                    max = cub.NumberPolygons;
                    break;
            }
            for (int i = 1; i <= max; i++)
                comboBox2.Items.Add(i);

            comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cub.AddActiveComponent(Convert.ToString(comboBox1.SelectedItem), Convert.ToInt32(comboBox2.SelectedItem));
            pictureBox2.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cub.DeliteActive();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cub.DeliteActive();
            button6.Visible = true;
            panel2.Visible = false;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            cub = new Cub(new PointComponent(0, 0, 0), 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Form activeform = new Form();
            // Label[] labes = new Label[cub.ActiveComponentsId.Count];
            // int startposx= 5, startposy=10;
            // for (int i = 0; i < labes.Length; i++)
            // {   
            //     labes[i] = new Label();
            //     labes[i].AutoSize = true;
            //     labes[i].Text = cub.ActiveComponentsId[i].Name + " " + cub.ActiveComponentsId[i].Description;
            //     labes[i].Location =new Point( startposx, startposy + labes[i].Size.Height * i + 1);

            //     activeform.Controls.Add(labes[i]);
            // }
            //activeform.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            int max = 0;
            switch (comboBox3.SelectedItem)
            {
                case "Point":
                    max = cub.NumberPoints;
                    break;
                case "Line":
                    max = cub.NumberLines;
                    break;
                case "Polygon":
                    max = cub.NumberPolygons;
                    break;
            }
            for (int i = 1; i <= max; i++)
                comboBox4.Items.Add(i);

            comboBox4.SelectedIndex = 0;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            cub.AddActiveComponent(Convert.ToString(comboBox3.SelectedItem), Convert.ToInt32(comboBox4.SelectedItem));
            panel2.Visible = true;
            button6.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cub.DeliteActive();
            button6.Visible = true;
            panel2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Id i = new Id(comboBox3.SelectedItem.ToString(), cub.ActiveComponentsId[0].Description);
            cub.DeliteActive();
            switch (comboBox3.SelectedItem)
            {
                case "Point":
                    cub.RemovePoint(i);
                    break;
                case "Line":
                    cub.RemoveLine(i);
                    break;
                case "Polygon":
                    cub.RemovePolygon(i);
                    break;
            }
            panel2.Visible = false;
            button6.Visible = true;
            comboBox4.Items.Clear();
            int max = 0;
            switch (comboBox3.SelectedItem)
            {
                case "Point":
                    max = cub.NumberPoints;
                    break;
                case "Line":
                    max = cub.NumberLines;
                    break;
                case "Polygon":
                    max = cub.NumberPolygons;
                    break;
            }
            for (int ie = 1; ie <= max; ie++)
                comboBox4.Items.Add(ie);

            if (max > 0)
                comboBox4.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cub.action(f.transformvisitor);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //renderThread = new Thread(RenderLoop);
            //renderThread.Name = "drawing";
            //renderThread.IsBackground = true;
            //renderThread.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "nc")
                Drawer.SetRaster = 0;
            if (textBox1.Text == "nt")
                Drawer.SetRaster = 1;
            if (textBox1.Text == "np")
                Drawer.SetRaster = 2;
            if (textBox1.Text == "b")
                Drawer.SetRaster = 3;
            if (textBox1.Text == "ray")
                Drawer = new DrawVisitorR(pictureBox2.Size, 1, curcam);
            if (textBox1.Text == "raster")
                Drawer = new DrawVisitorCamera(pictureBox2.Size, 1, curcam);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(ModifierKeys != Keys.ShiftKey)
                cub.DeliteActive();

            reader.InPoint = e.Location;
            cub.action(reader);
            ModelComponent io = reader.Find;
            if (io != null)
            {
                cub.GetConnectedElements(io.Id);
                
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Drawer.NumberofThreads = trackBar1.Value;
        }

        private void One_Click(object sender, EventArgs e)
        {

        }
    }
}
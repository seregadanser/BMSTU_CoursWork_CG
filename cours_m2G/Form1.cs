using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace cours_m2G
{
    delegate void Refresher();
    delegate void Action(Id id);
    delegate void CallBack();

    public partial class Form1 : Form
    {
        Camera curcam;
        DrawVisitor Drawer;
        ReadVisitor reader;
        Model cub;
        FormTransform f;
        ActiveElementsForm f1;
       // Bitmap bmp;
        Thread renderThread;
        CancellationTokenSource cancelTokenSource;
        private CancellationTokenSource _cancellation;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
          
            DoubleBuffered = true;
            pictureBox2.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);

            curcam = new Camera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 1, 0), new MatrixPerspectiveProjection(90, pictureBox2.Size.Width / pictureBox2.Size.Height, 1, 1000));//, new MatrixOrtoProjection(-300,300, -300, 300, 1, 1000));
            Drawer = new DrawVisitorCamera(pictureBox2.Size, 1, curcam);
        
            cub = new Cub(new PointComponent(0, 0, 0), 20);
            reader = new ReadVisitorCamera(curcam, pictureBox2.Size, 1);
            ObjReader er = new ObjReader(@"D:\1.obj");
         //   cub = er.ReadModel();
        
            PictureBuff.Init(pictureBox2.Size.Width, pictureBox2.Size.Height, new Refresher(RefreshP));

            cancelTokenSource = new CancellationTokenSource();
            renderThread = new Thread(new ParameterizedThreadStart(RenderLoop));
            renderThread.Name = "drawing";
            renderThread.IsBackground = true;
            renderThread.Start(cancelTokenSource.Token);

            f1 = new ActiveElementsForm(new Action(DelitFromModel), new Action(DelitFromActive), new CallBack(ShowActiveElemButton));
        }

        //protected override void WndProc(ref Message m)
        //{
        //    const int WM_NCLBUTTONDBLCLK = 0x00A3;
        //    const int WM_LBUTTONDOWN = 0x0201;
        //    base.WndProc(ref m);
        //    Console.WriteLine(m);
        //    switch (m.Msg)
        //    {
        //        case WM_NCLBUTTONDBLCLK:
        //            MessageBox.Show("Произошел двойной щелчок вне клиентской области");
        //            return;
        //        case WM_LBUTTONDOWN:
        //            MessageBox.Show("Произошел щелчок вне клиентской области");
        //            return;
        //    }
        //}
        #region Render
        Stopwatch st = new Stopwatch();
        Stopwatch st1 = new Stopwatch();
        public void RenderLoop(object boxedToken)
        {
           
             var cancellationToken = (CancellationToken)boxedToken;
            int k = 0;
             while (!cancellationToken.IsCancellationRequested)
            {
             
                st1.Start();
                cub.action(Drawer);
                TimeSpan ts = st1.Elapsed;
                st1.Reset();

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RenderTime " + elapsedTime);
                k++;
             
            }
        }
        public object locker = new();
        private void RefreshP()
        {
            st1.Stop();
            st.Reset();
            st.Start();
            lock (locker)
            {
                Bitmap LocalBMP = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                if (PictureBuff.Filled)
                    LocalBMP = Drawer.Bmp;
                //  LocalBMP.Save("ss.png", ImageFormat.Png);
                if (pictureBox2.InvokeRequired)
                {
                    pictureBox2.Invoke(new MethodInvoker(delegate
                    {
                        {

                            {
                                pictureBox2.Image = LocalBMP;
                            }


                        }
                    }));
                }
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
        #endregion
        #region SceneActions
        private void ShowActiveElemButton()
        {
            button13.Visible = true;
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
        private void button11_Click(object sender, EventArgs e)
        {
            cancelTokenSource.Cancel();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            renderThread = new Thread(new ParameterizedThreadStart(RenderLoop));
            renderThread.Name = "drawing";
            renderThread.IsBackground = true;
            renderThread.Start(cancelTokenSource.Token);
        }
        #endregion
        #region ObjectChoose
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MouseEventArgs ee = (MouseEventArgs)e;
            reader.InPoint = ee.Location;
            cub.action(reader);
            ModelComponent io = reader.Find;
            if (io != null)
            {
                SetActiveWindow(cub.GetConnectedElements(io.Id), io.Id);
            }
            else
            {
                DelActiveWindow();
            }
        }
        private void SetActiveWindow(List<Id> ids, Id ci)
        {
            button6.Visible = true;
            comboBox5.Items.Clear();
            comboBox5.Items.Add(ci.ToString());
            foreach (Id i in ids)
                comboBox5.Items.Add(i.ToString());
            comboBox5.SelectedIndex = 0;
        }
        private void DelActiveWindow()
        {
            comboBox5.Text = "";
            comboBox5.Items.Clear();
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
           
            string g = comboBox5.Text;
            if (g != "")
            {
                string[] g1 = g.Split(new char[] { ' ' });
                Id id = new Id(g1[0], g1[1]);
               
               if( f1.AddActive(id, cub.GetConnectedElements(id)))
                  cub.AddActiveComponent(id);
            }
            DelActiveWindow();
            button6.Visible = false;
        }

        private void DelitFromModel(Id id)
        {
                DelitFromActive(id);
                cub.RemovebyId(id);
        }
        private void DelitFromActive(Id id)
        {
            cub.DeliteActive(id);
        }


        private void button13_Click(object sender, EventArgs e)
        {
            f1 = new ActiveElementsForm(new Action(DelitFromModel), new Action(DelitFromActive), new CallBack(ShowActiveElemButton));
            
            foreach (Id i in cub.ActiveComponentsId)
                f1.AddActive(i, cub.GetConnectedElements(i));
            f1.Show();
            button13.Visible = false;
        }
        #endregion


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

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
      
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //   pictureBox2.Refresh();
        }


   
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 


        private void button5_Click(object sender, EventArgs e)
        {
            cub = new Cub(new PointComponent(0, 0, 0), 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            f = new FormTransform();
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


        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if(ModifierKeys != Keys.ShiftKey)
            //    cub.DeliteActive();

            //reader.InPoint = e.Location;
            //cub.action(reader);
            //ModelComponent io = reader.Find;
            //if (io != null)
            //{
            //  SetActiveWindow(cub.GetConnectedElements(io.Id));
            //}
        }

    
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
        }

        private void One_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
         
        }

      
    }
}
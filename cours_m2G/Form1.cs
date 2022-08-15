namespace cours_m2G
{
    public partial class Form1 : Form
    {
        string whatobj = "Model";
        int numobj;
        Camera[] cams;
        Camera curcam;
        Axes axes;
        Pyramide pyramide;
        DrawVisitor Drawer;
        ReadVisitor Reader;
        Model cub;
        FormTransform f = new FormTransform();
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
           panel2.Visible = false;
            DoubleBuffered = true;
            pictureBox2.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);
            //cam = new DynamicCamera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 1, 0), -90, 0);
            cams = new Camera[3];
            cams[0] = new Camera(new PointComponent(0, 400, 0), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 0, 1), new MatrixPerspectiveProjection(90, pictureBox2.Size.Width / pictureBox2.Size.Height, 1, 1000));
            cams[2] = new Camera(new PointComponent(300, 300, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(-1, 1, -1), new MatrixPerspectiveProjection(90, pictureBox2.Size.Width / pictureBox2.Size.Height, 1, 1000));
            cams[1] = new Camera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 1, 0), new MatrixPerspectiveProjection(90, pictureBox2.Size.Width / pictureBox2.Size.Height, 1, 1000));//, new MatrixOrtoProjection(-300,300, -300, 300, 1, 1000));

            //cams[1] = new Camera(new PointComponent(0, 0, 300), new MatrixCoord3D(0, 0, 0), new MatrixCoord3D(0, 1, 0), new MatrixOrtoProjection(0-pictureBox2.Size.Width/2, 0+ pictureBox2.Size.Width / 2, 0 - pictureBox2.Size.Height / 2, 0 + pictureBox2.Size.Height / 2, 1, 1000));
            curcam = cams[1];
            axes = new Axes();
            pyramide = new Pyramide();
            Drawer = new DrawVisitorCamera(null, cams[1], pictureBox2.Size, 1);
            Reader = new ReadVisitorCamera(cams[1], pictureBox2.Size, 1);
            cub = new Cub(new PointComponent(0, 0, 0), 20);
            // MatrixTransformation3D ry = new MatrixTransformationScale3D(2, 2,2);
            //IVisitor v = new EasyTransformVisitor(ry);
            //pyramide.action(v);

            // Refresh();
        }

        bool flag = true;


    

    

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
                cub.action(vvv);
            }
            if (e.KeyValue == 'O')
            {
                EasyTransformVisitor vvv = new EasyTransformVisitor(new MatrixTransformationTransfer3D(-1, 0, 0));
                cub.action(vvv);
            }
            if (e.KeyValue == 'L')
            {
              //  cub.DD();
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

    
   

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
         
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {

            
            Drawer.E = e;
            cub.action(Drawer);
        
            // axes.action(Drawer);
            // foreach (PointComponent ppp in pp)
            //  ppp.action(Drawer);
            //pyramide.action(Drawer);

        }
        List<PointComponent> pp = new List<PointComponent>();
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
         
            
            PointComponent p1 = new PointComponent(e.X, e.Y, 0);
            // p1 *= sc; 
            p1.action(Reader);
          
           bool jj =  axes.isget(curcam.Position.Coords, p1.Coords);
        
            //pp.Add(p1);
            //for (int i = 0; i < 400; i++)
            //{
            //    PointComponent po = new PointComponent(0, 0, 0);
            //    po.Coords = cams[1].Position.Coords + (p1.Coords * i);
            //    pp.Add(po);
            //}
            //p1.Color = Color.DarkRed;

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
            pictureBox2.Refresh();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cub.DeliteActive();
            button6.Visible = true;
            panel2.Visible = false;
            pictureBox2.Refresh();
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
            pictureBox2.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cub.DeliteActive();
            button6.Visible = true;
            panel2.Visible = false;
            pictureBox2.Refresh();
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

            if(max>0)
            comboBox4.SelectedIndex = 0;
            pictureBox2.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cub.action(f.transformvisitor);
            pictureBox2.Refresh();
        }
    }
}
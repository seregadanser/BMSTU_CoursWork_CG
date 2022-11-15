using System.Diagnostics;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Net.Mime.MediaTypeNames;

namespace cours_m2G
{
    
    delegate void NewP(Id LineId, PointComponent point);
    

     partial class Form1 : Form
    {

        Scene scene;
        FormTransform f;
        public ActiveElementsForm f1;
        bool flag_add_poly = false;
        int what = 0;
        public CallBackDelegates del;
        ObjReader er;
        public Form1()
        {
            InitializeComponent();
            pictureBox2.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);

            KeyPreview = true;
             
            DoubleBuffered = true;
           
            PictureBuff.Init(pictureBox2.Size);
            scene = new Scene(pictureBox2);
            scene.l2 = label2;




             del = new CallBackDelegates()
            {
                remove_active = scene.RemoveActiveComponent,
                remove_object = scene.RemoveComponent,
                close = ShowActiveElemButton       
            };
     
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
       
       
       
        #region SceneActions
        private void ShowActiveElemButton()
        {

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys != Keys.Control)
                scene.Move(e.KeyValue);
            else
            {
                if (e.KeyCode == Keys.Z)
                    scene.FromStack();
            }
        }

        private void pictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
                scene.Scale(e.Delta);
            else
            {
             
            }
        }
       
        private void button11_Click(object sender, EventArgs e)
        {
            scene.StopThread();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            scene.StartThread();
        }
        #endregion
        #region ObjectChoose
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MouseEventArgs ee = (MouseEventArgs)e;
            if(ee.Button == MouseButtons.Right)
            {
                    scene.NewP(ee.Location);
                return;
            }
            Tuple<List<Id>, Id, PointComponent> r = new Tuple<List<Id>, Id, PointComponent>(null, null, null);
            if (flag_add_poly)
            {
                if (what == 4)
                    label9.Text = scene.Read(ee.Location, 1).Item2.ToString();
                else
                {
                    Id i = scene.NewPolygon(ee.Location);
                    if (i != null)
                    {
                        if (what == 1)
                            label3.Text = i.ToString();
                        if (what == 2)
                            label6.Text = i.ToString();
                        if (what == 3)
                            label8.Text = i.ToString();

                    }
                }
                flag_add_poly = false;
            }
            else
            {
                if (Points.Checked)
                    r = scene.Read(ee.Location, 1);
                if (Lines.Checked)
                    r = scene.Read(ee.Location, 2);
                if (Polys.Checked)
                    r = scene.Read(ee.Location, 3);



                if (r.Item2 != null)
                    SetActiveWindow(r.Item1, r.Item2);
                else
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

                if (scene.AddComponentToActive(id))
                {
                    f1.Update();
                    f1.AddActive(id);
                }
            }
            DelActiveWindow();
            button6.Visible = false;
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

 
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //   pictureBox2.Refresh();
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

  

        private void button5_Click(object sender, EventArgs e)
        {
            if(er!=null)
                scene.RebildFigure(er);
            else
                scene.RebildFigure();
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
           // Mstack.Push(ObjectCopier.Clone(cub));
            scene.ModelAction(f.transformvisitor);
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
         //   curcam.Fovy = trackBar1.Value;
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

        bool MousePressed = false;
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            MousePressed = true;
        }
        private Point lastPoint;
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            var dx = e.Location.X - lastPoint.X;
            var dy = e.Location.Y - lastPoint.Y;
            string direction = "";
            if (Math.Abs(dy) > Math.Abs(dx))
                if (dy > 0)
                    direction = "south";
                else
                    direction = "north";
            else
                if (dx > 0)
                direction = "east";
            else
                direction = "west";


            if (MousePressed)
            {
                scene.ActiveMovement(direction);
            }
            //else
            // if (ModifierKeys == Keys.Control)
            //{
            //    switch (direction)
            //    {
            //        case "east":
            //            scene.Move('X');
            //            break;
            //        case "west":
            //            scene.Move('Z');
            //            break;
            //    }
            //}
            lastPoint = e.Location;
        }
   

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            scene.DropCam();
        }

        private void выборЭлементовToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Polys_Click(object sender, EventArgs e)
        {
            Lines.Checked = false;
            Points.Checked = false;
            Polys.Checked = true;
        }

        private void Lines_Click(object sender, EventArgs e)
        {
            Lines.Checked = true;
            Points.Checked = false;
            Polys.Checked = false;
        }

        private void Points_Click(object sender, EventArgs e)
        {
            Lines.Checked = false;
            Points.Checked = true;
            Polys.Checked = false;
        }

        private void Interpolation_Click(object sender, EventArgs e)
        {
            Interpolation.Checked = true;
            Bariscentric.Checked = false;
            NoCutter.Checked = false;
            Parallel.Checked = false;
            StepbyStep.Checked = false;
            scene.ChangeRender(5);
            scene.ChangeRender(1);
        }

        private void Bariscentric_Click(object sender, EventArgs e)
        {
            Interpolation.Checked = false;
            Bariscentric.Checked = true;
            NoCutter.Checked = false;
            Parallel.Checked = false;
            StepbyStep.Checked = false;
            scene.ChangeRender(5);
            scene.ChangeRender(2);
        }

        private void NoCutter_Click(object sender, EventArgs e)
        {
            Interpolation.Checked = false;
            Bariscentric.Checked = false;
            NoCutter.Checked = true;
            Parallel.Checked = false;
            StepbyStep.Checked = false;
            scene.ChangeRender(5);
            scene.ChangeRender(0);
        }

        private void Parallel_Click(object sender, EventArgs e)
        {
            Interpolation.Checked = false;
            Bariscentric.Checked = false;
            NoCutter.Checked = false;
            Parallel.Checked = true;
            StepbyStep.Checked = false;
            scene.ChangeRender(4);
            scene.ChangeRender(0);
        }

        private void StepbyStep_Click(object sender, EventArgs e)
        {
            Interpolation.Checked = false;
            Bariscentric.Checked = false;
            NoCutter.Checked = false;
            Parallel.Checked = false;
            StepbyStep.Checked = true;
            scene.ChangeRender(4);
            scene.ChangeRender(1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = "Выберите точку";
            textBox1.Visible = false;
            button2.Visible = false;
            flag_add_poly = true;
            what = 1;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label6.Text = "Выберите точку";
            textBox2.Visible = false;
            button3.Visible = false;
            flag_add_poly = true;
            what = 2;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.Text = "Выберите точку";
            textBox3.Visible = false;
            button7.Visible = false;
            flag_add_poly = true;
            what = 3;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            button2.Visible = true;
            textBox2.Visible = true;
            button3.Visible = true;
            textBox3.Visible = true;
            button7.Visible = true;
            label3.Visible = true;
            label6.Visible = true;
            label8.Visible = true;
            textBox1.Text = "Добавить новую";
            textBox2.Text = "Добавить новую";
            textBox3.Text = "Добавить новую";
            label3.Text = "Добавить существующую";
            label6.Text = "Добавить существующую";
            label8.Text = "Добавить существующую";
            
            what = 0;
            scene.NewPolygon();
        }

        private PointComponent PParserer(string text)
        {
            string[] words = text.Split(new char[] { ' ' });
            double[] points = new double[3];
            int i = 0;
            foreach (string s in words)
            {
                try
                {
                    points[i] = Convert.ToDouble(s);
                }
                catch { return null; }
                i++;
            }
            return new PointComponent(points[0], points[1], points[2]);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
          PointComponent p = PParserer(textBox1.Text);
            if (p == null)
            { textBox1.Text = "неправильный формат"; return; }
            scene.NewPolygon(p);
            label3.Visible = false;
            button2.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointComponent p = PParserer(textBox2.Text);
            if (p == null)
            { textBox2.Text = "неправильный формат"; return; }
            scene.NewPolygon(p);
            label6.Visible = false;
            button3.Visible = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            PointComponent p = PParserer(textBox3.Text);
            if (p == null)
            { textBox3.Text = "неправильный формат"; return; }
            scene.NewPolygon(p);
            label8.Visible = false;
            button7.Visible = false;

        }

        private void label9_Click(object sender, EventArgs e)
        {
            flag_add_poly = true;
            what = 4;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            PointComponent p = PParserer(textBox4.Text);
            if (p == null)
            { textBox1.Text = "неправильный формат"; return; }
            string[] g1 = label9.Text.Split(new char[] { ' ' });
            Id id = new Id(g1[0], g1[1]);
            scene.newCoords(id, new MatrixCoord3D(p.X, p.Y, p.Z));
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
        //    scene.Resize(pictureBox2.Size);

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            er = new ObjReader(filename);
            scene.StopThread();
            scene = new Scene(pictureBox2, er);
            scene.l2 = label2;

        }
    }
}
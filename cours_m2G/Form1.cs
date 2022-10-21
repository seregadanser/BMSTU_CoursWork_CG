using System.Diagnostics;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace cours_m2G
{
    
    delegate void NewP(Id LineId, PointComponent point);
    

    public partial class Form1 : Form
    {

        Scene scene;
        ReadVisitor reader;

        FormTransform f;
        ActiveElementsForm f1;

        Stack<Model> Mstack;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
           ObjReader er = new ObjReader(@"D:\1.obj");
            DoubleBuffered = true;
            pictureBox2.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);
            PictureBuff.Init(pictureBox2.Size);
          //  scene = new Scene(pictureBox2, er);
            scene = new Scene(pictureBox2);



            Mstack = new Stack<Model>(10);

            CallBackDelegates del = new CallBackDelegates()
            {
                remove_active = scene.RemoveActiveComponent,
                remove_object = scene.RemoveComponent,
                close = ShowActiveElemButton
            };
            f1 = new ActiveElementsForm(del);
            f1.Show();
        
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
            button13.Visible = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys != Keys.Control)
                scene.Move(e.KeyValue);     
        }

        private void pictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
                scene.Scale(e.Delta);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            { scene.ChangeRender(Convert.ToInt32(textBox1.Text)); }
            catch
            { }
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
            Tuple<List< Id >, Id, PointComponent > r = scene.Read(ee.Location);
            if(r.Item2!=null)
                SetActiveWindow(r.Item1, r.Item2);
            else
                DelActiveWindow();
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

        private void button13_Click(object sender, EventArgs e)
        {
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

 
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //   pictureBox2.Refresh();
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {

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
            else
             if (ModifierKeys == Keys.Control)
            {
                switch (direction)
                {
                    case "east":
                        scene.Move('X');
                        break;
                    case "west":
                        scene.Move('Z');
                        break;
                }
            }
            lastPoint = e.Location;
        }
   

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
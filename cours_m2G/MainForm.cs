using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cours_m2G
{
    public partial class MainForm : Form
    {
        Form1 F1;
        ActiveElementsForm F2;
        public MainForm()
        {
         InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            F1 = new Form1();
            F1.MdiParent = this;
           
            F2 = new ActiveElementsForm(F1.del);
            F2.MdiParent = this;
           
            F1.f1 = F2;
            F1.Show();
            F2.Show();
         
            LayoutMdi(MdiLayout.TileHorizontal);
        }
    }
}

using cours_m2G;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
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
    delegate void calback(Id id);
    partial class ActiveElementsForm : Form
    {
        List<Elem> elements;
        Action CallBack_Remove, CalBack_Delit;
        NewP nep;
        CallBack call;
        public ActiveElementsForm(Action remove, Action delite,NewP newp , CallBack call)
        {
            InitializeComponent();
            CallBack_Remove = remove;
            CalBack_Delit = delite;
            this.call = call;
            nep = newp;
            elements = new List<Elem>();

        }

        private void ActiveElementsForm_Load(object sender, EventArgs e)
        {

        }

        private void Update1(Id id)
        {
            for (int i = 0; i < elements.Count; i++)
                if (elements[i].id == id)
                {
                    TableLayoutHelper.RemoveArbitraryRow(tableLayoutPanel1, i);
                    elements.RemoveAt(i);
                    return;
                }
        }

        public void Del()
        {
            while (tableLayoutPanel1.Controls.Count > 0)
            {
                tableLayoutPanel1.Controls[0].Dispose();
            }
        }

        private void ActiveElementsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            call.Invoke();
        }

        public bool AddActive(Id id)
        {
            tableLayoutPanel1.RowCount++;
            elements.Add(new Elem(id, elements.Count, tableLayoutPanel1, CallBack_Remove, CalBack_Delit, nep,new calback(Update1)));
            return true;
        }

   
    }
    class Elem
    {
        calback calback;
        Label name;
        Button rem;
        Button del;
        Button add;
        public Id id;
        Action delDeleg, remDeleg;
        NewP np;
        public Elem(Id id, int num, TableLayoutPanel main, Action remove, Action delit, NewP np, calback calback)
        {
            this.id = id;
            delDeleg = delit;
            remDeleg = remove;
            this.np = np;
            name = new Label();
            name.AutoSize = true;
            name.Text = id.ToString();
            rem = new Button();
            rem.Click += new EventHandler(RemClic);
            rem.AutoSize = true;
            rem.Text = "Remove from Model";
            del = new Button();
            del.AutoSize = true;
            del.Text = "Delit from Active";
            del.Click += new EventHandler(DelClic);
            main.Controls.Add(name, 0, num);
            main.Controls.Add(rem, 1, num);
            main.Controls.Add(del, 2, num);
            this.calback = calback;
            if (id.Name == "Line")
            {
                add = new Button();
                add.Click += new EventHandler(NewPointClick);
                add.AutoSize = true;
                add.Text = "Add point to line";
                main.Controls.Add(add, 3, num);
            }
        }

        private void RemClic(object sender, EventArgs e)
        {
            calback.Invoke(id);
            remDeleg.Invoke(id);
        }
        private void DelClic(object sender, EventArgs e)
        {
            delDeleg.Invoke(id);
            calback.Invoke(id);
        }
        private void NewPointClick(object sender, EventArgs e)
        {
            np.Invoke(this.id, new PointComponent(Convert.ToDouble(5), Convert.ToDouble(-10), Convert.ToDouble(20)));
            calback.Invoke(id);
        }
        public void Update(int pos, TableLayoutPanel p)
        {
          
        }
    }


    static class TableLayoutHelper
    {
        public static void RemoveArbitraryRow(TableLayoutPanel panel, int rowIndex)
        {
            if (rowIndex >= panel.RowCount)
            {
                return;
            }

            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                var control = panel.GetControlFromPosition(i, rowIndex);
                panel.Controls.Remove(control);
            }

            // move up row controls that comes after row we want to remove
            for (int i = rowIndex + 1; i < panel.RowCount; i++)
            {
                for (int j = 0; j < panel.ColumnCount; j++)
                {
                    var control = panel.GetControlFromPosition(j, i);
                    if (control != null)
                    {
                        panel.SetRow(control, i - 1);
                    }
                }
            }

            var removeStyle = panel.RowCount - 1;

            if (panel.RowStyles.Count > removeStyle)
                panel.RowStyles.RemoveAt(removeStyle);

            panel.RowCount--;
        }
    }
}
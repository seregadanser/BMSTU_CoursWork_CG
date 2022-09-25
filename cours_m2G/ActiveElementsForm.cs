using cours_m2G;
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
        List<List<Id>> forbiden_elems;
        Action CallBack_Remove, CalBack_Delit;
        CallBack call;
        public ActiveElementsForm(Action remove, Action delite, CallBack call)
        {
            InitializeComponent();
            CallBack_Remove = remove;
            CalBack_Delit = delite;
            this.call = call;
            elements = new List<Elem>();
            forbiden_elems = new List<List<Id>>();
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
                    forbiden_elems.RemoveAt(i);
                    return;
                }
        }

        private void ActiveElementsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            call.Invoke();
        }

        public bool AddActive(Id id, List<Id> forbiden_elems1)
        {
            for (int i = 0; i < elements.Count; i++)
                if (elements[i].id == id)
                    return false;
            for (int i = 0; i < forbiden_elems.Count; i++)
                for (int j = 0; j < forbiden_elems[i].Count; j++)
                    if (forbiden_elems[i][j] == id)
                        return false;
            tableLayoutPanel1.RowCount++;
            elements.Add(new Elem(id, elements.Count, tableLayoutPanel1, CallBack_Remove, CalBack_Delit, new calback(Update1)));
            forbiden_elems.Add(forbiden_elems1);
            return true;
        }
    }


    class Elem
    {
        calback calback;
        Label name;
        Button rem;
        Button del;
        public Id id;
        Action delDeleg, remDeleg;
        public Elem(Id id, int num, TableLayoutPanel main, Action remove, Action delit, calback calback)
        {
            this.id = id;
            delDeleg = delit;
            remDeleg = remove;
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
            main.Controls.Add(name,0, num);
            main.Controls.Add(rem, 1, num);
            main.Controls.Add(del,2, num);
            this.calback = calback;
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
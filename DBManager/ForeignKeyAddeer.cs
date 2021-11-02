using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    public partial class ForeignKeyAddeer : Form
    {
        List<List<string>> tables = new List<List<string>>();
        public ForeignKeyAddeer(List<List<string>> _tables)
        {
            InitializeComponent();
            tables = _tables;
            foreach (List<string> itr in tables)
            {
                OwningCombo.Items.Add(itr[0]);
                ReferCombo.Items.Add(itr[0]);
            }
        }

        private void OwningCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OwningCombo.SelectedItem!=null)
            {
                OwningColumnCombo.Items.Clear();
                List<string> found = tables.Find(x => x[0] == OwningCombo.SelectedItem.ToString());
                for (int i = 1; i < found.Count; i++)
                {
                    OwningColumnCombo.Items.Add(found[i]);
                }
            }
        }

        private void ReferCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReferCombo.SelectedItem != null)
            {
                ReferColumnCombo.Items.Clear();
                List<string> found = tables.Find(x => x[0] == ReferCombo.SelectedItem.ToString());
                for (int i = 1; i < found.Count; i++)
                {
                    ReferColumnCombo.Items.Add(found[i]);
                }
            }
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            if (OwningCombo.SelectedItem == null || OwningColumnCombo.SelectedItem == null || ReferCombo.SelectedItem == null || ReferColumnCombo.SelectedItem == null || textBox1.Text==string.Empty)
            {
                MessageBox.Show("Please make sure u fill all the fields or exit", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}

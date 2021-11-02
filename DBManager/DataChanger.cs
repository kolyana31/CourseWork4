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
    public partial class DataChanger : Form
    {
        public DataChanger()
        {
            InitializeComponent();
            textBox1.Text = dateTimePicker1.Value.ToString();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = dateTimePicker1.Value.ToString();
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please choose date first");
                DialogResult = DialogResult.None;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}

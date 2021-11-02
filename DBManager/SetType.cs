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
    public partial class SetType : Form
    {
        List<DBType> types = new List<DBType>();
        public SetType(List<DBType> tp)
        {
            InitializeComponent();
            types = tp;
            foreach (DBType itr in types)
            {
                comboBox1.Items.Add(itr.name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (!types.Find(el=>el.name==comboBox1.SelectedItem.ToString()).NeedParametrs)
                {
                    textBox1.Text = string.Empty;
                    textBox1.ReadOnly = true;
                }
                else
                {
                    textBox1.ReadOnly = false;
                }
            }
        }
    }
}

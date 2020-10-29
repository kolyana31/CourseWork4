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
    public partial class SQLCreator : Form
    {
        DataBases connector;
        public SQLCreator(DataBases _connector)
        {
            InitializeComponent();
            connector = _connector;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Using this form you can execute your own methods such as: \n -SELECT \n -UPDATE \n -INSERT \n ETC...\n But highly recomended to use simple methods to prevent errors and exceptions.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

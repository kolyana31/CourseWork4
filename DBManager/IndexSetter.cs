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
    public partial class IndexSetter : Form
    {
        public IndexSetter()
        {
            InitializeComponent();
        }

        public IndexSetter(string _text)
        {
            InitializeComponent();
            label1.Text = _text;
        }
    }
}

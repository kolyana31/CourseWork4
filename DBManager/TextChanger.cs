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
    public partial class TextChanger : Form
    {
        public TextChanger(string _text)
        {
            InitializeComponent();
            richTextBox1.Text = _text;
        }

    }
}

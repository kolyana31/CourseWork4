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
    public partial class PictureSShower : Form
    {
        public PictureSShower(Image _pictures)
        {
            InitializeComponent();
            PictureBox.Image = _pictures;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void centerImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }
    }
}

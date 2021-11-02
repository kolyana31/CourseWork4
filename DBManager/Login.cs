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
    public partial class Login : Form
    {
        ConnectionForm ConnectForm;
        DataBaseShower DataBaseWindow;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectForm = new ConnectionForm((Int16)ComboBaseList.SelectedIndex,isLocal.Checked);
            if (ComboBaseList.SelectedItem != null)
            {
                if (ConnectForm.ShowDialog() == DialogResult.OK)
                {
                    if (ComboBaseList.SelectedIndex == 0)
                    {
                        DataBaseWindow = new DataBaseShower(ConnectForm.Myconnector);
                    }
                    else if(ComboBaseList.SelectedIndex == 1)
                    {
                        DataBaseWindow = new DataBaseShower(ConnectForm.Msconnector);
                    }
                    else if (ComboBaseList.SelectedIndex == 2)
                    {
                        DataBaseWindow = new DataBaseShower(ConnectForm.PGConnector);
                    }
                    else
                    {
                        DataBaseWindow = new DataBaseShower(ConnectForm.ACConnector);
                    }

                    this.Hide();
                    if (DataBaseWindow.ShowDialog() != DialogResult.None)
                    {
                        this.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Choose database type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBaseList.SelectedIndex == 1)
            {
                isLocal.Visible = true;
            }
            else
            {
                isLocal.Visible = false;
                isLocal.Checked = false;
            }
        }
    }
}
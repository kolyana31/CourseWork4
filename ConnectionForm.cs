using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CourseWork2
{
    public partial class ConnectionForm : Form
    {
        public MySQLConnector Myconnector;
        public bool Local;
        public MsSQLConnector Msconnector;
        public PostgresSQL PGConnector;
        public AccessConnector ACConnector;
        Int16 Type;

        public ConnectionForm()
        {
            InitializeComponent();
        }
        public void FormatForm(string Name)
        {
            foreach (var item in Controls)
            {
                if (item is Panel)
                {
                    if (((Panel)item).Name == Name)
                    {
                        ((Panel)item).Visible = true;
                        ((Panel)item).Top = 0;
                        ((Panel)item).Left = 0;
                    }
                    else
                    {
                        ((Panel)item).Visible = false;
                    }
                }
            }
        }
        public ConnectionForm(Int16 type,bool _Local)
        {
            InitializeComponent();
            switch (type)
            {
                case 0:
                    ServerPort.Text = "3306";
                    FormatForm("PanelPFormat");
                    break;
                case 1:
                    ServerPort.Text = "1433";
                    Local = _Local;
                    if (Local)
                    {
                        FormatForm("PanelLocalFormat");
                    }
                    else
                    {
                        FormatForm("PanelPFormat");
                    }
                    break;
                case 2:
                    ServerPort.Text = "5432";
                    FormatForm("PanelPFormat");
                    break;
                case 3:
                    FormatForm("PanelFileFormat");
                    break;
            }
            Type = type;
        }
        //20 - local button
        //30 - test button tag
        private void button1_Click(object sender, EventArgs e)
        {
            if ((ServerAdress.Text.Length > 0 &&
                ServerPort.Text.Length > 0 &&
                Database.Text.Length > 0) || 
                (((Button)sender).Tag.ToString() == "30" && LocalBD.Text.Length>0) ||
                (((Button)sender).Tag.ToString() == "20" && LocalBD.Text.Length>0) ||
                (((Button)sender).Tag.ToString() == "30" && FilePathString.Text.Length > 0) ||
                (((Button)sender).Tag.ToString() == "20" && FilePathString.Text.Length > 0))
            {
                bool ConRes = false;
                if (Type == 0)
                {
                    Myconnector = new MySQLConnector(ServerAdress.Text, Convert.ToInt16(ServerPort.Text), Database.Text, UserName.Text, Password.Text);

                    if (((Button)sender).Tag.ToString() == "30")
                    {
                        Myconnector.TestConnect();
                    }
                    else
                    {
                        ConRes = Myconnector.Connect();
                    }
                }
                else if (Type == 1)
                {
                    if (Local)
                    {
                        Msconnector = new MsSQLConnector(ServerAdress.Text, Convert.ToInt16(ServerPort.Text), LocalBD.Text, UserName.Text, Password.Text, Local);
                    }
                    else
                    {
                        Msconnector = new MsSQLConnector(ServerAdress.Text, Convert.ToInt16(ServerPort.Text), Database.Text, UserName.Text, Password.Text, Local);
                    }
                    
                    if (((Button)sender).Tag.ToString() == "30")
                    {
                        Msconnector.TestConnect();
                    }
                    else
                    {
                        ConRes = Msconnector.Connect();
                    }
                }
                else if (Type == 2)
                {
                    PGConnector = new PostgresSQL(ServerAdress.Text, Convert.ToInt16(ServerPort.Text), Database.Text, UserName.Text, Password.Text);

                    if (((Button)sender).Tag.ToString() == "30")
                    {
                        PGConnector.TestConnect();
                    }
                    else
                    {
                        ConRes = PGConnector.Connect();
                    }
                }
                else
                {
                    ACConnector = new AccessConnector(FilePathString.Text);

                    if (((Button)sender).Tag.ToString() == "30")
                    {
                        ACConnector.TestConnect();
                    }
                    else
                    {
                        ConRes = ACConnector.Connect();
                    }
                }

                if (((Button)sender).Tag.ToString() == "20" && ConRes)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("Please Feel required data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePathString.Text = openFileDialog1.FileName;
            }
            else
            {
                FilePathString.Text = null;
            }
        }
    }
}

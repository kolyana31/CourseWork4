using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;

namespace CourseWork2
{
    public partial class DataBaseShower : Form
    {
        DataBases connector;
        public DataBaseShower(MsSQLConnector _connector)
        {
            InitializeComponent();
            connector = new MsSQLConnector();
            connector = _connector;
        }
        public DataBaseShower(MySQLConnector _connector)
        {
            InitializeComponent();
            connector = new MySQLConnector();
            connector = _connector;
        }
        public DataBaseShower(AccessConnector _connector)
        {
            InitializeComponent();
            connector = new AccessConnector();
            connector = _connector;
        }
        public DataBaseShower(PostgresSQL _connector)
        {
            InitializeComponent();
            connector = new PostgresSQL();
            connector = _connector;
        }

        private void refreshDatabasrListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableList.Items.Clear();
            List<object> temp = connector.getDataBaseTables();
            if (temp != null)
            {
                foreach (var item in connector.getDataBaseTables())
                {
                    TableList.Items.Add(item);
                }
            }
            else
            {
                DialogResult = DialogResult.Abort;
            }
        }

        private void TableList_Click(object sender, EventArgs e)
        {
            DataViewer.DataSource = connector.selectFrom(TableList.SelectedItem.ToString());
        }
    }
}

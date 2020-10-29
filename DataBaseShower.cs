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

        private void ImageShower(DataGridView _grid)
        {
            if (_grid.SelectedCells[0].ValueType == typeof(System.Byte[]))
            {
                if (_grid.SelectedCells[0].Value != System.DBNull.Value)
                {
                    PictureSShower temp = new PictureSShower((Bitmap)((new ImageConverter()).ConvertFrom(_grid.SelectedCells[0].Value)));
                    temp.Show();
                }
                else
                {
                    MessageBox.Show("Image is corupted", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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
            connector.setStructureOfDatabase();
        }

        private void TableList_Click(object sender, EventArgs e)
        {
            DataViewer.DataSource = connector.selectFrom(TableList.SelectedItem.ToString());
        }

        private void sQLCreaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQLCreator SQLCreator = new SQLCreator(connector);
            Hide();
            if (SQLCreator.ShowDialog() != DialogResult.None)
            {
                Show();
            }
        }

        private void DeleteInfo_Click(object sender, EventArgs e)
        {
            if (TableList.SelectedItem != null && DataViewer.CurrentRow.Cells != null)
            {
                connector.DeleteInfo(TableList.SelectedItem.ToString(), DataViewer.CurrentRow.Cells);
                TableList_Click(sender, e);
            }
        }

        private void DataViewer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ImageShower(DataViewer);
        }

        private void DELETEALL_Click(object sender, EventArgs e)
        {
            if (TableList.SelectedItem != null)
            {
                if (MessageBox.Show("Are u sure u want to", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    connector.DeleteALL(TableList.SelectedItem.ToString());
                    TableList_Click(sender, e);
                }
            }
        }

        private void DataViewer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EditMode.Checked && DataViewer.CurrentRow != null) 
            {
                HelperGrid.Columns.Clear();
                foreach (DataGridViewColumn itr in DataViewer.Columns)
                {
                    HelperGrid.Columns.Add((DataGridViewColumn)itr.Clone());
                }
                foreach (DataGridViewRow r in DataViewer.SelectedRows)
                {
                    int index = HelperGrid.Rows.Add(r.Clone());
                    foreach (DataGridViewCell o in r.Cells)
                    {
                        HelperGrid.Rows[index].Cells[o.ColumnIndex].Value = o.Value;
                        HelperGrid.Rows[index].Cells[o.ColumnIndex].ValueType = o.ValueType;
                    }
                }
            }
        }

        private void HelperGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(HelperGrid.SelectedCells[0].ValueType);
        }

        private void HelperGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ImageShower(HelperGrid);
        }
    }
}

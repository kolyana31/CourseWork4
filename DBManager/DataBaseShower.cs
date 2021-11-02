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
        DataBaseStructureChanger StructChanger;

        public DataBaseShower(MsSQLConnector _connector)
        {
            InitializeComponent();
            connector = new MsSQLConnector();
            connector = _connector;
            connector.type = 1;
        }
        public DataBaseShower(MySQLConnector _connector)
        {
            InitializeComponent();
            connector = new MySQLConnector();
            connector = _connector;
            connector.type = 2;
        }
        public DataBaseShower(AccessConnector _connector)
        {
            InitializeComponent();
            connector = new AccessConnector();
            connector = _connector;
            connector.type = 3;
        }
        public DataBaseShower(PostgresSQL _connector)
        {
            InitializeComponent();
            connector = new PostgresSQL();
            connector = _connector;
            connector.type = 4;
        }

        private void SetStructureForm(Int16 type)
        {
            if (type == 1)
            {
                StructChanger = new DataBaseStructureChanger((MsSQLConnector)connector);
            }
            else if (type == 2)
            {
                StructChanger = new DataBaseStructureChanger((MySQLConnector)connector);
            }
            else if (type == 3)
            {
                StructChanger = new DataBaseStructureChanger((AccessConnector)connector);
            }
            else if (type == 4)
            {
                StructChanger = new DataBaseStructureChanger((PostgresSQL)connector);
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        private void ImageShower(DataGridView _grid)
        {
            if (_grid.SelectedCells[0].ValueType == typeof(System.Byte[]))
            {
                if (_grid.SelectedCells[0].Value != System.DBNull.Value && _grid.SelectedCells[0].Value != null)
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
            DataViewer.Columns.Clear();
            HelperGrid.Columns.Clear();
            List<object> temp = connector.getDataBaseTables();
            if (temp != null)
            {
                foreach (var item in temp)
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
            try
            {
                if (TableList.SelectedItem!=null)
                {
                    DataViewer.DataSource = connector.selectFrom(TableList.SelectedItem.ToString());
                }
            }
            catch (Exception err)
            {
                connector.ErrorShower(err);
                //MessageBox.Show("Please wait data is loading...", "Data loading", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (MessageBox.Show("Are u sure u want to delete all information?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

        private void HelperGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ImageShower(HelperGrid);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (HelperGrid.SelectedCells.Count >0)
            {
                if (HelperGrid.SelectedCells[0].ValueType == typeof(System.Byte[]))
                {
                    if (openPictureDialog.ShowDialog() == DialogResult.OK)
                    {
                        HelperGrid.SelectedCells[0].Value = ImageToByteArray(Image.FromFile(openPictureDialog.FileName));
                    }
                }
                else if (HelperGrid.SelectedCells[0].ValueType == typeof(System.DateTime))
                {
                    DataChanger changer = new DataChanger();
                    if (changer.ShowDialog() == DialogResult.OK)
                    {
                        HelperGrid.SelectedCells[0].Value = changer.dateTimePicker1.Value;
                    }
                    changer = null;
                }
                else if (HelperGrid.SelectedCells[0].ValueType == typeof(System.String))
                {
                    TextChanger changer = new TextChanger(HelperGrid.SelectedCells[0].Value.ToString());
                    if (changer.ShowDialog() == DialogResult.OK)
                    {
                        HelperGrid.SelectedCells[0].Value = changer.richTextBox1.Text;
                    }
                    changer = null;
                }
                else
                {
                    MessageBox.Show("Use it for changing Pictures/Dates/Text for other changes use grid interface", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ClearEditValue_Click(object sender, EventArgs e)
        {
            HelperGrid.SelectedCells[0].Value = null;
        }

        private void HelperGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("There is a type error, pls use other data", "ERROR TYPE", MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (DataViewer.SelectedRows.Count>0 && EditMode.Checked)
            {
                connector.UpdateElem(TableList.SelectedItem.ToString(), HelperGrid.CurrentRow.Cells, DataViewer.SelectedRows[0].Cells);
                HelperGrid.Columns.Clear();
                EditMode.Checked = false;
                TableList_Click(sender, e);
            }
            else if (InsertMode.Checked)
            {
                connector.InsertElem(TableList.SelectedItem.ToString(), HelperGrid.CurrentRow.Cells);
                HelperGrid.Columns.Clear();
                InsertMode.Checked = false;
                TableList_Click(sender, e);
            }
        }

        private void InsertMode_CheckedChanged(object sender, EventArgs e)
        {
            if (InsertMode.Checked)
            {
                if (!EditMode.Checked)
                {
                    HelperGrid.Columns.Clear();
                    foreach (DataGridViewColumn itr in DataViewer.Columns)
                    {
                        HelperGrid.Columns.Add((DataGridViewColumn)itr.Clone());
                    }
                    int index = HelperGrid.Rows.Add();
                    for (int i = 0; i < HelperGrid.Columns.Count; i++)
                    {
                        HelperGrid.Rows[index].Cells[i].ValueType = DataViewer.Columns[i].ValueType;
                    }
                }
                else
                {
                    MessageBox.Show("Only one Mode can be chosen for use", "Mode warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    InsertMode.Checked = false;
                    EditMode.Checked = false;
                    HelperGrid.Columns.Clear();
                }
            }
            else
            {
                HelperGrid.Columns.Clear();
            }
        }

        private void EditMode_CheckedChanged(object sender, EventArgs e)
        {
            if (!EditMode.Checked)
            {
                HelperGrid.Columns.Clear();
            }
            else if (EditMode.Checked && InsertMode.Checked)
            {
                MessageBox.Show("Only one Mode can be chosen for use", "Mode warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InsertMode.Checked = false;
                EditMode.Checked = false;
                HelperGrid.Columns.Clear();
            }
        }

        private void utillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStructureForm(connector.type);
            this.Hide();
            if (StructChanger.ShowDialog() != DialogResult.None)
            {
                this.Show();
                refreshDatabasrListToolStripMenuItem_Click(sender, e);
            }
        }
    }
}

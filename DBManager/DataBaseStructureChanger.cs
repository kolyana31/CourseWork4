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
    public partial class DataBaseStructureChanger : Form
    {
        DataBases connector;
        DBTableStructure Table;
        List<DBType> types;
        List<List<string>> TableCol = new List<List<string>>();
        List<List<string>> FKlist = new List<List<string>>();

        public DataBaseStructureChanger(MsSQLConnector _connector)
        {
            InitializeComponent();
            connector = new MsSQLConnector();
            connector = _connector;
        }
        public DataBaseStructureChanger(MySQLConnector _connector)
        {
            InitializeComponent();
            connector = new MySQLConnector();
            connector = _connector;
        }
        public DataBaseStructureChanger(AccessConnector _connector)
        {
            InitializeComponent();
            connector = new AccessConnector();
            connector = _connector;
        }
        public DataBaseStructureChanger(PostgresSQL _connector)
        {
            InitializeComponent();
            connector = new PostgresSQL();
            connector = _connector;
        }

        private void DataBaseStructureChanger_Shown(object sender, EventArgs e)
        {
            TableList.Items.Clear();
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

        private void DeleteTable_Click(object sender, EventArgs e)
        {
            if (TableList.SelectedItem != null)
            {
                connector.DeleteTable(TableList.SelectedItem.ToString());
                DataBaseStructureChanger_Shown(sender, e);
            }
            else
            {
                MessageBox.Show("Please chose table first", "Table not chosen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void DropIndex_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0].ColumnIndex == 2)
            {
                connector.DropPKByConstraint(TableList.SelectedItem.ToString(), dataGridView1.SelectedCells[0].Value.ToString());
                connector.setStructureOfDatabase();
                TableList_Click(sender, e);
            }
            else if (dataGridView1.SelectedCells[0].ColumnIndex == 3)
            {
                connector.DropUniqueByConstraint(TableList.SelectedItem.ToString(), dataGridView1.SelectedCells[0].Value.ToString());
                connector.setStructureOfDatabase();
                TableList_Click(sender, e);
            }
        }

        private void TableList_Click(object sender, EventArgs e)
        {
            AddRow.Checked = false;
            AddTable.Checked = false;
            if (TableList.SelectedItem != null)
            {
                Table = connector.GetDBTableStructure(TableList.SelectedItem.ToString());
                FKlist.Clear();
                FKlist = connector.getFKlist(TableList.SelectedItem.ToString());
            }
            if (Table != null && !ForeignKeyMode.Checked)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ColumnName", HeaderText = "ColumnName", Width = 100, ReadOnly = true }); ;
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TypeOfColumn", HeaderText = "TypeOfColumn", Width = 100, ReadOnly = true });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PK Index", HeaderText = "PK Index", Width = 100, ReadOnly = true });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "UQ Index", HeaderText = "UQ Index", Width = 100, ReadOnly = true });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Is Nullable", HeaderText = "Is Nullable", Width = 100, ReadOnly = true });

                foreach (var itr in Table.VariablesNTypes)
                {
                    dataGridView1.Rows.Add(itr[0], itr[1], itr[2], itr[3], itr[4]);
                }
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                        foreach (DataGridViewCell itr in item.Cells)
                        {
                            if (itr.Value is null)
                            {
                                itr.Tag = "404";
                                itr.Value = "NULL";
                            }
                        }
                }
            }
            else if (FKlist != null && ForeignKeyMode.Checked)
            {
                dataGridView1.Columns.Clear();
                foreach (string itr in FKlist[0])
                {
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = itr, HeaderText = itr, Width = 100, ReadOnly = true }); ;
                }
                for (int i = 1; i < FKlist.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j].Value = FKlist[i][j].ToString();
                    }
                }
            }
        }

        private void DeleteColumn_Click(object sender, EventArgs e)
        {
            if (TableList.SelectedItem != null && dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0] != null)
            {
                connector.DeleteColumn(TableList.SelectedItem.ToString(), dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                connector.setStructureOfDatabase();
                TableList_Click(sender, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (TableList.SelectedItem != null && AddRow.Checked)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Column Name", HeaderText = "Column Name", Width = 100, ReadOnly = false }); ;
                dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "TypeOfColumn", HeaderText = "TypeOfColumn", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Type Parametr", HeaderText = "Type Parametr", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PK Index", HeaderText = "PK Index", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "UQ Index", HeaderText = "UQ Index", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Is Nullable", HeaderText = "Is Nullable", Width = 100, ReadOnly = false });

                types = connector.getTypes();

                dataGridView1.Rows.Add("", "", "", "", "", false);
                foreach (var itr in types)
                {
                    ((DataGridViewComboBoxCell)dataGridView1.Rows[0].Cells[1]).Items.Add(itr.name);
                }
            }
            else if (!AddRow.Checked)
            {
                dataGridView1.Columns.Clear();
                if (TableList.SelectedItem != null)
                {
                    TableList_Click(sender, e);
                }
            }
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            if (AddRow.Checked && TableList.SelectedItem != null && dataGridView1.Rows[0].Cells[0].Value.ToString() != string.Empty && dataGridView1.Rows[0].Cells[1].Value.ToString() != string.Empty)
            {
                connector.AddColumn(TableList.SelectedItem.ToString(), dataGridView1.Rows[0].Cells[0].Value.ToString(), dataGridView1.Rows[0].Cells[1].Value.ToString(), dataGridView1.Rows[0].Cells[2].Value.ToString(), dataGridView1.Rows[0].Cells[3].Value.ToString(), dataGridView1.Rows[0].Cells[4].Value.ToString(), dataGridView1.Rows[0].Cells[5].Value.ToString());
                connector.setStructureOfDatabase(); 
                TableList_Click(sender, e);
                DataBaseStructureChanger_Shown(sender, e);
                AddRow.Checked = false;
                AddTable.Checked = false;
            }
            else if (AddTable.Checked && TableList.SelectedItem != null && dataGridView1.Rows[0].Cells[0].Value.ToString() != string.Empty && dataGridView1.Rows[0].Cells[1].Value.ToString() != string.Empty)
            {
                connector.AddTableWithColumn(dataGridView1.Rows[0].Cells[0].Value.ToString(), dataGridView1.Rows[0].Cells[1].Value.ToString(), dataGridView1.Rows[0].Cells[2].Value.ToString(), dataGridView1.Rows[0].Cells[3].Value.ToString(), dataGridView1.Rows[0].Cells[4].Value.ToString(), dataGridView1.Rows[0].Cells[5].Value.ToString(), dataGridView1.Rows[0].Cells[6].Value.ToString());
                connector.setStructureOfDatabase();
                TableList_Click(sender, e);
                DataBaseStructureChanger_Shown(sender, e);
                AddRow.Checked = false;
                AddTable.Checked = false;
            }
            else
            {
                MessageBox.Show("Probably you  didnt fill column name or type or table name","WARNING",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (AddRow.Checked)
            {
                if (dataGridView1.SelectedCells[0].ColumnIndex==1 && dataGridView1.SelectedCells[0].Value!=null)
                {
                    dataGridView1.Rows[0].Cells[2].Value = string.Empty;
                    if (types.FindIndex(x=>x.name==dataGridView1.SelectedCells[0].Value.ToString())!=-1)
                    {
                        if (types.Find(x => x.name == dataGridView1.SelectedCells[0].Value.ToString()).NeedParametrs)
                        {
                            dataGridView1.Columns[2].ReadOnly = false;
                        }
                        else
                        {
                            dataGridView1.Columns[2].ReadOnly = true;
                        }
                    }
                }
            }
            else if (AddTable.Checked)
            {
                if (dataGridView1.SelectedCells[0].ColumnIndex == 2 && dataGridView1.SelectedCells[0].Value != null)
                {
                    dataGridView1.Rows[0].Cells[3].Value = string.Empty;
                    if (types.FindIndex(x => x.name == dataGridView1.SelectedCells[0].Value.ToString()) != -1)
                    {
                        if (types.Find(x => x.name == dataGridView1.SelectedCells[0].Value.ToString()).NeedParametrs)
                        {
                            dataGridView1.Columns[3].ReadOnly = false;
                        }
                        else
                        {
                            dataGridView1.Columns[3].ReadOnly = true;
                        }
                    }
                }
            }
        }

        private void AddTable_CheckedChanged(object sender, EventArgs e)
        {
            if (TableList.SelectedItem != null && AddTable.Checked)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Table Name", HeaderText = "Table Name", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Column Name", HeaderText = "Column Name", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "TypeOfColumn", HeaderText = "TypeOfColumn", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Type Parametr", HeaderText = "Type Parametr", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PK Index", HeaderText = "PK Index", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "UQ Index", HeaderText = "UQ Index", Width = 100, ReadOnly = false });
                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Is Nullable", HeaderText = "Is Nullable", Width = 100, ReadOnly = false });

                types = connector.getTypes();

                dataGridView1.Rows.Add("", "", "", "", "", "", false);
                foreach (var itr in types)
                {
                    ((DataGridViewComboBoxCell)dataGridView1.Rows[0].Cells[2]).Items.Add(itr.name);
                }
            }
            else if (!AddTable.Checked)
            {
                dataGridView1.Columns.Clear();
                if (TableList.SelectedItem != null)
                {
                    TableList_Click(sender, e);
                }
            }
        }

        private void AddIndex_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                IndexSetter indexsetter = new IndexSetter();
                if (indexsetter.ShowDialog() == DialogResult.OK)
                {
                    if (dataGridView1.CurrentCell.ColumnIndex == 2)
                    {
                        connector.AddPrimaryKey(TableList.SelectedItem.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), indexsetter.InputField.Text);
                    }
                    else if(dataGridView1.CurrentCell.ColumnIndex == 3)
                    {
                        connector.AddUniqueKey(TableList.SelectedItem.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), indexsetter.InputField.Text);
                    }
                    connector.setStructureOfDatabase();
                    TableList_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please choose Primary key field or unique field", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditName_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                IndexSetter indexsetter = new IndexSetter();
                if (indexsetter.ShowDialog() == DialogResult.OK)
                {
                    if (dataGridView1.CurrentCell.ColumnIndex == 2)
                    {
                        connector.AddPrimaryKey(TableList.SelectedItem.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), indexsetter.InputField.Text);
                    }
                    else if (dataGridView1.CurrentCell.ColumnIndex == 3)
                    {
                        connector.AddUniqueKey(TableList.SelectedItem.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), indexsetter.InputField.Text);
                    }
                    connector.setStructureOfDatabase();
                    TableList_Click(sender, e);
                }
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                IndexSetter indexsetter = new IndexSetter("Enter new column");
                if (indexsetter.ShowDialog() == DialogResult.OK)
                {
                    connector.ChangeColumn(TableList.SelectedItem.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), indexsetter.InputField.Text);
                    connector.setStructureOfDatabase();
                    TableList_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please choose Primary key field or unique field", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditType_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 1 && TableList.SelectedItem!=null)
            {
                types = connector.getTypes();
                SetType TypeSetter = new SetType(types);
                if (TypeSetter.ShowDialog() == DialogResult.OK )
                {
                    connector.ChangeColumnType(TableList.SelectedItem.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), TypeSetter.comboBox1.Text, TypeSetter.textBox1.Text);
                    connector.setStructureOfDatabase();
                    TableList_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please choose Column type and table", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ForeignKeyMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ForeignKeyMode.Checked)
            {
                AddRow.Checked = false;
                AddTable.Checked = false;
            }
            else
            {
                dataGridView1.Columns.Clear();
                if (TableList.SelectedItem != null)
                {
                    TableList_Click(sender, e);
                }
            }
        }

        private void DeleteFK_Click(object sender, EventArgs e)
        {
            if (TableList.SelectedItem != null && ForeignKeyMode.Checked && dataGridView1.CurrentCell.OwningColumn.Name=="fk_constraint_name")
            {
                connector.DropFKByConstraint(TableList.SelectedItem.ToString(), dataGridView1.CurrentCell.Value.ToString());
                connector.setStructureOfDatabase();
                TableList_Click(sender, e);
            }
        }

        private void AddFK_Click(object sender, EventArgs e)
        {
            TableCol.Clear();
            foreach (string itr in TableList.Items)
            {
                TableCol.Add(new List<string> { itr });
                foreach (List<string> item in connector.GetDBTableStructure(itr).VariablesNTypes)
                {
                    TableCol[TableCol.Count-1].Add(item[0]);
                }
            }
            ForeignKeyAddeer FKAdder = new ForeignKeyAddeer(TableCol);
            if (FKAdder.ShowDialog() == DialogResult.OK)
            {
                connector.AddFKByConstraint(FKAdder.OwningCombo.Text, FKAdder.textBox1.Text, FKAdder.OwningColumnCombo.Text, FKAdder.ReferCombo.Text, FKAdder.ReferColumnCombo.Text);
                connector.setStructureOfDatabase();
                TableList_Click(sender, e);
                DataBaseStructureChanger_Shown(sender, e);
            }
        }
    }
}

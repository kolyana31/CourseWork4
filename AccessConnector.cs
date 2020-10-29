using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace CourseWork2
{
    public class AccessConnector : DataBases
    {
        public OleDbCommand ACcommand = new OleDbCommand();
        public OleDbDataReader ACreader;

        public AccessConnector() { }
        public AccessConnector(string _FilePath)
            : base(_FilePath)
        {
            CreateConnectionString();
        }
        public void CreateConnectionString()
        {
            string connString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={FilePath};";
            conn = new OleDbConnection(connString);
            setTypes();
        }

        public override List<object> getDataBaseTables()
        {
            OpenCon();
            List<object> Temp = new List<object>();
            ACcommand.Connection = (OleDbConnection)conn;
            try
            {
                string[] restrictions = new string[4];
                restrictions[3] = "Table";
                DataTable userTables = conn.GetSchema("Tables", restrictions);
                for (int i = 0; i < userTables.Rows.Count; i++)
                {
                    Temp.Add(userTables.Rows[i][2]);
                }
                CloseCon();
                return Temp;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                CloseCon();
                return null;
            }
        }

        public override DataTable selectFrom(string Form)
        {
            OpenCon();
            ACcommand = (OleDbCommand)conn.CreateCommand();
            ACcommand.CommandText = $"select * from {Form}";
            OleDbDataAdapter da = new OleDbDataAdapter(ACcommand.CommandText, (OleDbConnection)conn);
            dataSet.Reset();
            da.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            CloseCon();
            return dataTable;
        }

        public override void setStructureOfDatabase()
        {
            OpenCon();
            TablesTypes.Clear();
            ACcommand.Connection = (OleDbConnection)conn;
            try
            {
                string[] restrictions = new string[4];
                restrictions[3] = "Table";
                DataTable userTables = conn.GetSchema("Tables", restrictions);
                for (int i = 0; i < userTables.Rows.Count; i++)
                {
                    TablesTypes.Add(new DBTableStructure(userTables.Rows[i][2].ToString()));
                }
                foreach (var itr in TablesTypes)
                {
                    ACcommand.CommandText = $"select * from {itr.Name}";
                    ACreader = ACcommand.ExecuteReader();
                    dataTable = ACreader.GetSchemaTable();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        itr.VariablesNTypes.Add(new string[] { dataTable.Rows[i][0].ToString(), dataTable.Rows[i][5].ToString() });
                    }
                    ACreader.Close();
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            CloseCon();
        }

        public override void DeleteInfo(string _tableName,System.Windows.Forms.DataGridViewCellCollection currentRow)
        {
            OpenCon();
            List<string> columnsNames = new List<string>();
            List<string> values = new List<string>();
            string terms = "";

            foreach (System.Windows.Forms.DataGridViewCell itr in currentRow)
            {
                if (itr.ValueType.ToString() != "System.Byte[]")
                {
                    if (itr.ValueType.ToString() == "System.Boolean")
                    {
                        columnsNames.Add($"[{itr.OwningColumn.Name}]");
                        if ((bool)itr.Value == false)
                        {
                            values.Add("0");
                        }
                        else
                        {
                            values.Add("-1");
                        }
                    }
                    else if (itr.ValueType.ToString() == "System.DateTime")
                    {
                        columnsNames.Add($"[{itr.OwningColumn.Name}]");
                        values.Add(((DateTime)itr.Value).ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        columnsNames.Add($"[{itr.OwningColumn.Name}]");
                        values.Add(itr.Value.ToString());
                    }

                }
            }

            for (int i = 0; i < columnsNames.Count; i++)
            {
                terms += $" {columnsNames[i]} Like '{values[i]}' ";
                if (i < columnsNames.Count - 1)
                {
                    terms += " and ";
                }
            }

            ACcommand.CommandText = string.Format("DELETE FROM {0} where {1}",
                _tableName,
                terms);
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }

        protected void setTypes()
        {
            Types.Clear();
            foreach (var itr in Enum.GetValues(typeof(OleDbType)))
            {
                Types.Add(itr.ToString());
            }
        }

        public override void DeleteALL(string _table)
        {
            OpenCon();
            ACcommand.CommandText = $"DELETE FROM {_table}";
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }
    }
}

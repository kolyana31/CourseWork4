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
        public OleDbCommand ACcommand;
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
            ACcommand = new OleDbCommand();
        }

        public override List<object> getDataBaseTables()
        {
            List<object> Temp = new List<object>();
            ACcommand = new OleDbCommand();
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
                return Temp;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Data.Common;

namespace CourseWork2
{
    abstract public class DataBases
    {
        protected DbConnection conn;
        protected DbCommand command;
        protected DbDataReader reader;
        protected SqlDataAdapter adapter;
        protected DataSet dataSet = new DataSet();
        protected DataTable dataTable = new DataTable();

        protected string host;
        protected int port;
        protected string database;
        protected string username;
        protected string password;
        protected string FilePath;
        protected Int16 type;
        protected string connString;

        protected List<string> Types = new List<string>();
        protected List<DBTableStructure> TablesTypes = new List<DBTableStructure>();
        
        protected DataBases() { }

        protected DataBases(string _host, int _port, string _database, string _username, string _password)
        {
            host = _host;
            port = _port;
            database = _database;
            username = _username;
            password = _password;
            type = 0;  
        }

        public void OpenCon()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public void CloseCon()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        protected DataBases(string _FilePath)
        {
            FilePath = _FilePath;
            type = 1;
        }

        public virtual List<object> getDataBaseTables()
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = "show tables;";
            reader = command.ExecuteReader();
            List<object> temp = new List<object>();
            while (reader.Read())
            {
                temp.Add(reader.GetValue(0));
            }
            CloseCon();
            return temp;
        }

        public ConnectionState getConnectionState()
        {
            return conn.State;
        }

        public abstract DataTable selectFrom(string Form);

        public bool Connect()
        {
            try
            {
                conn.Open();

                System.Windows.Forms.MessageBox.Show("Conection Has Benn set", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return true;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Error in process of connecting", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        public void TestConnect()
        {
            try
            {
                conn.Open();

                System.Windows.Forms.MessageBox.Show("Conection Has Benn set", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Error in process of connecting", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            conn.Close();
        }

        protected void ErrorShower(Exception err)
        {
            System.Windows.Forms.MessageBox.Show(err.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public virtual void setStructureOfDatabase()
        {
            OpenCon();
            TablesTypes.Clear();
            command = conn.CreateCommand();
            command.CommandText = "show tables;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                TablesTypes.Add(new DBTableStructure(reader.GetValue(0).ToString()));
            }
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{itr.Name}'";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    itr.VariablesNTypes.Add(new string[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString() });
                }
            }
            CloseCon();
        }

        public virtual void DeleteInfo(string _tableName, DataGridViewCellCollection currentRow)
        {
            OpenCon();
            List<string> columnsNames = new List<string>();
            List<string> values = new List<string>();
            string terms = "";

            foreach (DataGridViewCell itr in currentRow)
            {
                if (itr.ValueType.ToString() != "System.Byte[]")
                {
                    columnsNames.Add(itr.OwningColumn.Name);
                    values.Add(itr.Value.ToString());
                }
            }

            for (int i = 0; i < columnsNames.Count; i++)
            {
                terms += $" {columnsNames[i]}='{values[i]}' ";
                if (i < columnsNames.Count - 1)
                {
                    terms += " and ";
                }
            }

            command.CommandText = string.Format("DELETE FROM {0} where {1}",
                _tableName,
                terms);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }

        public virtual void DeleteALL(string _table)
        {
            OpenCon();
            command.CommandText = $"DELETE FROM {_table}";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }
    }
}
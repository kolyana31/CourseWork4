using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace CourseWork2
{
    abstract public class DataBases
    {
        protected DbConnection conn;
        protected DbCommand command;
        protected DbDataReader reader;
        protected DataSet dataSet;
        protected DataTable dataTable;

        protected string host;
        protected int port;
        protected string database;
        protected string username;
        protected string password;
        protected string FilePath;
        protected Int16 type;
        protected string connString;

        protected DataBases() 
        {
            dataSet = new DataSet();
            dataTable = new DataTable();
        }

        protected DataBases(string _host, int _port, string _database, string _username, string _password)
        {
            host = _host;
            port = _port;
            database = _database;
            username = _username;
            password = _password;
            type = 0;
            dataSet = new DataSet();
            dataTable = new DataTable();
        }

        protected void OpenCon()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        protected void CloseCon()
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
            dataSet = new DataSet();
            dataTable = new DataTable();
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
    }
}
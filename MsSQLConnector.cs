using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CourseWork2
{
    public class MsSQLConnector : DataBases
    {
        public MsSQLConnector() { }
        public MsSQLConnector(string _host, int _port, string _database, string _username, string _password,bool Local)
            : base(_host, _port, _database, _username, _password)
        {
            CreateConnectionString(Local);
        }

        public void CreateConnectionString(bool Local)
        {
            if (Local)
            {
                connString = "Server= localhost; Database= "+ database +";Integrated Security = SSPI; ";
                Console.WriteLine(connString);
            }
            else
            {
                connString = "Data Source=" + host + ";Network Library = DBMSSOCN;"
                           + "Initial Catalog=" + database + ";User ID = " + username + ";Password =" + password;
            }
            conn = new SqlConnection(connString);
            setTypes();
        }

        public override List<object> getDataBaseTables()
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = "select name from sys.tables;";
            reader = command.ExecuteReader();
            List<object> temp = new List<object>();
            while (reader.Read())
            {
                temp.Add(reader.GetValue(0));
            }
            CloseCon();
            return temp;
        }

        public override void setStructureOfDatabase()
        {
            OpenCon();
            TablesTypes.Clear();
            command = conn.CreateCommand();
            command.CommandText = "select name from sys.tables;";
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
                    itr.VariablesNTypes.Add(new string[]{reader.GetValue(0).ToString(), reader.GetValue(1).ToString()});
                }
            }
            CloseCon();
        }

        public override DataTable selectFrom(string Form)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"select * from {Form}";
            SqlDataAdapter da = new SqlDataAdapter(command.CommandText, (SqlConnection)conn);
            dataSet.Reset();
            da.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            CloseCon();
            return dataTable;
        }

        protected void setTypes()
        {
            Types.Clear();
            foreach (var itr in Enum.GetValues(typeof(SqlDbType)))
            {
                Types.Add(itr.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace CourseWork2
{
    public class MySQLConnector : DataBases
    {
        public MySQLConnector() { }
        public MySQLConnector(string _host, int _port, string _database, string _username, string _password)
            :base( _host, _port, _database, _username, _password)
        {
            CreateConnectionString();
        }
        public void CreateConnectionString()
        {
            connString = "Server=" + host + ";Database=" + database
                       + ";port="  + port + ";User Id="  + username + ";password=" + password;
            conn = new MySqlConnection(connString);
            setTypes();
        }

        public override DataTable selectFrom(string Form)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"select * from {Form};";
            MySqlDataAdapter da = new MySqlDataAdapter(command.CommandText, (MySqlConnection)conn);
            dataSet.Reset();
            da.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            CloseCon();
            return dataTable;
        }

        protected void setTypes() 
        {
            Types.Clear();
            foreach (var itr in Enum.GetValues(typeof(MySql.Data.MySqlClient.MySqlDbType)))
            {
                Types.Add(itr.ToString());
            }
        }
    }
}

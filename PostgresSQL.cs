using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace CourseWork2
{
    public class PostgresSQL : DataBases
    {
        public PostgresSQL() { }
        public PostgresSQL(string _host, int _port, string _database, string _username, string _password)
            : base(_host, _port, _database, _username, _password)
        {
            CreateConnectionString();
        }
        public void CreateConnectionString()
        {
            string connString = String.Format("Server={0};Port={1};" +
                                                "User Id={2};Password={3};Database={4};",
                                                host, port.ToString(), username,
                                                password, database);
            conn = new NpgsqlConnection(connString);
        }

        public override List<object> getDataBaseTables()
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = "SELECT table_name FROM information_schema.tables WHERE table_schema NOT IN ('information_schema','pg_catalog');";
            reader = command.ExecuteReader();
            List<object> temp = new List<object>();
            while (reader.Read())
            {
                temp.Add(reader.GetValue(0));
            }
            CloseCon();
            return temp;
        }

        public override DataTable selectFrom(string Form)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"select * from {Form}";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(command.CommandText, (NpgsqlConnection)conn);
            dataSet.Reset();
            da.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            CloseCon();
            return dataTable;
        }
    }
}


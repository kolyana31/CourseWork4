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
        protected DataSet dataSet = new DataSet();
        protected DataTable dataTable = new DataTable();

        protected string host;
        protected int port;
        protected string database;
        protected string username;
        protected string password;
        protected string FilePath;
        public Int16 type;
        protected string connString;

        protected List<DBType> Types = new List<DBType>();

        protected List<DBTableStructure> TablesTypes = new List<DBTableStructure>();

        protected List<List<string>> FKList = new List<List<string>>();//1 table name 2  column name primary 3primary table 4 pk column name 5 fk constraint

        protected DataBases() { }

        protected DataBases(string _host, int _port, string _database, string _username, string _password)
        {
            host = _host;
            port = _port;
            database = _database;
            username = _username;
            password = _password;
            type = 0;
            FKList.Add(new List<string> {"table_name","column_name","primary_table","primary_column_name","fk_constraint_name"});
        }

        protected void FKlistReset()
        {
            FKList.Clear();
            FKList.Add(new List<string> { "table_name", "column_name", "primary_table", "primary_column_name", "fk_constraint_name" });
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

        public List<DBType> getTypes()
        {
            return Types;
        }
        protected DataBases(string _FilePath)
        {
            FilePath = _FilePath;
            type = 1;
        }

        public abstract void AddColumn(string _table, string _columnname,string _type, string _param, string _pk, string _uq , string _nullable);

        public abstract void AddTableWithColumn(string _table, string _columnname, string _type, string _param, string _pk, string _uq, string _nullable);

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

        public void ErrorShower(Exception err)
        {
            System.Windows.Forms.MessageBox.Show(err.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
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
                command.CommandText = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{database}' and TABLE_NAME = '{itr.Name}'";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    itr.VariablesNTypes.Add(new List<string> { reader.GetValue(0).ToString(), reader.GetValue(1).ToString() , null, null, null, null });
                }
            }
            //pk index
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = "select stat.table_schema as database_name," +
                    " stat.table_name," +
                    " stat.index_name," +
                    " stat.column_name," +
                    " tco.constraint_type" +
                    " from information_schema.statistics stat" +
                    " join information_schema.table_constraints tco" +
                    " on stat.table_schema = tco.table_schema" +
                    " and stat.table_name = tco.table_name" +
                    " and stat.index_name = tco.constraint_name" +
                    " where stat.non_unique = 0" +
                    " and stat.table_schema not in ('information_schema', 'sys'," +
                    " 'performance_schema', 'mysql')" +
                    $" and stat.table_schema = '{database}'" +
                    $" and stat.table_name = '{itr.Name}'" +
                    " and tco.constraint_type = 'PRIMARY KEY'" +
                    " group by stat.table_schema," +
                    " stat.table_name," +
                    " stat.index_name," +
                    " tco.constraint_type" +
                    " order by stat.table_schema," +
                    " stat.table_name; ";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(1).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0]==reader.GetValue(3).ToString())
                        {
                            found.VariablesNTypes[i][2] = reader.GetValue(2).ToString();
                        }
                    }
                }
            }
            //unique index
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = "select stat.table_schema as database_name," +
                    " stat.table_name," +
                    " stat.index_name," +
                    " stat.column_name," +
                    " tco.constraint_type" +
                    " from information_schema.statistics stat" +
                    " join information_schema.table_constraints tco" +
                    " on stat.table_schema = tco.table_schema" +
                    " and stat.table_name = tco.table_name" +
                    " and stat.index_name = tco.constraint_name" +
                    " where stat.non_unique = 0" +
                    " and stat.table_schema not in ('information_schema', 'sys'," +
                    " 'performance_schema', 'mysql')" +
                    $" and stat.table_schema = '{database}'" +
                    $" and stat.table_name = '{itr.Name}'" +
                    " and tco.constraint_type = 'UNIQUE'" +
                    " group by stat.table_schema," +
                    " stat.table_name," +
                    " stat.index_name," +
                    " tco.constraint_type" +
                    " order by stat.table_schema," +
                    " stat.table_name; ";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(1).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0] == reader.GetValue(3).ToString())
                        {
                            found.VariablesNTypes[i][3] = reader.GetValue(2).ToString();
                        }
                    }
                }
            }
            //nullable
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = "SELECT TABLE_NAME, COLUMN_NAME " +
                    " FROM information_schema.COLUMNS" +
                    " WHERE" +
                    $" TABLE_SCHEMA = '{database}' and" +
                    $" TABLE_NAME = '{itr.Name}'" +
                    " AND IS_NULLABLE = 'YES'; ";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(0).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0] == reader.GetValue(1).ToString())
                        {
                            found.VariablesNTypes[i][4] = "true";
                        }
                        else
                        {
                            found.VariablesNTypes[i][4] = "false";
                        }
                    }
                }
            }
            // FK
                reader.Close();
                command.CommandText = "select  " +
                "col.table_name, \n" +
                "col.column_name as column_name, \n" +
                "kcu.referenced_table_name \n" +
                "as primary_table, \n" +
                "kcu.referenced_column_name as pk_column_name, \n" +
                "kcu.constraint_name as fk_constraint_name \n" +
                "from information_schema.columns col \n" +
                "join information_schema.tables tab \n" +
                "on col.table_schema = tab.table_schema \n" +
                "and col.table_name = tab.table_name \n" +
                "left join information_schema.key_column_usage kcu \n" +
                "on col.table_schema = kcu.table_schema \n" +
                "and col.table_name = kcu.table_name \n" +
                "and col.column_name = kcu.column_name \n" +
                "and kcu.referenced_table_schema is not null \n" +
                "where col.table_schema not in('information_schema','sys', \n" +
                "'mysql', 'performance_schema') \n" +
                "and tab.table_type = 'BASE TABLE' \n" +
                "--    and fks.constraint_schema = 'database name' \n" +
                "and kcu.constraint_name is not null \n" +
                $"and col.table_schema = '{database}' \n" +
                "order by col.table_schema, \n" +
                "col.table_name, \n" +
                "col.ordinal_position; ";
                reader = command.ExecuteReader();
                FKlistReset();
                while (reader.Read())
                {
                    FKList.Add(new List<string> { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString() });
                }
            CloseCon();
        }

        public List<List<string>> getFKlist(string _table)
        {
            List<List<string>> filtered = new List<List<string>>();
            filtered.Add(new List<string> { "table_name", "column_name", "primary_table", "primary_column_name", "fk_constraint_name" });
            for (int i = 1; i < FKList.Count; i++)
            {
                if (FKList[i][0]==_table)
                {
                    filtered.Add(FKList[i]);
                }
            }
            return filtered;
        }

        public virtual void DropFKByConstraint(string _table, string _fk)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"ALTER TABLE {_table} DROP FOREIGN KEY `{_fk}`; ";
            try
            {
                Console.WriteLine(command.CommandText);
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }

        public virtual DBTableStructure GetDBTableStructure(string Name)
        {
            if (Name!= null)
            {
                return TablesTypes.Find(el => el.Name == Name);
            }
            return null;
        }

        public abstract void DropPKByConstraint(string _table, string PKIndex);

        public abstract void DropUniqueByConstraint(string _table, string PKIndex);

        public virtual void DeleteInfo(string _tableName, DataGridViewCellCollection currentRow)
        {
            OpenCon();
            List<string> columnsNames = new List<string>();
            List<string> values = new List<string>();
            string terms = "";

            foreach (DataGridViewCell itr in currentRow)
            {
                if (itr.ValueType != typeof(System.Byte[]) && itr.ValueType != typeof(System.Double) && itr.ValueType != typeof(System.Decimal))
                {
                    columnsNames.Add(itr.OwningColumn.Name);
                    values.Add(itr.Value.ToString());
                }
                else if (itr.ValueType == typeof(System.Double) || itr.ValueType == typeof(System.Decimal))
                {
                    columnsNames.Add(itr.OwningColumn.Name);
                    values.Add(itr.Value.ToString().Replace(",", "."));
                }
            }

            for (int i = 0; i < columnsNames.Count; i++)
            {
                if (values[i] == string.Empty)
                {
                    terms += $" {columnsNames[i]} is null ";
                }
                else
                {
                    terms += $" {columnsNames[i]}='{values[i]}' ";
                }
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

        public virtual void AddFKByConstraint(string _table, string _FK,string _toColumn, string _refTable, string _refColumn)
        {
            OpenCon();
            command.CommandText = $"ALTER TABLE {_table} ADD CONSTRAINT {_FK} FOREIGN KEY({_toColumn}) REFERENCES {_refTable}({_refColumn}); ";
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

        public abstract void UpdateElem(string _tableName, System.Windows.Forms.DataGridViewCellCollection currentRow, System.Windows.Forms.DataGridViewCellCollection checkRow);

        public abstract void InsertElem(string _tableName, DataGridViewCellCollection currentRow);

        public virtual void DeleteTable(string _tableName)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"DROP TABLE {_tableName};";
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

        protected void NotSupported()
        {
            MessageBox.Show("This DataBase dosent support this function", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ConsoleLogTable()
        {
            foreach (var itr in TablesTypes)
            {
                itr.LogTable();
            }
        }

        public abstract void DeleteColumn(string table,string column);

        public virtual void AddPrimaryKey(string _table , string _column, string _pk)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"ALTER TABLE {_table} ADD CONSTRAINT {_pk} PRIMARY KEY({_column}); ";
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

        public virtual void AddUniqueKey(string _table, string _column, string _pk)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"ALTER TABLE {_table} ADD CONSTRAINT {_pk} UNIQUE ({_column}); ";
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

        public virtual void ChangeColumn(string _table, string _oldColumn, string _newColumn) 
        {
            NotSupported();
        }

        public virtual void ChangeColumnType(string _table, string _column, string _type, string _parametr)
        {
            NotSupported();
        }
    }
}
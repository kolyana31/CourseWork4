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
            setTypes();
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

        public override void setStructureOfDatabase()
        {
            OpenCon();
            TablesTypes.Clear();
            command = conn.CreateCommand();
            command.CommandText = "SELECT table_name FROM information_schema.tables WHERE table_schema NOT IN ('information_schema','pg_catalog');";
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
                    itr.VariablesNTypes.Add(new List<string> { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), null, null, null, null });
                }
            }
            //pk index
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = "select kcu.table_name, " +
                    " tco.constraint_name, " +
                    " string_agg(kcu.column_name, ', ') as key_columns " +
                    " from information_schema.table_constraints tco " +
                    " join information_schema.key_column_usage kcu " +
                    " on kcu.constraint_name = tco.constraint_name " +
                    " and kcu.constraint_schema = tco.constraint_schema " +
                    " and kcu.constraint_name = tco.constraint_name " +
                    $" where tco.constraint_type = 'PRIMARY KEY' and kcu.table_name = '{itr.Name}' " +
                    " group by tco.constraint_name, " +
                    " kcu.table_schema, " +
                    " kcu.table_name " +
                    " order by kcu.table_schema, " +
                    " kcu.table_name;" ;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(0).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0] == reader.GetValue(2).ToString())
                        {
                            found.VariablesNTypes[i][2] = reader.GetValue(1).ToString();
                        }
                    }
                }
            }
            //unique index
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = "select kcu.table_name, " +
                    " tco.constraint_name, " +
                    " string_agg(kcu.column_name, ', ') as key_columns " +
                    " from information_schema.table_constraints tco " +
                    " join information_schema.key_column_usage kcu " +
                    " on kcu.constraint_name = tco.constraint_name " +
                    " and kcu.constraint_schema = tco.constraint_schema " +
                    " and kcu.constraint_name = tco.constraint_name " +
                    $" where tco.constraint_type = 'UNIQUE' and kcu.table_name = '{itr.Name}' " +
                    " group by tco.constraint_name, " +
                    " kcu.table_schema, " +
                    " kcu.table_name " +
                    " order by kcu.table_schema, " +
                    " kcu.table_name;";
                reader = command.ExecuteReader();
                Console.WriteLine(command.CommandText);
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(0).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0] == reader.GetValue(2).ToString())
                        {
                            found.VariablesNTypes[i][3] = reader.GetValue(1).ToString();
                        }
                    }
                }
            }
            //nullable
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = "select c.table_schema, " +
                    "c.table_name, " +
                    "c.column_name, " +
                    "c.is_nullable " +
                    "from information_schema.columns c " +
                    "join information_schema.tables t " +
                    "on c.table_schema = t.table_schema " +
                    "and c.table_name = t.table_name " +
                    "where c.table_schema not in ('pg_catalog', 'information_schema') " +
                    "and t.table_type = 'BASE TABLE' " +
                    $"and c.table_name = '{itr.Name}' " +
                    "and c.is_nullable = 'YES' " +
                    "order by table_schema, " +
                    "table_name, " +
                    "column_name;";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(1).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0] == reader.GetValue(2).ToString())
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
            command.CommandText = "select col.table_name as table, " +
                "col.column_name, " +
                "rel.table_name as primary_table, " +
                "rel.column_name as primary_column, " +
                "kcu.constraint_name " +
                "from information_schema.columns col " +
                "left join(select kcu.constraint_schema, " +
                "kcu.constraint_name, " +
                "kcu.table_schema, " +
                "kcu.table_name, " +
                "kcu.column_name, " +
                "kcu.ordinal_position, " +
                "kcu.position_in_unique_constraint " +
                "from information_schema.key_column_usage kcu " +
                "join information_schema.table_constraints tco " +
                "on kcu.constraint_schema = tco.constraint_schema " +
                "and kcu.constraint_name = tco.constraint_name " +
                "and tco.constraint_type = 'FOREIGN KEY' " +
                ") as kcu " +
                "on col.table_schema = kcu.table_schema " +
                "and col.table_name = kcu.table_name " +
                "and col.column_name = kcu.column_name " +
                "left join information_schema.referential_constraints rco " +
                "on rco.constraint_name = kcu.constraint_name " +
                "and rco.constraint_schema = kcu.table_schema " +
                "left join information_schema.key_column_usage rel " +
                "on rco.unique_constraint_name = rel.constraint_name " +
                "and rco.unique_constraint_schema = rel.constraint_schema " +
                "and rel.ordinal_position = kcu.position_in_unique_constraint " +
                "where col.table_schema not in ('information_schema', 'pg_catalog') " +
                "and kcu.constraint_name is not null " +
                "order by col.table_schema, " +
                "col.table_name;";
            reader = command.ExecuteReader();
            FKlistReset();
            while (reader.Read())
            {
                FKList.Add(new List<string> { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString() });
            }
            CloseCon();
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

        protected void setTypes()
        {
            Types.Clear();
            Types.Add(new DBType("CHAR", true));
            Types.Add(new DBType("VARCHAR", true));
            Types.Add(new DBType("TEXT", false));

            Types.Add(new DBType("SERIAL", false));
            Types.Add(new DBType("SMALLSERIAL", false));
            Types.Add(new DBType("BIGSERIAL", false));
            Types.Add(new DBType("SMALLINT", false));
            Types.Add(new DBType("INTEGER", false));
            Types.Add(new DBType("NUMERIC", true));
            Types.Add(new DBType("DECIMAL", true));
            Types.Add(new DBType("BYTEA", false));
            Types.Add(new DBType("TIMESTAMP", false));
            Types.Add(new DBType("DATE", false));
            Types.Add(new DBType("TIME", false));
            Types.Add(new DBType("BOOLEAN", false));
            Types.Add(new DBType("JSON", false));
        }

        public override void UpdateElem(string _tableName, System.Windows.Forms.DataGridViewCellCollection currentRow, System.Windows.Forms.DataGridViewCellCollection checkRow)
        {
            OpenCon();
            List<string[]> changedValues = new List<string[]>(); //1-column   2-value

            List<byte[]> pictures = new List<byte[]>();
            List<string> pictureColumns = new List<string>();

            List<string> columnsNames = new List<string>();
            List<string> values = new List<string>();
            string condition = "";
            string setSTR = "";

            //значения для срванения
            foreach (System.Windows.Forms.DataGridViewCell itr in checkRow)
            {
                if (itr.ValueType != typeof(System.Byte[]) && itr.ValueType != typeof(System.DateTime))
                {
                    columnsNames.Add(itr.OwningColumn.Name);
                    if (itr.Value != null && itr.ValueType != typeof(System.Double))
                    {
                        values.Add(itr.Value.ToString());
                    }
                    else if (itr.ValueType == typeof(System.Double))
                    {
                        values.Add(itr.Value.ToString().Replace(",", "."));
                    }
                    else
                    {
                        values.Add(string.Empty);
                    }

                }
            }

            //формирование измененнных значений
            for (int i = 0; i < currentRow.Count; i++)
            {
                if (currentRow[i].Value != null)
                {
                    Console.WriteLine(currentRow[i].ValueType);
                    if (!currentRow[i].Value.Equals(checkRow[i].Value) && currentRow[i].ValueType != typeof(System.Byte[]) && currentRow[i].ValueType != typeof(System.DateTime) && currentRow[i].ValueType != typeof(System.Double))
                    {
                        changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, currentRow[i].Value.ToString() });
                    }
                    else if (currentRow[i].ValueType == typeof(System.DateTime))
                    {
                        changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, ((DateTime)currentRow[i].Value).ToString("yyyy-MM-dd HH:mm") });
                    }
                    else if (currentRow[i].ValueType == typeof(System.Byte[]))
                    {
                        pictures.Add((byte[])currentRow[i].Value);
                        pictureColumns.Add(currentRow[i].OwningColumn.Name);
                    }
                    else if (currentRow[i].ValueType == typeof(System.Double))
                    {
                        changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, currentRow[i].Value.ToString().Replace(",", ".") });
                    }
                }
                else
                {
                    changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, string.Empty });
                }
            }

            //формирование условия запроса 
            for (int i = 0; i < columnsNames.Count; i++)
            {
                if (values[i] == string.Empty)
                {
                    condition += $" {columnsNames[i]} is null ";
                }
                else
                {
                    condition += $" {columnsNames[i]}='{values[i]}' ";
                }
                if (i < columnsNames.Count - 1)
                {
                    condition += " and ";
                }
            }
            //формирование set запроса
            for (int i = 0; i < changedValues.Count; i++)
            {
                if (changedValues[i][1] == string.Empty)
                {
                    setSTR += $" {changedValues[i][0]} is null";
                }
                else
                {
                    setSTR += $" {changedValues[i][0]}='{changedValues[i][1]}'";
                }
                if (i < changedValues.Count - 1 || pictureColumns.Count > 0)
                {
                    setSTR += " , ";
                }
            }
            command = new NpgsqlCommand();
            command.Connection = conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                setSTR += $" {pictureColumns[i]}=@{pictureColumns[i]} ";
                ((NpgsqlCommand)command).Parameters.AddWithValue($"@{pictureColumns[i]}", pictures[i]);
                if (i < pictureColumns.Count - 1)
                {
                    setSTR += " , ";
                }
            }
            command.CommandText = string.Format("UPDATE {0} " +
                                                "SET {1}" +
                                                " where {2} ;",
                _tableName,
                setSTR,
                condition);
            Console.WriteLine(command.CommandText);
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
        public override void InsertElem(string _tableName, System.Windows.Forms.DataGridViewCellCollection currentRow)
        {
            OpenCon();
            List<string> columnsNames = new List<string>();
            List<string> values = new List<string>();

            List<byte[]> pictures = new List<byte[]>();
            List<string> pictureColumns = new List<string>();

            string valuesStr = "";
            string columnsStr = "";
            // формирование масивов
            for (int i = 0; i < currentRow.Count; i++)
            {
                if (currentRow[i].Value != null)
                {
                    if (currentRow[i].ValueType != typeof(System.Byte[]) && currentRow[i].ValueType != typeof(System.DateTime) && currentRow[i].ValueType != typeof(System.Double))
                    {
                        columnsNames.Add(currentRow[i].OwningColumn.Name);
                        values.Add(currentRow[i].Value.ToString());
                    }
                    else if (currentRow[i].ValueType == typeof(System.DateTime))
                    {
                        columnsNames.Add(currentRow[i].OwningColumn.Name);
                        values.Add(((DateTime)currentRow[i].Value).ToString("yyyy-MM-dd HH:mm"));
                    }
                    else if (currentRow[i].ValueType == typeof(System.Byte[]))
                    {
                        pictures.Add((byte[])currentRow[i].Value);
                        pictureColumns.Add(currentRow[i].OwningColumn.Name);
                    }
                    else if (currentRow[i].ValueType == typeof(System.Double))
                    {
                        columnsNames.Add(currentRow[i].OwningColumn.Name);
                        values.Add(currentRow[i].Value.ToString().Replace(",", "."));
                    }
                }
                else
                {
                    columnsNames.Add(currentRow[i].OwningColumn.Name);
                    values.Add(string.Empty);
                }
            }

            //формирование значений и порядка колонок и запроса
            for (int i = 0; i < columnsNames.Count; i++)
            {
                columnsStr += $" {columnsNames[i]} ";
                if (values[i] == string.Empty)
                {
                    valuesStr += $" null ";
                }
                else
                {
                    valuesStr += $" '{values[i]}' ";
                }
                if (i < columnsNames.Count - 1 || pictureColumns.Count > 0)
                {
                    columnsStr += " , ";
                    valuesStr += " , ";
                }
            }
            command = new NpgsqlCommand();
            command.Connection = conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                columnsStr += $" {pictureColumns[i]} ";
                valuesStr += $" @{pictureColumns[i]} ";
                ((NpgsqlCommand)command).Parameters.AddWithValue($"@{pictureColumns[i]}", pictures[i]);
                if (i < pictureColumns.Count - 1)
                {
                    columnsStr += " , ";
                    valuesStr += " , ";
                }
            }
            command.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});",
                                    _tableName,
                                    columnsStr,
                                    valuesStr);
            Console.WriteLine(command.CommandText);
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

        public override void DropPKByConstraint(string _table, string PKIndex)
        {
            try
            {
                OpenCon();
                command = conn.CreateCommand();
                command.CommandText = $"ALTER TABLE {_table} DROP CONSTRAINT {PKIndex}; ";
                command.ExecuteNonQuery();
                CloseCon();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
        }

        public override void DropUniqueByConstraint(string _table, string PKIndex)
        {
            try
            {
                OpenCon();
                command = conn.CreateCommand();
                command.CommandText = $"ALTER TABLE {_table} DROP CONSTRAINT {PKIndex}; ";
                Console.WriteLine(command.CommandText);
                command.ExecuteNonQuery();
                CloseCon();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
        }

        public override void DeleteColumn(string table, string column)
        {
            try
            {
                OpenCon();
                command = conn.CreateCommand();
                command.CommandText = $"ALTER TABLE {table} DROP COLUMN {column}; ";
                command.ExecuteNonQuery();
                CloseCon();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
        }

        public override void AddColumn(string _table, string _columnname, string _type, string _param, string _pk, string _uq, string _nullable)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"ALTER TABLE {_table} ADD {_columnname} ";
            if (_param == string.Empty)
            {
                command.CommandText += _type;
            }
            else
            {
                command.CommandText += $"{_type}({_param})";
            }
            if (!Convert.ToBoolean(_nullable))
            {
                command.CommandText += $" NOT NULL ";
            }
            if (_pk != string.Empty)
            {
                command.CommandText += $"; ALTER TABLE {_table} ADD CONSTRAINT {_pk} PRIMARY KEY ({_columnname}) ";
            }
            if (_uq != string.Empty)
            {
                command.CommandText += $"; ALTER TABLE {_table} ADD CONSTRAINT {_uq} UNIQUE ({_columnname}) ";
            }
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

        public override void AddTableWithColumn(string _table, string _columnname, string _type, string _param, string _pk, string _uq, string _nullable)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"CREATE TABLE {_table} ( " +
                $" {_columnname} ";
            if (_param == string.Empty)
            {
                command.CommandText += _type;
            }
            else
            {
                command.CommandText += $"{_type}({_param})";
            }
            if (!Convert.ToBoolean(_nullable))
            {
                command.CommandText += $" NOT NULL ";
            }
            if (_pk != string.Empty)
            {
                command.CommandText += $",  PRIMARY KEY({ _columnname}) ";
            }
            command.CommandText += ");";
            if (_uq != string.Empty)
            {
                command.CommandText += $"ALTER TABLE {_table} ADD CONSTRAINT {_uq} UNIQUE  ({_columnname});";
            }
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

        public override void ChangeColumn(string _table, string _oldColumn, string _newColumn)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"ALTER TABLE {_table} RENAME COLUMN {_oldColumn} TO {_newColumn};";
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

        public override void ChangeColumnType(string _table, string _column, string _type, string _parametr)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"ALTER TABLE {_table} ALTER COLUMN {_column}[SET DATA] TYPE ";
            if (_parametr == string.Empty)
            {
                command.CommandText += $" {_type} ;";
            }
            else
            {
                command.CommandText += $" {_type}({_parametr}) ;";
            }
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

        public override void DropFKByConstraint(string _table, string _fk)
        {
            DropUniqueByConstraint(_table, _fk);
        }
    }
}


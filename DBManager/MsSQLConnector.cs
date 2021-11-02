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
                    itr.VariablesNTypes.Add(new List<string> {reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), null, null, null, null });
                }
            }
            //pk index
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = "SELECT TABLE_NAME, COLUMN_NAME,CONSTRAINT_NAME" +
                    " FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                    " WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1" +
                    $" AND TABLE_NAME = '{itr.Name}' ";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(0).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0] == reader.GetValue(1).ToString())
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
                command.CommandText = "select t.[name] as table_view, " +
                    " case when t.[type] = 'U' then 'Table'" +
                    " when t.[type] = 'V' then 'View'" +
                    " end as [object_type]," +
                    " case when c.[type] = 'UQ' then 'Uniq'" +
                    " when i.[type] = 1 then 'Uniq'" +
                    " when i.type = 2 then 'Uniq'" +
                    " end as constraint_type, " +
                    " c.[name] as constraint_name," +
                    " substring(column_names, 1, len(column_names) - 1) as [columns]," +
                    " i.[name] as index_name," +
                    " case when i.[type] = 1 then 'Clustered index'" +
                    " when i.type = 2 then 'Index'" +
                    " end as index_type" +
                    " from sys.objects t" +
                    " left outer join sys.indexes i" +
                    " on t.object_id = i.object_id" +
                    " left outer join sys.key_constraints c" +
                    " on i.object_id = c.parent_object_id" +
                    " and i.index_id = c.unique_index_id" +
                    " cross apply(select col.[name] +', '" +
                    " from sys.index_columns ic" +
                    " inner" +
                    " join sys.columns col" +
                    " on ic.object_id = col.object_id" +
                    " and ic.column_id = col.column_id" +
                    " where ic.object_id = t.object_id" +
                    " and ic.index_id = i.index_id" +
                    " order by col.column_id" +
                    " for xml path ('') ) D(column_names)" +
                    $" where is_unique = 1 and(c.[type] = 'UQ' or i.[type] = 1 or i.type = 2) and t.[name] = '{itr.Name}' and c.[name] is not null" +
                    " and t.is_ms_shipped <> 1" +
                    " order by schema_name(t.schema_id) + '.' + t.[name]";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var found = TablesTypes.Find(el => el.Name == reader.GetValue(0).ToString());
                    for (int i = 0; i < found.VariablesNTypes.Count(); i++)
                    {
                        if (found.VariablesNTypes[i][0] == reader.GetValue(4).ToString())
                        {
                            found.VariablesNTypes[i][3] = reader.GetValue(3).ToString();
                        }
                    }
                }
            }
            //nullable
            foreach (var itr in TablesTypes)
            {
                reader.Close();
                command.CommandText = $"USE {database};" +
                    " SELECT TABLE_NAME, COLUMN_NAME, IS_NULLABLE" +
                    " FROM INFORMATION_SCHEMA.COLUMNS" +
                    " WHERE TABLE_SCHEMA = 'dbo'" +
                    $" AND TABLE_NAME = '{itr.Name}'" +
                    " AND IS_NULLABLE = 'YES'";
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
                "tab.name as [table], " +
                "col.name as column_name, " +
                "pk_tab.name as primary_table, " +
                "pk_col.name as pk_column_name, " +
                "fk.name as fk_constraint_name " +
                "from sys.tables tab " +
                "inner " +
                "join sys.columns col " +
                "on col.object_id = tab.object_id " +
                "left outer join sys.foreign_key_columns fk_cols " +
                "on fk_cols.parent_object_id = tab.object_id " +
                "and fk_cols.parent_column_id = col.column_id " +
                "left outer join sys.foreign_keys fk " +
                "on fk.object_id = fk_cols.constraint_object_id " +
                "left outer join sys.tables pk_tab " +
                "on pk_tab.object_id = fk_cols.referenced_object_id " +
                "left outer join sys.columns pk_col " +
                "on pk_col.column_id = fk_cols.referenced_column_id " +
                "and pk_col.object_id = fk_cols.referenced_object_id " +
                "where fk.name is not null " +
                "order by schema_name(tab.schema_id) + '.' + tab.name, " +
                "col.column_id";
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
            Types.Add(new DBType("CHAR", false));
            Types.Add(new DBType("VARCHAR", true));
            Types.Add(new DBType("NCHAR", true));
            Types.Add(new DBType("NVARCHAR", true));

            Types.Add(new DBType("TINYINT", false));
            Types.Add(new DBType("SMALLINT", false));
            Types.Add(new DBType("BIT", false));
            Types.Add(new DBType("INT", false));
            Types.Add(new DBType("BIGINT", false));
            Types.Add(new DBType("DECIMAL", true));
            Types.Add(new DBType("FLOAT", false));
            Types.Add(new DBType("DOUBLE", false));
            Types.Add(new DBType("DATETIME", false));
            Types.Add(new DBType("DATE", false));
            Types.Add(new DBType("TIME", true));
            Types.Add(new DBType("BINARY", false));
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
            command = new SqlCommand();
            command.Connection = conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                setSTR += $" {pictureColumns[i]}=@{pictureColumns[i]} ";
                ((SqlCommand)command).Parameters.AddWithValue($"@{pictureColumns[i]}", pictures[i]);
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
            command = new SqlCommand();
            command.Connection = conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                columnsStr += $" {pictureColumns[i]} ";
                valuesStr += $" @{pictureColumns[i]} ";
                ((SqlCommand)command).Parameters.AddWithValue($"@{pictureColumns[i]}", pictures[i]);
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
                command.ExecuteNonQuery();
                Console.WriteLine(command.CommandText);
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

        public override void AddColumn(string _table, string _columnname, string _type, string _param, string _pk, string _uq, string _nullable) {
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
            command.CommandText = $"EXECUTE sp_RENAME '{database}.dbo.{_table}.{_oldColumn}', '{_newColumn}', 'COLUMN';";
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
            command.CommandText = $"ALTER TABLE {_table} ALTER COLUMN {_column} ";
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

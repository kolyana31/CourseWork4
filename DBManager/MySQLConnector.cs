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
            Types.Add(new DBType("CHAR", true));
            Types.Add(new DBType("VARCHAR", true));
            Types.Add(new DBType("TINYTEXT", false));
            Types.Add(new DBType("TEXT", false));
            Types.Add(new DBType("BLOB", true));
            Types.Add(new DBType("MEDIUMTEXT", false));
            Types.Add(new DBType("MEDIUMBLOB", false));
            Types.Add(new DBType("LONGTEXT", false));
            Types.Add(new DBType("LONGBLOB", false));

            Types.Add(new DBType("TINYINT", false));
            Types.Add(new DBType("SMALLINT", false));
            Types.Add(new DBType("MEDIUMINT", false));
            Types.Add(new DBType("INT", false));
            Types.Add(new DBType("BIGINT", false));
            Types.Add(new DBType("DECIMAL", true));
            Types.Add(new DBType("FLOAT", false));
            Types.Add(new DBType("DOUBLE", false));
            Types.Add(new DBType("DATETIME", true));
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
                        values.Add(itr.Value.ToString().Replace(",","."));
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
                        changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, ((DateTime)currentRow[i].Value).ToString("yyyy-MM-dd HH:mm")});
                    }
                    else if (currentRow[i].ValueType == typeof(System.Byte[]))
                    {
                        pictures.Add((byte[])currentRow[i].Value);
                        pictureColumns.Add(currentRow[i].OwningColumn.Name);
                    }
                    else if (currentRow[i].ValueType == typeof(System.Double))
                    {
                        changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, currentRow[i].Value.ToString().Replace(",",".")});
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
            command = new MySqlCommand();
            command.Connection = conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                setSTR += $" {pictureColumns[i]}=@{pictureColumns[i]} ";
                ((MySqlCommand)command).Parameters.AddWithValue($"@{pictureColumns[i]}",pictures[i]);
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
            command = new MySqlCommand();
            command.Connection = conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                columnsStr += $" {pictureColumns[i]} ";
                valuesStr += $" @{pictureColumns[i]} ";
                ((MySqlCommand)command).Parameters.AddWithValue($"@{pictureColumns[i]}", pictures[i]);
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

        public override void DropPKByConstraint(string _table, string NaN)
        {
            try
            {
                OpenCon();
                command = conn.CreateCommand();
                command.CommandText = $"ALTER TABLE {_table} DROP Primary key; ";
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
                command.CommandText = $"ALTER TABLE {_table} DROP INDEX {PKIndex}; ";
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
                command.CommandText = $"ALTER TABLE {table} DROP  {column}; ";
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
            command.CommandText = $"CREATE TABLE `{database}`.`{_table}` ( " +
                $"`{_columnname}` ";
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
                command.CommandText += $",  PRIMARY KEY(`{ _columnname}`) ";
            }
            if (_uq != string.Empty)
            {
                command.CommandText += $", UNIQUE INDEX `{_uq}` (`{_columnname}` ASC) VISIBLE ";
            }
            command.CommandText += ");";
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
            command.CommandText = $"ALTER TABLE {_table} MODIFY {_column} ";
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
    }
}

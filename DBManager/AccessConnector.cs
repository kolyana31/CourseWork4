using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CourseWork2
{
    public class AccessConnector : DataBases
    {
        public OleDbCommand ACcommand = new OleDbCommand();
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
            setTypes();
        }

        public override List<object> getDataBaseTables()
        {
            OpenCon();
            List<object> Temp = new List<object>();
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
                CloseCon();
                return Temp;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                CloseCon();
                return null;
            }
        }

        public override DataTable selectFrom(string Form)
        {
            OpenCon();
            ACcommand = (OleDbCommand)conn.CreateCommand();
            ACcommand.CommandText = $"select * from [{Form}]";
            OleDbDataAdapter da = new OleDbDataAdapter(ACcommand.CommandText, (OleDbConnection)conn);
            dataSet.Reset();
            da.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            CloseCon();
            return dataTable;
        }

        public override void setStructureOfDatabase()
        {
            OpenCon();
            TablesTypes.Clear();
            ACcommand.Connection = (OleDbConnection)conn;
            DataTable schemaTbl;
            try
            {
                foreach (string itr in getDataBaseTables())
                {
                    OpenCon();
                    TablesTypes.Add(new DBTableStructure(itr));
                    schemaTbl = ((OleDbConnection)conn).GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, itr, null });
                    foreach (DataRow row in schemaTbl.Rows)
                    {
                        TablesTypes.Last().VariablesNTypes.Add(new List<string> { row["COLUMN_NAME"].ToString(), ((OleDbType)Convert.ToInt32(row["DATA_TYPE"].ToString())).ToString(), null, null, null, null });
                    }
                }
                GetSchemaPrimaryKeysList();
                GetSchemaUNIQUEist();
                GetSchemaNullables();
                GetFKList();
                //ConsoleLogTable();
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            CloseCon();
        }

        public void GetFKList()
        {
            OpenCon();
            FKlistReset();
            foreach (DBTableStructure elem in TablesTypes)
            {
                DataTable schemaTable = ((OleDbConnection)conn).GetOleDbSchemaTable(
                                            OleDbSchemaGuid.Foreign_Keys,
                                            new object[] { null, null, elem.Name });
                //foreach (DataRow itr in schemaTable.Rows)
                //{
                //    Console.WriteLine("------------------------------");
                //    foreach (DataColumn itr2 in schemaTable.Columns)
                //    {
                //        Console.WriteLine($"{itr2.ColumnName} = {itr[itr2.ColumnName]}");
                //    }
                //    //Console.WriteLine($"Table: {itr.ItemArray[2]} || Primary: {itr.ItemArray[3]}");
                //}

                foreach (DataRow itr in schemaTable.Rows)
                {
                    FKList.Add(new List<string> { itr["FK_TABLE_NAME"].ToString(), itr["FK_COLUMN_NAME"].ToString(), itr["PK_TABLE_NAME"].ToString(), itr["PK_COLUMN_NAME"].ToString(), itr["FK_NAME"].ToString() });
                }
            }
            CloseCon();
        }

        public void GetSchemaPrimaryKeysList()
        {
            OpenCon();
            foreach (DBTableStructure elem in TablesTypes)
            {
                DataTable schemaTable = ((OleDbConnection)conn).GetOleDbSchemaTable(
                                            OleDbSchemaGuid.Primary_Keys,
                                            new object[] { null, null, elem.Name });
                //foreach (DataRow itr in schemaTable.Rows)
                //{
                //    foreach (DataColumn itr2 in schemaTable.Columns)
                //    {
                //        Console.WriteLine($"{itr2.ColumnName} = {itr[itr2.ColumnName]}");
                //    }
                //    //Console.WriteLine($"Table: {itr.ItemArray[2]} || Primary: {itr.ItemArray[3]}");
                //}
                foreach (DataRow itr in schemaTable.Rows)
                {
                    List<string> found = elem.VariablesNTypes.Find(x => x[0] == itr["COLUMN_NAME"].ToString());
                    if (found!=null)
                    {
                        found[2] = itr["PK_NAME"].ToString();
                    }
                }
            }
            CloseCon();
        }

        public void GetSchemaUNIQUEist()
        {
            OpenCon();
            foreach (DBTableStructure itr in TablesTypes)
            {
                DataTable columns = ((OleDbConnection)conn).GetOleDbSchemaTable(OleDbSchemaGuid.Indexes,
                                    new Object[] { null, null, null, null, itr.Name });

                foreach (DataRow row in columns.Rows)
                {
                    List<string> found = itr.VariablesNTypes.Find(x => x[0] == row["COLUMN_NAME"].ToString());
                    if (found != null && Convert.ToBoolean(row["UNIQUE"]))
                    {
                        found[3] = row["INDEX_NAME"].ToString();
                    }
                    //Console.WriteLine("//////////////////////");
                    //Console.WriteLine(row["COLUMN_NAME"].ToString());
                    //Console.WriteLine(row["TABLE_NAME"].ToString());
                    //Console.WriteLine(row["UNIQUE"].ToString());
                    //Console.WriteLine(row["INDEX_NAME"].ToString());
                }
            }
            CloseCon();
        }

        public void GetSchemaNullables(){
            OpenCon();
            foreach (DBTableStructure itr in TablesTypes)
            {
                string[] rest = new string[] { null, null, itr.Name, null };
                var schema = ((OleDbConnection)conn).GetSchema("COLUMNS", rest);
                foreach (DataRow row in schema.Rows)
                {
                    List<string> found = itr.VariablesNTypes.Find(x => x[0] == row["COLUMN_NAME"].ToString());
                    if (found != null)
                    {
                        found[4] = row["IS_NULLABLE"].ToString();
                    }
                }
            }
            CloseCon();
        }

        public override void DeleteInfo(string _tableName,System.Windows.Forms.DataGridViewCellCollection currentRow)
        {
            OpenCon();
            List<string> columnsNames = new List<string>();
            List<string> values = new List<string>();
            string terms = "";

            foreach (System.Windows.Forms.DataGridViewCell itr in currentRow)
            {
                if (itr.ValueType != typeof(System.Byte[]))
                {
                    if (itr.ValueType == typeof(System.Boolean))
                    {
                        columnsNames.Add($"[{itr.OwningColumn.Name}]");
                        if ((bool)itr.Value == false)
                        {
                            values.Add("0");
                        }
                        else
                        {
                            values.Add("-1");
                        }
                    }
                    else
                    {
                        columnsNames.Add($"[{itr.OwningColumn.Name}]");
                        values.Add(itr.Value.ToString());
                    }

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
                    terms += $" {columnsNames[i]} Like '{values[i]}' ";
                }
                
                if (i < columnsNames.Count - 1)
                {
                    terms += " and ";
                }
            }
            ACcommand.Connection = (OleDbConnection)conn;
            ACcommand.CommandText = string.Format("DELETE FROM {0} where {1}",
                _tableName,
                terms);
            Console.WriteLine(ACcommand.CommandText);
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }


        public override void DeleteALL(string _table)
        {
            OpenCon();
            ACcommand.CommandText = $"DELETE FROM {_table}";
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }

        public override void DeleteTable(string _tableName)
        {
            OpenCon();
            ACcommand = ((OleDbConnection)conn).CreateCommand();
            ACcommand.CommandText = $"DROP TABLE [{_tableName}];";
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
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
                    if (itr.Value != null && itr.ValueType != typeof(System.Double) && itr.ValueType != typeof(System.Boolean))
                    {
                        values.Add(itr.Value.ToString());
                    }
                    else if (itr.ValueType == typeof(System.Double))
                    {
                        values.Add(itr.Value.ToString().Replace(",", "."));
                    }
                    else if (itr.ValueType == typeof(System.Boolean))
                    {
                        if ((bool)itr.Value == true)
                        {
                            values.Add("-1");
                        }
                        else
                        {
                            values.Add("0");
                        }
                        
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
                    if (!currentRow[i].Value.Equals(checkRow[i].Value) && currentRow[i].ValueType != typeof(System.Byte[]) && currentRow[i].ValueType != typeof(System.DateTime) && currentRow[i].ValueType != typeof(System.Double) && currentRow[i].ValueType != typeof(System.Boolean))
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
                    else if (currentRow[i].ValueType == typeof(System.Boolean))
                    {
                        if ((bool)currentRow[i].Value == true)
                        {
                            changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, "-1" });
                        }
                        else
                        {
                            changedValues.Add(new string[] { currentRow[i].OwningColumn.Name, "0" });
                        }
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
                    condition += $" [{columnsNames[i]}] is null ";
                }
                else
                {
                    condition += $" [{columnsNames[i]}] like'{values[i]}' ";
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
                    setSTR += $" [{changedValues[i][0]}] = null";
                }
                else
                {
                    setSTR += $" [{changedValues[i][0]}]='{changedValues[i][1]}'";
                }
                if (i < changedValues.Count - 1 || pictureColumns.Count > 0)
                {
                    setSTR += " , ";
                }
            }
            ACcommand.Connection = (OleDbConnection)conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                setSTR += $" {pictureColumns[i]}=@{pictureColumns[i]} ";
                ((OleDbCommand)ACcommand).Parameters.AddWithValue($"@{pictureColumns[i]}", pictures[i]);
                if (i < pictureColumns.Count - 1)
                {
                    setSTR += " , ";
                }
            }
            ACcommand.CommandText = string.Format("UPDATE {0} " +
                                                "SET {1}" +
                                                " where {2} ;",
                _tableName,
                setSTR,
                condition);
            Console.WriteLine(ACcommand.CommandText);
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }

        protected void setTypes()
        {
            Types.Clear();
            Types.Add(new DBType("COUNTER", false));
            Types.Add(new DBType("BYTE", false));
            Types.Add(new DBType("SMALLINT", false));
            Types.Add(new DBType("INTEGER", false));
            Types.Add(new DBType("REAL", true));
            Types.Add(new DBType("FLOAT", false));
            Types.Add(new DBType("DECIMAL", true));
            Types.Add(new DBType("MONEY", false));
            Types.Add(new DBType("CHAR", false));
            Types.Add(new DBType("MEMO", false));
            Types.Add(new DBType("DATETIME", false));
            Types.Add(new DBType("BIT", false));
            Types.Add(new DBType("IMAGE", false));
            Types.Add(new DBType("UNIQUEIDENTIFIER", false));
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
                    if (currentRow[i].ValueType != typeof(System.Byte[]) && currentRow[i].ValueType != typeof(System.DateTime) && currentRow[i].ValueType != typeof(System.Double) && currentRow[i].ValueType != typeof(System.Boolean))
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
                    else if (currentRow[i].ValueType == typeof(System.Boolean))
                    {
                        columnsNames.Add(currentRow[i].OwningColumn.Name);
                        if ((bool)currentRow[i].Value == true)
                        {
                            values.Add("-1");
                        }
                        else
                        {
                            values.Add("0");
                        }
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
                columnsStr += $" [{columnsNames[i]}] ";
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
            ACcommand = new OleDbCommand();
            ACcommand.Connection = (OleDbConnection)conn;
            for (int i = 0; i < pictureColumns.Count; i++)
            {
                columnsStr += $" {pictureColumns[i]} ";
                valuesStr += $" @{pictureColumns[i]} ";
                ((OleDbCommand)ACcommand).Parameters.AddWithValue($"@{pictureColumns[i]}", pictures[i]);
                if (i < pictureColumns.Count - 1)
                {
                    columnsStr += " , ";
                    valuesStr += " , ";
                }
            }
            ACcommand.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});",
                                    _tableName,
                                    columnsStr,
                                    valuesStr);
            Console.WriteLine(ACcommand.CommandText);
            try
            {
                ACcommand.ExecuteNonQuery();
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
                ACcommand = ((OleDbConnection)conn).CreateCommand();
                ACcommand.CommandText = $"ALTER TABLE [{_table}] DROP CONSTRAINT [{PKIndex}]; ";
                ACcommand.ExecuteNonQuery();
                Console.WriteLine(ACcommand.CommandText);
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
                ACcommand = ((OleDbConnection)conn).CreateCommand();
                ACcommand.CommandText = $"ALTER TABLE [{_table}] DROP CONSTRAINT [{PKIndex}]; ";
                ACcommand.ExecuteNonQuery();
                Console.WriteLine(ACcommand.CommandText);
                CloseCon();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
        }

        public override void AddPrimaryKey(string _table, string _column, string _pk)
        {
            OpenCon();
            ACcommand = ((OleDbConnection)conn).CreateCommand();
            ACcommand.CommandText = $"ALTER TABLE [{_table}] ADD CONSTRAINT {_pk} PRIMARY KEY([{_column}]); ";
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }

        public override void AddUniqueKey(string _table, string _column, string _pk)
        {
            OpenCon();
            ACcommand = ((OleDbConnection)conn).CreateCommand();
            ACcommand.CommandText = $"ALTER TABLE [{_table}] ADD CONSTRAINT {_pk} UNIQUE ([{_column}]); ";
            try
            {
                ACcommand.ExecuteNonQuery();
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
            ACcommand = ((OleDbConnection)conn).CreateCommand();
            ACcommand.CommandText = $"ALTER TABLE [{_table}] ALTER COLUMN [{_column}] ";
            if (_parametr == string.Empty)
            {
                ACcommand.CommandText += $" {_type} ;";
            }
            else
            {
                ACcommand.CommandText += $" {_type}({_parametr}) ;";
            }
            try
            {
                Console.WriteLine(ACcommand.CommandText);
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }

        public override void DeleteColumn(string table, string column)
        {
            try
            {
                OpenCon();
                ACcommand = ((OleDbConnection)conn).CreateCommand();
                ACcommand.CommandText = $"ALTER TABLE [{table}] DROP COLUMN {column}; ";
                ACcommand.ExecuteNonQuery();
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
            ACcommand = ((OleDbConnection)conn).CreateCommand();
            ACcommand.CommandText = $"ALTER TABLE [{_table}] ADD [{_columnname}] ";
            if (_param == string.Empty)
            {
                ACcommand.CommandText += _type;
            }
            else
            {
                ACcommand.CommandText += $"{_type}({_param})";
            }
            if (!Convert.ToBoolean(_nullable))
            {
                ACcommand.CommandText += $" NOT NULL ";
            }
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }

            if (_pk != string.Empty)
            {
                OpenCon();
                ACcommand.CommandText = "";
                ACcommand.CommandText += $"ALTER TABLE [{_table}] ADD CONSTRAINT [{_pk}] PRIMARY KEY ([{_columnname}]) ";
                try
                {
                    ACcommand.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    ErrorShower(err);
                }
            }
            if (_uq != string.Empty)
            {
                OpenCon();
                ACcommand.CommandText = "";
                ACcommand.CommandText += $"ALTER TABLE [{_table}] ADD CONSTRAINT [{_uq}] UNIQUE ([{_columnname}]) ";
                try
                {
                    ACcommand.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    ErrorShower(err);
                }
            }
            CloseCon();
        }

        public override void AddTableWithColumn(string _table, string _columnname, string _type, string _param, string _pk, string _uq, string _nullable)
        {
            OpenCon();
            command = conn.CreateCommand();
            command.CommandText = $"CREATE TABLE [{_table}] ( " +
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
                command.CommandText += $",  PRIMARY KEY([{ _columnname}]) ";
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
            if (_uq != string.Empty)
            {
                OpenCon();
                command.CommandText = $"ALTER TABLE [{_table}] ADD CONSTRAINT [{_uq}] UNIQUE  ([{_columnname}]);";
                try
                {
                    Console.WriteLine(command.CommandText);
                    command.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    ErrorShower(err);
                }
                
            }
            CloseCon();
        }

        public override void DropFKByConstraint(string _table, string _fk)
        {
            DropUniqueByConstraint(_table, _fk);
        }

        public override void AddFKByConstraint(string _table, string _FK, string _toColumn, string _refTable, string _refColumn)
        {
            OpenCon();
            ACcommand.CommandText = $"ALTER TABLE [{_table}] ADD CONSTRAINT [{_FK}] FOREIGN KEY([{_toColumn}]) REFERENCES [{_refTable}]([{_refColumn}]); ";
            try
            {
                ACcommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ErrorShower(err);
            }
            CloseCon();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace Kame.Core.Entity.Log
{
    public class LogSchema
    {/*
        public string TableName { get; set; }
        public LogSchemaColunm[] Colunms { get; set; }

        public void InsertCommand(SqlCeConnection connection, params object[] values)
        {
            StringBuilder sqlCommand = new StringBuilder();
            SqlCeCommand command = new SqlCeCommand();
            command.Connection = connection;

            sqlCommand.Append("INSERT INTO " + this.TableName + "(");

            for (int i = 0; i < this.Colunms.Length; i++)
            {
                if (i > 0)
                {
                    sqlCommand.Append(",");
                }
                sqlCommand.Append(this.Colunms[i].Name);
            }
            sqlCommand.Append(") VALUES(");
            for (int i = 0; i < this.Colunms.Length; i++)
            {
                if (i > 0)
                {
                    sqlCommand.Append(",");
                }
                sqlCommand.Append("@p" + (i + 1));

                command.Parameters.Add("p" + (i + 1), (values[i] == null ? DBNull.Value : values[i]));
            }
            sqlCommand.Append(")");

            command.CommandText = sqlCommand.ToString();
            command.ExecuteNonQuery();
        }

        public void CheckLoalLogSchema(SqlCeConnection connection)
        {
            SqlCeCommand command = new SqlCeCommand("SELECT count(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME='" + this.TableName.Replace("'", "''") + "'", connection);
            int qtdeTabelas = (int)command.ExecuteScalar();

            StringBuilder sqlText;
            if (qtdeTabelas == 0)
            {
                sqlText = new StringBuilder();
                sqlText.Append("CREATE TABLE " + this.TableName.Replace("'", "") + " (");

                for (int i = 0; i < this.Colunms.Length; i++)
                {
                    if (i > 0)
                    {
                        sqlText.Append(",");
                    }
                    sqlText.Append(this.Colunms[i].Name + " " + this.Colunms[i].DataType);
                }

                sqlText.Append(")");

                command = new SqlCeCommand(sqlText.ToString(), connection);
                command.ExecuteNonQuery();
            }
        }
        */
    }

    public class LogSchemaColunm
    {
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}

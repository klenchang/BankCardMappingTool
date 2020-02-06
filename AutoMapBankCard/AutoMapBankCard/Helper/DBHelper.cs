using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AutoMapBankCard.Helper
{
    public class DBHelper
    {
        private string _conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public void DeleteAllBankCardList()
        {
            string sql = @"TRUNCATE TABLE BankCardList";
            ExecuteNonQuery(sql);
        }
        public void InsertFullBankCardList(DataTable dt)
        {
            using (var copy = new SqlBulkCopy(_conn))
            {
                copy.BatchSize = 3;
                copy.BulkCopyTimeout = 10;
                copy.DestinationTableName = "BankCardList";
                copy.ColumnMappings.Add(dt.Columns["Ac. Name"].ColumnName, "AccountName");
                copy.ColumnMappings.Add(dt.Columns["Ac. Number"].ColumnName, "AccountNumber");
                copy.ColumnMappings.Add(dt.Columns["Issuing Bank Address:"].ColumnName, "IssuingBankAddress");
                copy.WriteToServer(dt);
            }
        }
        public DataTable GetBankCardList(int startIndex, int endIndex, bool isGetAll = false)
        {
            DataTable dt = new DataTable();
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT SerialNo, AccountName, AccountNumber, IssuingBankAddress FROM BankCardList WITH(NOLOCK) ");
            if (!isGetAll)
                sqlBuilder.Append("WHERE SerialNo BETWEEN @StartIndex AND @EndIndex");
            using (var adapter = new SqlDataAdapter(sqlBuilder.ToString(), _conn))
            {
                adapter.SelectCommand.Parameters.AddRange(
                    new SqlParameter[]
                    {
                        new SqlParameter("@StartIndex", startIndex),
                        new SqlParameter("@EndIndex", endIndex)
                    });
                adapter.Fill(dt);
            }

            return dt;
        }
        private void ExecuteNonQuery(string commandText)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand commandObj = new SqlCommand(commandText, conn))
                {
                    commandObj.CommandType = CommandType.Text;
                    commandObj.ExecuteNonQuery();
                }
            }
        }
    }
}
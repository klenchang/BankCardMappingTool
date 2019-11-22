using System;
using System.Data;
using System.Data.SqlClient;

namespace AutoMapBankCard
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
        public DataTable GetBankCardList(int startIndex, int endIndex)
        {
            DataTable dt = new DataTable();
            var sql = "SELECT SerialNo, AccountName, AccountNumber, IssuingBankAddress FROM BankCardList WITH(NOLOCK) WHERE SerialNo BETWEEN @StartIndex AND @EndIndex";
            using (var adapter = new SqlDataAdapter(sql, _conn))
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
        public bool IsBankCardExsit(string accountNo, string accountName, string issueBankAddress)
        {
            var sql = @"IF EXISTS (SELECT 1 FROM BankCardList WITH(NOLOCK) WHERE AccountName LIKE @AccountName AND AccountNumber LIKE @AccountNumber AND IssuingBankAddress LIKE @IssuingBankAddress)
                        BEGIN
                            SELECT 1;
                        END
                        ELSE
                        BEGIN
                            SELECT 0;
                        END";
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand commandObj = new SqlCommand(sql, conn))
                {
                    commandObj.CommandType = CommandType.Text;
                    commandObj.Parameters.AddRange(
                        new SqlParameter[]
                        {
                            new SqlParameter() { ParameterName = "@AccountName", Value = $"{accountName}%", SqlDbType = SqlDbType.NVarChar, Size = 10 },
                            new SqlParameter() { ParameterName = "@AccountNumber", Value = $"%{accountNo}", SqlDbType = SqlDbType.VarChar, Size = 20 },
                            new SqlParameter() { ParameterName = "@IssuingBankAddress", Value = $"{issueBankAddress}%", SqlDbType = SqlDbType.NVarChar, Size = 30 }
                        });
                    return Convert.ToBoolean(commandObj.ExecuteScalar());
                }
            }
        }
        public int GetBankCardCount()
        {
            var sql = "SELECT COUNT(1) FROM BankCardList WITH(NOLOCK)";
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand commandObj = new SqlCommand(sql, conn))
                {
                    commandObj.CommandType = CommandType.Text;
                    return Convert.ToInt32(commandObj.ExecuteScalar());
                }
            }
        }
    }
}
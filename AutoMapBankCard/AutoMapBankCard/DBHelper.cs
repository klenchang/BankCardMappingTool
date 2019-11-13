using System.Data;
using System.Data.SqlClient;

namespace AutoMapBankCard
{
    public class DBHelper
    {
        private string _conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public void DeleteAllBankCardList()
        {
            string sql = @"DELETE FROM [BankCardList]";
            ExecuteNonQuery(sql);
        }
        public void InsertFullBankCardList(DataTable dt)
        {
            using (var copy = new SqlBulkCopy(_conn))
            {
                copy.BatchSize = 3;
                copy.BulkCopyTimeout = 10;
                copy.DestinationTableName = "BankCardList";
                copy.ColumnMappings.Add(dt.Columns[0].ColumnName, "AccountName");
                copy.ColumnMappings.Add(dt.Columns[1].ColumnName, "AccountNumber");
                copy.ColumnMappings.Add(dt.Columns[2].ColumnName, "IssuingBankAddress");
                copy.WriteToServer(dt);
            }
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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapBankCard.Helper;
using System.Text;

namespace AutoMapBankCard.Utility
{
    public class GeneralUtility
    {
        public static string ConvertToKeyValueString(Dictionary<string, string> dictionary)
            => dictionary.Select(d => $"{d.Key}={d.Value}").Aggregate((result, next) => $"{result}&{next}");

        public static List<T> ConvertDataTableToList<T>(DataTable dt) where T : class, new()
        {
            var result = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                var item = new T();
                foreach (DataColumn dc in dt.Columns)
                {
                    var value = "";
                    if (dc.ColumnName != "SerialNo")
                        value = CryptoHelper.AES.DecryptFromBase64String(dr[dc.ColumnName].ToString(), CryptoHelper.AES.Key, Encoding.UTF8, cipherMode: System.Security.Cryptography.CipherMode.ECB);
                    else
                        value = dr[dc.ColumnName].ToString();

                    item.GetType().GetProperty(dc.ColumnName).SetValue(item, value, null);
                }

                result.Add(item);
            }
            return result;
        }
    }
}
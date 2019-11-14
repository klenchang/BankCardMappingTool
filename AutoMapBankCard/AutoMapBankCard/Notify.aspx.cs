using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace AutoMapBankCard
{
    public partial class Notify : System.Web.UI.Page
    {
        DBHelper _dBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var content = "";
                using (var reader = new StreamReader(Request.InputStream, Encoding.UTF8))
                    content = reader.ReadToEnd();

                if (!string.IsNullOrWhiteSpace(content))
                {
                    var jObj = JsonTryParse(content);
                    if (jObj != null)
                    {
                        var jResult = new JObject();
                        foreach (var property in jObj.Properties())
                        {
                            var card = property.Value;
                            var accountNo = card["AccountNo"]?.ToString();
                            var accountName = card["AccountName"]?.ToString();
                            var issueBankAddress = card["IssueBankAddress"]?.ToString();
                            var isExist = _dBHelper.IsBankCardExsit(accountNo, accountName, issueBankAddress);
                            if (!isExist)
                                jResult.Add(property.Name, "not exist");
                        }
                        Response.Write(jResult.ToString());
                    }
                    else
                        Response.Write("content is not json format");
                }
                else
                    Response.Write("content is empty");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private JObject JsonTryParse(string json)
        {
            try
            {
                return JObject.Parse(json);  
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace AutoMapBankCard
{
    public partial class Notify : System.Web.UI.Page
    {
        DBHelper _dBHelper = new DBHelper();
        private string teamsUrl = System.Configuration.ConfigurationManager.AppSettings["TeamsUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            var status = 0;
            var msg = "";
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
                        if (jObj.Count > 0)
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
                            msg = SendTeamsMsg("No card need to be verified");
                    }
                    else
                        msg = SendTeamsMsg("Content is not json format");
                }
                else
                    msg = SendTeamsMsg("Content is empty");
            }
            catch (Exception ex)
            {
                msg = SendErrorMsgToTeams(ex.Message);
                Response.Write(new JObject { { "status", status }, { "message", msg } }.ToString(Newtonsoft.Json.Formatting.None));
                Response.End();
                return;
            }
            Response.Write(new JObject { { "status", 1 }, { "message", msg } }.ToString(Newtonsoft.Json.Formatting.None));
            Response.End();
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
        private string SendTeamsMsg(string text)
        {
            var content = new JObject
            {
                { "title", "Bank Card Verification" },
                { "text", text }
            };
            var request = new HttpRequestUtility()
            {
                FormMethod = HttpMethod.Post,
                ContentType = "application/json",
                FormContent = new StringContent(content.ToString()),
                Url = teamsUrl,
                Timeout = 3
            };
            using (var resp = request.Submit()) { }

            return text;
        }
        private string SendErrorMsgToTeams(string text)
        {
            try
            {
                SendTeamsMsg(text);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return text;
        }
    }
}
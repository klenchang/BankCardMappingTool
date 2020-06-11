using AutoMapBankCard.Helper;
using AutoMapBankCard.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;

namespace AutoMapBankCard.Page
{
    public partial class Notify : System.Web.UI.Page
    {
        private string teamsUrl = System.Configuration.ConfigurationManager.AppSettings["TeamsUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            var status = 0;
            var content = "";
            var msg = "";
            try
            {
                using (var reader = new StreamReader(Request.InputStream, Encoding.UTF8))
                    content = reader.ReadToEnd();

                if (!string.IsNullOrWhiteSpace(content))
                {
                    var jObj = JsonTryParse(content);
                    if (jObj != null)
                    {
                        if (jObj.Count > 0)
                        {
                            var sourceList = new List<string>();
                            var resultList = new List<string>();
                            foreach (var property in jObj.Properties())
                            {
                                var card = property.Value;
                                var accountNo = card["AccountNo"]?.ToString();
                                var accountName = card["AccountName"]?.ToString();
                                var issueBankAddress = card["IssueBankAddress"]?.ToString();
                                sourceList.Add($"{property.Name}: {accountNo} {accountName} {issueBankAddress}");
                                var isExist = BankCardHelper.IsBankCardExsit(accountNo, accountName?.Substring(0, 1), issueBankAddress);
                                if (!isExist)
                                    resultList.Add(property.Name);
                            }
                            //send result
                            msg = SendResultMsgToTeams(sourceList, resultList);
                        }
                        else
                            msg = SendPureMsgToTeams(content, "No card need to be verified.");
                    }
                    else
                        msg = SendPureMsgToTeams(content, "Content is not json format, please check your input.");
                }
                else
                    msg = SendPureMsgToTeams(content, "Content is empty, please check your input.");

                status = 1;
            }
            catch (Exception ex)
            {
                msg = SendErrorMsgToTeams(content, ex.ToString());
            }
            Response.Write(new JObject { { "status", status }, { "message", msg } }.ToString(Newtonsoft.Json.Formatting.None));
            Response.Flush();
            Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
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
        private string SendErrorMsgToTeams(string input, string text)
        {
            try
            {
                SendPureMsgToTeams(input, text);
            }
            catch (Exception ex)
            {
                return ex.InnerException.ToString();
            }
            return text;
        }
        private string SendResultMsgToTeams(List<string> sourceList, List<string> resultList)
        {
            var receiveData = string.Join("<br/>", sourceList);
            var verifyResult = "";
            if (resultList.Count <= 0)
                verifyResult += "All accounts exist in mow.";
            else
            {
                var verb = resultList.Count == 1 ? "does" : "do";
                verifyResult += $"{string.Join(", ", resultList)} {verb} not exist in mow";
            }
            SendPureMsgToTeams(receiveData, verifyResult);
            
            return "process successfully";
        }
        private string SendPureMsgToTeams(string input, string text)
        {
            var processText = "Receive Data<br/>";
            processText += "==================================<br/>";
            processText += string.IsNullOrWhiteSpace(input) ? "empty" : input;
            processText += "<br/>";
            processText += "<br/>";
            processText += "Verification Result<br/>";
            processText += "==================================<br/>";
            processText += text;
            SendTeamsMsg(processText);
            
            return text;
        }
    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace AutoMapBankCard
{
    public partial class CheckBankCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            var fullText = txtMessage.Text;
            //replace new line 
            fullText = fullText.Replace(Environment.NewLine, "$nl$");
            var m = Regex.Match(fullText, @"hosts\..*\[Office365connector\]");
            var result = m.Value.Trim("host.[Office365connector]$nl$,".ToArray());

            JObject jObj = new JObject();
            var cards = result.Split(new string[] { "$nl$" }, StringSplitOptions.RemoveEmptyEntries);
            if (cards.Length > 0)
            {
                for (int i = 1; i <= cards.Length; i++)
                {
                    var split = Regex.Matches(cards[i - 1], @"[^\s]+");
                    if (split.Count == 4)
                    {
                        var jItem = new JObject
                        {
                            { "AccountNo", split[1].ToString() },
                            { "AccountName", split[2].ToString() },
                            { "IssueBankAddress", split[3].ToString().TrimEnd(',') },
                        };
                        jObj.Add($"Card{i.ToString()}", jItem);
                    }
                }
            }
            //send request
            var request = new HttpRequestUtility
            {
                FormMethod = HttpMethod.Post,
                ContentType = "application/json",
                CharSet = "utf8",
                Url = $"{Request.Url.Scheme}://{Request.Url.Authority}{Request.Url.Segments.Aggregate((sum, next) => sum + next).Replace(Request.Url.Segments[Request.Url.Segments.Count() - 1], "Notify")}",
                FormContent = new StringContent(jObj.ToString(Newtonsoft.Json.Formatting.None))
            };
            using (var response = request.Submit()) { }
            lbMsg.Text = "Execution Result : submit successfully";
        }
    }
}
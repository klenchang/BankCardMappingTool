using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoMapBankCard
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BankCardHelper.CardNumber == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "UploadFile", "<script>alert('Please Upload Data');</script>");
                rblOption.SelectedValue = "2";
                ifMain.Src = "Upload";
            }
        }

        protected void rblOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = ((RadioButtonList)sender).SelectedValue;
            if (value.Equals("1"))
                ifMain.Src = "CheckBankCard";
            else if (value.Equals("2"))
                ifMain.Src = "Upload";
            else if (value.Equals("3"))
                ifMain.Src = "Search";
        }
    }
}
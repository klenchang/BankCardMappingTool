using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoMapBankCard
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void rblOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = ((RadioButtonList)sender).SelectedValue;
            if (value.Equals("1"))
                ifMain.Src = "CheckBankCard";
            else if(value.Equals("2"))
                ifMain.Src = "Upload";
        }
    }
}
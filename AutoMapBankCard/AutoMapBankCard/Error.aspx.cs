using System;

namespace AutoMapBankCard
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var exception = Server.GetLastError();
                if (exception != null)
                    lbErrorMsg.Text = exception.InnerException.Message;
            }
        }
    }
}
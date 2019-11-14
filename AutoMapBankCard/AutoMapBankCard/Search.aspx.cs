using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoMapBankCard
{
    public partial class Search : System.Web.UI.Page
    {
        DBHelper _dBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void BindGV()
        {
            gvShow.DataSource = _dBHelper.GetBankCardList();
            gvShow.DataBind();
        }
        protected void gvShow_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvShow.PageIndex = e.NewPageIndex;
            BindGV();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGV();
        }
    }
}
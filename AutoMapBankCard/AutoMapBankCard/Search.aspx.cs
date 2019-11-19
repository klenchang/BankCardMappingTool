using System;
using System.Web.UI.WebControls;

namespace AutoMapBankCard
{
    public partial class Search : System.Web.UI.Page
    {
        DBHelper _dBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbCount.Text = BankCardHelper.CardNumber.ToString();
                BindDDLPageIndex();
            }
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
        private void BindDDLPageIndex()
        {
            double total = BankCardHelper.CardNumber;
            double pageSize = double.Parse(ddlPageSize.SelectedValue);
            var pageIndex = Convert.ToInt16(Math.Ceiling(total / pageSize));
            for (int i = 1; i <= pageIndex; i++)
                ddlPageIndex.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPageIndex.Items.Clear();
            BindDDLPageIndex();
            ddlPageIndex.SelectedIndex = 0;
        }
    }
}
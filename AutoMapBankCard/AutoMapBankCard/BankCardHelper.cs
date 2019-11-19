using System.Web;

namespace AutoMapBankCard
{
    public class BankCardHelper
    {
        public static int CardNumber
        {
            get
            {
                if (HttpContext.Current.Session["_bank_card_no"] == null)
                    HttpContext.Current.Session["_bank_card_no"] = new DBHelper().GetBankCardCount();
                return (int)HttpContext.Current.Session["_bank_card_no"];
            }
            set
            {
                HttpContext.Current.Session["_bank_card_no"] = value;
            }
        }
    }
}
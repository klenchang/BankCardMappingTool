using AutoMapBankCard.Model;
using System.Collections.Generic;
using System.Data;
using AutoMapBankCard.Utility;
using System.Linq;

namespace AutoMapBankCard.Helper
{
    public static class BankCardHelper
    {
        private static List<BankCard> _bankCardList = null;
        public static List<BankCard> BankCardList
        {
            get
            {
                if (_bankCardList == null)
                {
                    var dt = new DBHelper().GetBankCardList(0, 0, true);
                    _bankCardList = GeneralUtility.ConvertDataTableToList<BankCard>(dt);
                }
                return _bankCardList;
            }
            set
            {
                _bankCardList = value;
            }
        }
        public static bool IsBankCardExsit(string accountNo, string accountName, string issueBankAddress)
        => BankCardList.Where(i => i.AccountName.StartsWith(accountName) && i.AccountNumber.EndsWith(accountNo) && i.IssuingBankAddress.StartsWith(issueBankAddress)).Count() > 0;
            
    }
}
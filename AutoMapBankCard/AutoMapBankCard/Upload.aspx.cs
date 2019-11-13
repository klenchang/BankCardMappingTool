using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Transactions;

namespace AutoMapBankCard
{
    public partial class Upload : System.Web.UI.Page
    {
        DBHelper _dBHelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var extension = Path.GetExtension(fuData.FileName);
            //verify file
            var verifyResult = VerifyFile(extension);
            if (verifyResult)
            {
                //get table data
                var dt = new DataTable();
                using (Stream stream = fuData.PostedFile.InputStream)
                    dt = ImportExcel(stream, extension, "");
                if (dt.Rows.Count == 0)
                    lbMsg.Text = "Data is empty";
                else
                {
                    using (TransactionScope txnScope = new TransactionScope())
                    {
                        _dBHelper.DeleteAllBankCardList();
                        _dBHelper.InsertFullBankCardList(dt);
                        txnScope.Complete();
                    }
                    lbMsg.Text = "Upload successfully";
                }
            }
        }
        private static DataTable ImportExcel(Stream stream, string extension, string sheetName)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            try
            {
                //get workbook
                workbook = GetWorkbook(extension, stream);
                //get worksheet
                var sheet = !string.IsNullOrWhiteSpace(sheetName) ? workbook.GetSheet(sheetName) : workbook.GetSheetAt(0);
                if (sheet == null) return null;

                //get header
                IRow headerRow = sheet.GetRow(0);
                foreach (var headerCell in headerRow.Cells)
                {
                    if (string.IsNullOrWhiteSpace(headerCell?.ToString()))
                        break;
                    dt.Columns.Add(headerCell.ToString());
                }

                //get table content
                for (var currentRowNumber = (sheet.FirstRowNum + 1); currentRowNumber <= sheet.LastRowNum; currentRowNumber++)
                {
                    IRow row = sheet.GetRow(currentRowNumber);
                    //check has data in row
                    if (!HasRowData(row, dt.Columns.Count)) break;
                    //add data to data table
                    DataRow dataRow = dt.NewRow();
                    for (var currentColumnNumber = 0; currentColumnNumber < dt.Columns.Count; currentColumnNumber++)
                        dataRow[currentColumnNumber] = row.GetCell(currentColumnNumber)?.ToString();

                    dt.Rows.Add(dataRow);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workbook != null)
                    workbook.Close();
            }
            return dt;
        }
        private static IWorkbook GetWorkbook(string extension, Stream stream)
        {
            switch (extension)
            {
                case ".xlsx":
                    return new XSSFWorkbook(stream);
                default:
                    return new HSSFWorkbook(stream);
            }
        }
        private static bool HasRowData(IRow row, int cellCount)
        {
            var result = false;
            for (int currentCellNumber = 0; currentCellNumber < cellCount; currentCellNumber++)
            {
                if (!string.IsNullOrWhiteSpace(row.GetCell(currentCellNumber)?.ToString()))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        private bool VerifyFile(string extension)
        {
            if (!fuData.HasFile) lbMsg.Text = "Please upload the file";
            else
            {
                if (extension != ".xlsx" && extension != ".xls")
                    lbMsg.Text = "Only allow upload excel file";
                else
                    return true;
            }
            return false;
        }
    }
}
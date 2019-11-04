using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Data;
using System.IO;

namespace AutoMapBankCard
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!fuData.HasFile)
                lbMsg.Text = "Please upload the file";
            else
            {
                var extension = Path.GetExtension(fuData.FileName);
                if (extension != ".xlsx" && extension != ".xls")
                    lbMsg.Text = "Only allow upload excel file";
                else
                {
                    var dt = ImportExcel(fuData.PostedFile.InputStream, extension, "");
                }
            }
        }
        private static DataTable ImportExcel(Stream stream, string extension, string sheetName)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            try
            {
                //xls使用HSSFWorkbook类实现，xlsx使用XSSFWorkbook类实现
                switch (extension)
                {
                    case ".xlsx":
                        workbook = new XSSFWorkbook(stream);
                        break;
                    default:
                        workbook = new HSSFWorkbook(stream);
                        break;
                }
                ISheet sheet = null;
                //获取工作表 默认取第一张
                if (string.IsNullOrWhiteSpace(sheetName))
                    sheet = workbook.GetSheetAt(0);
                else
                    sheet = workbook.GetSheet(sheetName);

                if (sheet == null)
                    return null;
                IEnumerator rows = sheet.GetRowEnumerator();
                #region 获取表头
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (!string.IsNullOrWhiteSpace(cell?.ToString()))
                        dt.Columns.Add(cell.ToString());
                    else
                    {
                        cellCount = j;
                        break;
                    }
                }
                #endregion
                #region 获取内容
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (!string.IsNullOrWhiteSpace(row.GetCell(0)?.ToString()))
                        break;

                    DataRow dataRow = dt.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                        dataRow[j] = row.GetCell(j).ToString();

                    dt.Rows.Add(dataRow);
                }
                #endregion

            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                //if (stream != null)
                //{
                //    stream.Close();
                //    stream.Dispose();
                //}
            }
            return dt;
        }
    }
}
using System;
using System.Data;
using System.IO;
using NPOI.XSSF.UserModel;

namespace RaffleDraw.Common
{
    /// <summary>
    /// Excel 工具。
    /// </summary>
    public class ExcelUtility
    {
        /// <summary>
        /// 讀取 Excel。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="sheetIndex">工作表索引。</param>
        /// <param name="headerRowIndex">表頭索引。</param>
        /// <param name="columnsIndex">行索引。</param>
        /// <param name="columnsCount">行數。</param>
        /// <returns>資料表。</returns>
        public static DataTable Read(string fileName, int sheetIndex, int headerRowIndex, int columnsIndex, int columnsCount)
        {
            var fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
                return new DataTable();

            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var workbook = new XSSFWorkbook(fileStream);
            var sheet = workbook.GetSheetAt(sheetIndex) as XSSFSheet;
            var headerRow = sheet.GetRow(headerRowIndex) as XSSFRow;
            var dataTable = new DataTable();

            for (var i = columnsIndex; i < columnsCount; i++)
                dataTable.Columns.Add(new DataColumn(headerRow.GetCell(i).StringCellValue));

            for (var i = headerRowIndex + 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i) as XSSFRow;
                var hasValue = false;
                var dataRow = dataTable.NewRow();

                for (int j = columnsIndex; j < columnsCount; j++)
                {
                    var value = Convert.ToString(row.GetCell(j));
                    dataRow[j] = value;
                    if (!string.IsNullOrWhiteSpace(value))
                        hasValue = true;
                }

                if (hasValue)
                    dataTable.Rows.Add(dataRow);
            }

            fileStream.Close();
            workbook = null;
            sheet = null;

            return dataTable;
        }
    }
}
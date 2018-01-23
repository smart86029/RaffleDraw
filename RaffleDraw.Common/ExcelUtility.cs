using System;
using System.Data;
using System.IO;
using System.Linq;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;

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
                var dataRow = dataTable.NewRow();

                for (int j = columnsIndex; j < columnsCount; j++)
                {
                    var value = Convert.ToString(row.GetCell(j));
                    dataRow[j] = value;
                }

                if (dataRow.ItemArray.Any(x => !string.IsNullOrWhiteSpace(x as string)))
                    dataTable.Rows.Add(dataRow);
            }

            fileStream.Close();
            workbook = null;
            sheet = null;

            return dataTable;
        }

        /// <summary>
        /// 寫入 Excel。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="dataTables">資料表。</param>
        public static void Write(string fileName, List<DataTable> dataTables)
        {
            Write(fileName, dataTables, 0);
        }

        /// <summary>
        /// 寫入 Excel。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="dataTables">資料表。</param>
        /// <param name="headerRowIndex">表頭索引。</param>
        public static void Write(string fileName, List<DataTable> dataTables, int headerRowIndex)
        {
            var workbook = new XSSFWorkbook();

            foreach (var dataTable in dataTables)
            {
                var sheet = string.IsNullOrWhiteSpace(dataTable.TableName) ? workbook.CreateSheet() : workbook.CreateSheet(dataTable.TableName);
                var headerRow = sheet.CreateRow(headerRowIndex);

                foreach (DataColumn column in dataTable.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

                var rowIndex = headerRowIndex + 1;
                foreach (DataRow row in dataTable.Rows)
                {
                    var dataRow = sheet.CreateRow(rowIndex) as XSSFRow;

                    foreach (DataColumn column in dataTable.Columns)
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());

                    rowIndex++;
                }
            }

            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }
        }
    }
}
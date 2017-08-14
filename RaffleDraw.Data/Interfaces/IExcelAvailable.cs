namespace RaffleDraw.Data
{
    /// <summary>
    /// 定義 Excel 讀取的方法。
    /// </summary>
    public interface IExcelAvailable
    {
        /// <summary>
        /// 從指定的 URL 載入 Excel 文件。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        void LoadExcel(string fileName);

        /// <summary>
        /// 將 Excel 文件儲存至指定的檔案。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        void SaveExcel(string fileName);
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RaffleDraw.Common;
using RaffleDraw.Models;

namespace RaffleDraw.Data
{
    /// <summary>
    /// 獎項倉儲。
    /// </summary>
    public class PrizeRepository : IPrizeRepository
    {
        private static PrizeRepository prizeRepository;
        private ObservableCollection<Prize> prizes;

        private PrizeRepository()
        {
            prizes = new ObservableCollection<Prize>();
        }

        public static PrizeRepository Instance
        {
            get
            {
                if (prizeRepository == null)
                    prizeRepository = new PrizeRepository();

                return prizeRepository;
            }
        }

        public ObservableCollection<Prize>  Prizes
        {
            get => prizes;
        }

        /// <summary>
        /// 傳回倉儲中符合指定之條件的實體。
        /// </summary>
        /// <param name="predicate">用來測試每個實體是否符合條件的函式。</param>
        /// <returns><see cref="IEnumerable{Prize}"/>，其中包含倉儲中通過函式指定之測試的實體。</returns>
        public IEnumerable<Prize> Many(Expression<Func<Prize, bool>> predicate)
        {
            var query = prizes.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            return query.ToList();
        }

        /// <summary>
        /// 從指定的 URL 載入 Excel 文件。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        public void LoadExcel(string fileName)
        {
            var dataTable = ExcelUtility.Read(fileName, 0, 1, 0, 4);

            foreach (DataRow row in dataTable.Rows)
            {
                prizes.Add(new Prize
                {
                    SerialNumber = Convert.ToString(row[0]),
                    Quentity = Convert.ToInt32(row[1]),
                    Content = Convert.ToString(row[2]),
                    Provider = Convert.ToString(row[3])
                });
            }
        }

        /// <summary>
        /// 將 Excel 文件儲存至指定的檔案。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        public void SaveExcel(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}

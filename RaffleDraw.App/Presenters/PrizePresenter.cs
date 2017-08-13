using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaffleDraw.Data;
using RaffleDraw.App.Views;
using RaffleDraw.Common;
using RaffleDraw.Models;
using System.Data;

namespace RaffleDraw.App.Presenters
{
    /// <summary>
    /// 獎項主持人。
    /// </summary>
    public class PrizePresenter
    {
        private IPrizeView prizeView;
        private IPrizeRepository prizeRepository;

        /// <summary>
        /// 初始化 <see cref="PrizePresenter"/> 類別的新執行個體。
        /// </summary>
        /// <param name="prizeView">獎項視圖。</param>
        /// <param name="prizeRepository">獎項倉儲。</param>
        public PrizePresenter(IPrizeView prizeView, IPrizeRepository prizeRepository)
        {
            prizeView.PrizePresenter = this;
            this.prizeView = prizeView;
            this.prizeRepository = prizeRepository;
        }

        public void UpdatePrizeView()
        {
        }

        public void LoadExcel(string fileName)
        {
            var prizes = new List<Prize>();
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

            prizeView.Prizes = prizes;
        }
    }
}

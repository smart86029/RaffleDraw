using System.Linq;
using RaffleDraw.App.Views;
using RaffleDraw.Data;

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

        /// <summary>
        /// 載入 Excel。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        public void LoadExcel(string fileName)
        {
            prizeRepository.LoadExcel(fileName);
            UpdatePrizeView();
        }

        /// <summary>
        /// 更新視圖。
        /// </summary>
        private void UpdatePrizeView()
        {
            var prizes = prizeRepository.Many(null).ToList();

            prizeView.Prizes = prizes;
        }
    }
}
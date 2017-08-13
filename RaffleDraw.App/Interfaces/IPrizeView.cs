using System.Collections.Generic;
using RaffleDraw.App.Presenters;
using RaffleDraw.Models;

namespace RaffleDraw.App.Views
{
    /// <summary>
    /// 獎項視圖介面。
    /// </summary>
    public interface IPrizeView
    {
        /// <summary>
        /// 獎項。
        /// </summary>
        ICollection<Prize> Prizes { get; set; }

        /// <summary>
        /// 獎項主持人。
        /// </summary>
        PrizePresenter PrizePresenter { get; set; }
    }
}
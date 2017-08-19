using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using RaffleDraw.Data;
using RaffleDraw.Models;

namespace RaffleDraw.Wpf.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        private PrizeRepository prizeRepository = PrizeRepository.Instance;
        private Prize prize;

        public ResultViewModel()
        {

        }

        public Prize Prize
        {
            get => prize;
            set => Set(ref prize, value);
        }

        /// <summary>
        /// 回傳獎品清單
        /// </summary>
        public ObservableCollection<Prize> Prizes
        {
            get =>prizeRepository.Prizes;
        }
    }
}

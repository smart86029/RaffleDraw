using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RaffleDraw.Data;
using RaffleDraw.Models;

namespace RaffleDraw.Wpf.ViewModels
{
    public class CreatePrizeViewModel : ViewModelBase
    {
        private PrizeRepository prizeRepository = PrizeRepository.Instance;

        public CreatePrizeViewModel()
        {
            CreatePrizeCommand = new RelayCommand(() => CreatePrize());
            HideCreatePrizeDialogCommand = new RelayCommand(() => HideCreatePrizeDialog());
        }

        /// <summary>
        /// 取得或設定新增獎項命令。
        /// </summary>
        public ICommand CreatePrizeCommand { get; private set; }

        /// <summary>
        /// 隱藏新增獎項對話方塊。
        /// </summary>
        public ICommand HideCreatePrizeDialogCommand { get; private set; }

        /// <summary>
        /// 取得或設定獎項。
        /// </summary>
        public Prize Prize { get; set; } = new Prize();

        public string Message { get; set; }

        private void CreatePrize()
        {
            prizeRepository.Prizes.Add(Prize);
            Prize = new Prize();
            HideCreatePrizeDialog();
        }

        private void HideCreatePrizeDialog()
        {
            MessengerInstance.Send(new NotificationMessage("HideCreatePrizeDialog"));
        }
    }
}

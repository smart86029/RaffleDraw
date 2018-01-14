using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RaffleDraw.Data;
using RaffleDraw.Models;

namespace RaffleDraw.Wpf.ViewModels
{
    /// <summary>
    /// 新增獎項檢視模型。
    /// </summary>
    public class CreatePrizeViewModel : ViewModelBase
    {
        private PrizeRepository prizeRepository = PrizeRepository.Instance;
        private Prize prize = new Prize();

        /// <summary>
        /// 初始化新增獎項檢視模型的執行個體。
        /// </summary>
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
        /// 取得或設定隱藏新增獎項對話方塊命令。
        /// </summary>
        public ICommand HideCreatePrizeDialogCommand { get; private set; }

        /// <summary>
        /// 取得或設定獎項。
        /// </summary>
        public Prize Prize
        {
            get => prize;
            set => Set(ref prize, value);
        }

        public string Message { get; set; }

        /// <summary>
        /// 新增獎項。
        /// </summary>
        private void CreatePrize()
        {
            prizeRepository.Prizes.Add(Prize);
            Prize = new Prize();
            HideCreatePrizeDialog();
        }

        /// <summary>
        /// 隱藏新增獎項對話方塊。
        /// </summary>
        private void HideCreatePrizeDialog()
        {
            MessengerInstance.Send(new NotificationMessage("HideCreatePrizeDialog"));
        }
    }
}
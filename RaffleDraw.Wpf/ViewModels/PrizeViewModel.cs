using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RaffleDraw.Data;
using RaffleDraw.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RaffleDraw.Wpf.ViewModels
{
    /// <summary>
    /// 獎項檢視模型。
    /// </summary>
    public class PrizeViewModel : ViewModelBase
    {
        private PrizeRepository prizeRepository = PrizeRepository.Instance;

        /// <summary>
        /// 初始化獎項檢視模型的執行個體。
        /// </summary>
        public PrizeViewModel()
        {
            ShowCreatePrizeDialogCommand = new RelayCommand(() => ShowCreatePrizeDialog());
            ImportPrizeCommand = new RelayCommand(() => ImportPrize());
            DeletePrizeCommand = new RelayCommand<Prize>(p => DeletePrize(p));
        }

        /// <summary>
        /// 取得或設定顯示新增員工方塊命令。
        /// </summary>
        public ICommand ShowCreatePrizeDialogCommand { get; private set; }

        /// <summary>
        /// 取得或設定匯入獎項命令。
        /// </summary>
        public ICommand ImportPrizeCommand { get; private set; }

        /// <summary>
        /// 取得或設定刪除獎項命令。
        /// </summary>
        public ICommand DeletePrizeCommand { get; private set; }

        /// <summary>
        /// 取得獎項清單。
        /// </summary>
        public ObservableCollection<Prize> Prizes => prizeRepository.Prizes;

        /// <summary>
        /// 顯示新增員工方塊。
        /// </summary>
        private void ShowCreatePrizeDialog()
        {
            MessengerInstance.Send(new NotificationMessage("ShowCreatePrizeDialog"));
        }

        /// <summary>
        /// 匯入獎項。
        /// </summary>
        private void ImportPrize()
        {
            var fileName = string.Empty;

            MessengerInstance.Send(new NotificationMessageAction<string>("ImportPrize", x => fileName = x));
            if (!string.IsNullOrWhiteSpace(fileName))
                prizeRepository.LoadExcel(fileName);
        }

        /// <summary>
        /// 刪除獎項。
        /// </summary>
        /// <param name="prize">獎項。</param>
        private void DeletePrize(Prize prize)
        {
            prizeRepository.Prizes.Remove(prize);
        }
    }
}
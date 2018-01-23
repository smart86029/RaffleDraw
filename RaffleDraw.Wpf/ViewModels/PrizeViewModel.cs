using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RaffleDraw.Data;
using RaffleDraw.Models;

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
        }

        /// <summary>
        /// 取得顯示新增員工方塊命令。
        /// </summary>
        public ICommand ShowCreatePrizeDialogCommand => new RelayCommand(() => ShowCreatePrizeDialog());

        /// <summary>
        /// 取得匯入獎項命令。
        /// </summary>
        public ICommand ImportPrizeCommand => new RelayCommand(() => ImportPrize());

        /// <summary>
        /// 取得匯出中獎者命令。
        /// </summary>
        public ICommand ExportPrizeCommand => new RelayCommand(() => ExportPrize());

        /// <summary>
        /// 取得刪除獎項命令。
        /// </summary>
        public ICommand DeletePrizeCommand => new RelayCommand<Prize>(p => DeletePrize(p));

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
        /// 匯出獎項。
        /// </summary>
        private void ExportPrize()
        {
            var fileName = string.Empty;

            MessengerInstance.Send(new NotificationMessageAction<string>("ExportPrize", x => fileName = x));
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            prizeRepository.SaveExcel(fileName);
            MessengerInstance.Send(new NotificationMessage("ExportPrizeCompleted"));
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
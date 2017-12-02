using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using RaffleDraw.Common;
using RaffleDraw.Data;
using RaffleDraw.Models;

namespace RaffleDraw.Wpf.ViewModels
{
    public class PrizeViewModel : ViewModelBase
    {
        private PrizeRepository prizeRepository = PrizeRepository.Instance;
        private string importPrizeMessage;

        public PrizeViewModel()
        {
            ShowCreatePrizeDialogCommand = new RelayCommand(() => ShowCreatePrizeDialog());
            ImportPrizeCommand = new RelayCommand(() => ImportPrize());
        }

        public string ImportPrizeMessage
        {
            get => importPrizeMessage;
            set => Set(ref importPrizeMessage, value);
        }

        /// <summary>
        /// 取得或設定顯示新增員工方塊命令。
        /// </summary>
        public ICommand ShowCreatePrizeDialogCommand { get; private set; }

        /// <summary>
        /// 回傳瀏覽獎品命令
        /// </summary>
        public ICommand ImportPrizeCommand { get; private set; }

        /// <summary>
        /// 回傳獎品清單
        /// </summary>
        public ObservableCollection<Prize> Prizes => prizeRepository.Prizes;

        private void ShowCreatePrizeDialog()
        {
            MessengerInstance.Send(new NotificationMessage("ShowCreatePrizeDialog"));
        }

        /// <summary>
        /// 載入獎品清單
        /// </summary>
        private void ImportPrize()
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = Constant.ExcelDefaultExtension,
                Filter = Constant.ExcelFileFilter
            };
            var result = openFileDialog.ShowDialog();
            if (result.GetValueOrDefault())
            {
                prizeRepository.LoadExcel(openFileDialog.FileName);

                var resultViewModel = ServiceLocator.Current.GetInstance<ResultViewModel>();
                foreach (var prize in prizeRepository.Prizes)
                    resultViewModel.Prizes.Add(prize);

                ImportPrizeMessage = "完成";
            }
        }
    }
}
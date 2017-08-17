using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using RaffleDraw.Data;

namespace RaffleDraw.Wpf.ViewModels
{
    public class PrizeViewModel : ViewModelBase
    {
        private PrizeRepository prizeRepository = new PrizeRepository();
        private string importPrizeMessage;
        public PrizeViewModel()
        {
            ImportPrizeCommand = new RelayCommand(() => ImportPrize());
        }
        
        /// <summary>
        /// 回傳瀏覽獎品命令
        /// </summary>
        public ICommand ImportPrizeCommand { get; private set; }

        public string ImportPrizeMessage
        {
            get
            {
                return importPrizeMessage;
            }
            set
            {
                if (importPrizeMessage != value)
                {
                    importPrizeMessage = value.Trim();
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 載入獎品清單
        /// </summary>
        private void ImportPrize()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "Excel 檔案 (*xls; *.xlsx; *.xlsm)|*xls; *.xlsx; *.xlsm|所有檔案 (*.*)|*.*";

            var result = openFileDialog.ShowDialog();
            if (result.GetValueOrDefault())
            {
                prizeRepository.LoadExcel(openFileDialog.FileName);
                ImportPrizeMessage = "完成";
                //importPrizeLink.Text = "完成";
                //importPrizeLink.Enabled = false;
            }
        }
    }
}

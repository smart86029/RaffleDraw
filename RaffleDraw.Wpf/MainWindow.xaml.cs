using System.ComponentModel;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using RaffleDraw.Wpf.ViewModels;

namespace RaffleDraw.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool shouldClose;

        /// <summary>
        /// 初始化 <see cref="MainWindow"/> 的執行個體。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindowClosingAsync;
        }

        /// <summary>
        /// 關閉視窗。
        /// </summary>
        /// <param name="sender">引發事件的物件。</param>
        /// <param name="e">事件相關資訊。</param>
        private async void MainWindowClosingAsync(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
                return;
            e.Cancel = !shouldClose;
            if (shouldClose)
                return;
            await CheckExportAsync();

            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "是",
                NegativeButtonText = "否",
                AnimateShow = true
            };
            var result = await this.ShowMessageAsync("詢問", "是否要離開?", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (shouldClose = result == MessageDialogResult.Affirmative)
                Close();
        }

        /// <summary>
        /// 檢查是否需要匯出。
        /// </summary>
        /// <returns>非同步作業。</returns>
        private async Task CheckExportAsync()
        {
            var resultViewModel = ServiceLocator.Current.GetInstance<RecordViewModel>();
            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "是",
                NegativeButtonText = "否",
                AnimateShow = true
            };

            if (!resultViewModel.ShouldExport)
                return;

            var result = await this.ShowMessageAsync("詢問", "變更還沒有儲存，是否要匯出 Excel?", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (result == MessageDialogResult.Affirmative)
                resultViewModel.ExportWinnerCommand.Execute(null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;

namespace RaffleDraw.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool shouldClose;

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindowClosingAsync;
        }

        private async void MainWindowClosingAsync(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
                return;

            e.Cancel = !shouldClose;
            if (shouldClose)
                return;

            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "是",
                NegativeButtonText = "否",
                AnimateShow = true
            };
            var result = await this.ShowMessageAsync("詢問", "是否要離開?", MessageDialogStyle.AffirmativeAndNegative, settings);

            shouldClose = result == MessageDialogResult.Affirmative;
            if (shouldClose)
                Close();

            //if (!isSaved)
            //    if (MessageBox.Show("變更還沒有儲存，是否要匯出Excel?", "詢問", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //        SaveWinningExcel(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\中獎名單" + string.Format("{0:MM}", DateTime.Now) + DateTime.Now.Day + ".xlsx");
            //if (MessageBox.Show("是否要離開?", "詢問", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //    e.Cancel = true;
        }
    }
}

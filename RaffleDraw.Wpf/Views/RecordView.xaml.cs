using System;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using RaffleDraw.Common;

namespace RaffleDraw.Wpf.Views
{
    /// <summary>
    /// ResultView.xaml 的互動邏輯
    /// </summary>
    public partial class RecordView : UserControl
    {
        private IDialogCoordinator dialogCoordinator = DialogCoordinator.Instance;

        public RecordView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceivedAsync);
            Messenger.Default.Register<NotificationMessageAction<string>>(this, NotificationMessageActionReceived);
        }

        private async void NotificationMessageReceivedAsync(NotificationMessage message)
        {
            switch (message.Notification)
            {
                case "ExportWinnerCompleted":
                    await dialogCoordinator.ShowMessageAsync(DataContext, "成功", "匯出完成");
                    break;
            }
        }

        private void NotificationMessageActionReceived<T>(NotificationMessageAction<T> message)
        {
            switch (message.Notification)
            {
                case "ExportWinner":
                    var fileName = $"中獎名單{DateTime.Now.ToString("MMdd")}.xlsx";
                    var saveFileDialog = new SaveFileDialog
                    {
                        FileName = fileName,
                        DefaultExt = Constant.ExcelDefaultExtension,
                        Filter = Constant.ExcelFileFilter
                    };
                    var result = saveFileDialog.ShowDialog();
                    if (result.GetValueOrDefault())
                        message.Execute(saveFileDialog.FileName);
                    break;
            }
        }
    }
}
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using RaffleDraw.Common;

namespace RaffleDraw.Wpf.Views
{
    /// <summary>
    /// PrizeView.xaml 的互動邏輯
    /// </summary>
    public partial class PrizeView : UserControl
    {
        private CreatePrizeView dialog = new CreatePrizeView();
        private IDialogCoordinator dialogCoordinator = DialogCoordinator.Instance;

        public PrizeView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceivedAsync);
            Messenger.Default.Register<NotificationMessageAction<string>>(this, NotificationMessageActionReceived);
        }

        private async void NotificationMessageReceivedAsync(NotificationMessage message)
        {
            if (message.Notification == "ShowCreatePrizeDialog")
                await dialogCoordinator.ShowMetroDialogAsync(DataContext, dialog);
        }

        private void NotificationMessageActionReceived<T>(NotificationMessageAction<T> message)
        {
            switch (message.Notification)
            {
                case "ImportPrize":
                    var openFileDialog = new OpenFileDialog
                    {
                        DefaultExt = Constant.ExcelDefaultExtension,
                        Filter = Constant.ExcelFileFilter
                    };
                    var result = openFileDialog.ShowDialog();
                    if (result.GetValueOrDefault())
                        message.Execute(openFileDialog.FileName);
                    break;
            }
        }
    }
}
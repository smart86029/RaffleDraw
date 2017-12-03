using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using RaffleDraw.Common;

namespace RaffleDraw.Wpf.Views
{
    /// <summary>
    /// EmployeeView.xaml 的互動邏輯
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        private CreateEmployeeView dialog = new CreateEmployeeView();
        private IDialogCoordinator dialogCoordinator = DialogCoordinator.Instance;

        public EmployeeView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceivedAsync);
            Messenger.Default.Register<NotificationMessageAction<string>>(this, NotificationMessageActionReceived);
        }

        private async void NotificationMessageReceivedAsync(NotificationMessage message)
        {
            switch (message.Notification)
            {
                case "ShowCreateEmployeeDialog":
                    await dialogCoordinator.ShowMetroDialogAsync(DataContext, dialog);
                    break;
            }
        }

        private void NotificationMessageActionReceived<T>(NotificationMessageAction<T> message)
        {
            switch (message.Notification)
            {
                case "ImportEmployee":
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
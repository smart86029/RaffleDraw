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
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;

namespace RaffleDraw.Wpf.Views
{
    /// <summary>
    /// CreateEmployeeView.xaml 的互動邏輯
    /// </summary>
    public partial class CreateEmployeeView : CustomDialog
    {
        private IDialogCoordinator dialogCoordinator = DialogCoordinator.Instance;

        public CreateEmployeeView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceivedAsync);
        }

        private async void NotificationMessageReceivedAsync(NotificationMessage message)
        {
            if (message.Notification == "HideCreateEmployeeDialog")
                await dialogCoordinator.HideMetroDialogAsync(DataContext, this);
        }
    }
}

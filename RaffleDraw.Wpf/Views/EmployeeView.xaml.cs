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
        }

        private async void NotificationMessageReceivedAsync(NotificationMessage message)
        {
            if (message.Notification == "ShowCreateEmployeeDialog")
                await dialogCoordinator.ShowMetroDialogAsync(DataContext, dialog);
        }
    }
}

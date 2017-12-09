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
using Microsoft.Win32;
using RaffleDraw.Common;

namespace RaffleDraw.Wpf.Views
{
    /// <summary>
    /// ResultView.xaml 的互動邏輯
    /// </summary>
    public partial class RecordView : UserControl
    {
        public RecordView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessageAction<string>>(this, NotificationMessageActionReceived);
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

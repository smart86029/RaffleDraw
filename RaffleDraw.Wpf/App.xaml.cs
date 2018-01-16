using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro;
using RaffleDraw.Common;

namespace RaffleDraw.Wpf
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    // add custom accent and theme resource dictionaries to the ThemeManager
        //    // you should replace MahAppsMetroThemesSample with your application name
        //    // and correct place where your custom accent lives
        //    ThemeManager.AddAppTheme("CustomTheme", new Uri("pack://application:,,,/RaffleDraw.Wpf;component/CustomTheme.xaml"));

        //    // get the current app style (theme and accent) from the application
        //    var theme = ThemeManager.DetectAppStyle(Current);

        //    // now change app style to the custom accent and current theme
        //    ThemeManager.ChangeAppStyle(Current, theme.Item2, ThemeManager.GetAppTheme("CustomTheme"));

        //    base.OnStartup(e);
        //}

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogUtility.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.Exception);
            e.Handled = true;
        }
    }
}

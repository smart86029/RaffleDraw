/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:RaffleDraw.Wpf"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace RaffleDraw.Wpf.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EmployeeViewModel>();
            SimpleIoc.Default.Register<CreateEmployeeViewModel>();
            SimpleIoc.Default.Register<PrizeViewModel>();
            SimpleIoc.Default.Register<CreatePrizeViewModel>();
            SimpleIoc.Default.Register<RecordViewModel>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public EmployeeViewModel EmployeeViewModel => ServiceLocator.Current.GetInstance<EmployeeViewModel>();
        public CreateEmployeeViewModel CreateEmployeeViewModel => ServiceLocator.Current.GetInstance<CreateEmployeeViewModel>();
        public PrizeViewModel PrizeViewModel => ServiceLocator.Current.GetInstance<PrizeViewModel>();
        public CreatePrizeViewModel CreatePrizeViewModel => ServiceLocator.Current.GetInstance<CreatePrizeViewModel>();
        public RecordViewModel RecordViewModel => ServiceLocator.Current.GetInstance<RecordViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
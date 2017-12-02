using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RaffleDraw.Data;

namespace RaffleDraw.Wpf.ViewModels
{
    public class CreateEmployeeViewModel : ViewModelBase
    {
        private EmployeeRepository employeeRepository = EmployeeRepository.Instance;

        public CreateEmployeeViewModel()
        {
            CreateEmployeeCommand = new RelayCommand(() => CreateEmployee());
            HideCreateEmployeeDialogCommand = new RelayCommand(() => HideCreateEmployeeDialog());
        }

        /// <summary>
        /// 取得或設定新增員工命令。
        /// </summary>
        public ICommand CreateEmployeeCommand { get; private set; }

        public ICommand HideCreateEmployeeDialogCommand { get; private set; }

        private void CreateEmployee()
        {

        }

        private void HideCreateEmployeeDialog()
        {
            MessengerInstance.Send(new NotificationMessage("HideCreateEmployeeDialog"));
        }
    }
}

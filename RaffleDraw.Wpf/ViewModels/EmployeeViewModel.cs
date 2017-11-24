using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using RaffleDraw.Data;
using RaffleDraw.Models;

namespace RaffleDraw.Wpf.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        private EmployeeRepository employeeRepository = EmployeeRepository.Instance;
        //private ObservableCollection<Employee> employees;
        private string importEmployeeMessage;

        public EmployeeViewModel()
        {
            ImportEmployeeCommand = new RelayCommand(() => ImportEmployee());
        }

        public string ImportEmployeeMessage
        {
            get => importEmployeeMessage;
            set => Set(ref importEmployeeMessage, value);
        }

        /// <summary>
        /// 回傳獎品清單
        /// </summary>
        public ObservableCollection<Employee> Employees
        {
            get => employeeRepository.Employees;
        }

        /// <summary>
        /// 回傳瀏覽獎品命令
        /// </summary>
        public ICommand ImportEmployeeCommand { get; private set; }



        /// <summary>
        /// 載入獎品清單
        /// </summary>
        private void ImportEmployee()
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "Excel 檔案 (*xls; *.xlsx; *.xlsm)|*xls; *.xlsx; *.xlsm|所有檔案 (*.*)|*.*"
            };
            var result = openFileDialog.ShowDialog();
            if (result.GetValueOrDefault())
            {
                employeeRepository.LoadExcel(openFileDialog.FileName);
                //Employees = employeeRepository.Employees;
                ImportEmployeeMessage = "完成";
            }
        }
    }
}

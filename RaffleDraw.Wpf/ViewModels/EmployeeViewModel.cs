﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using RaffleDraw.Common;
using RaffleDraw.Data;
using RaffleDraw.Models;
using RaffleDraw.Wpf.Views;

namespace RaffleDraw.Wpf.ViewModels
{
    /// <summary>
    /// 員工檢視模型。
    /// </summary>
    public class EmployeeViewModel : ViewModelBase
    {
        private EmployeeRepository employeeRepository = EmployeeRepository.Instance;
        private string importEmployeeMessage;
        
        public EmployeeViewModel()
        {

            ShowCreateEmployeeDialogCommand = new RelayCommand(() => ShowCreateEmployeeDialog());
            ImportEmployeeCommand = new RelayCommand(() => ImportEmployee());
        }

        public string ImportEmployeeMessage
        {
            get => importEmployeeMessage;
            set => Set(ref importEmployeeMessage, value);
        }


        /// <summary>
        /// 取得或設定顯示新增員工方塊命令。
        /// </summary>
        public ICommand ShowCreateEmployeeDialogCommand { get; private set; }

        /// <summary>
        /// 取得或設定匯入員工命令。
        /// </summary>
        public ICommand ImportEmployeeCommand { get; private set; }


        /// <summary>
        /// 取得員工清單。
        /// </summary>
        public ObservableCollection<Employee> Employees => employeeRepository.Employees;


        private void ShowCreateEmployeeDialog()
        {
            MessengerInstance.Send(new NotificationMessage("ShowCreateEmployeeDialog"));
        }

        /// <summary>
        /// 載入獎品清單
        /// </summary>
        private void ImportEmployee()
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = Constant.ExcelDefaultExtension,
                Filter = Constant.ExcelFileFilter
            };
            var result = openFileDialog.ShowDialog();
            if (result.GetValueOrDefault())
            {
                employeeRepository.LoadExcel(openFileDialog.FileName);
                //ImportEmployeeMessage = "完成";
            }
        }
    }
}
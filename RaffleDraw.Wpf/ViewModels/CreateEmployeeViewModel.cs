﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RaffleDraw.Data;
using RaffleDraw.Models;

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

        /// <summary>
        /// 隱藏新增員工對話方塊。
        /// </summary>
        public ICommand HideCreateEmployeeDialogCommand { get; private set; }

        /// <summary>
        /// 取得或設定員工。
        /// </summary>
        public Employee Employee { get; set; } = new Employee();

        public string Message { get; set; }

        private void CreateEmployee()
        {
            employeeRepository.Employees.Add(Employee);
            Employee = new Employee();
            HideCreateEmployeeDialog();
            //if (employeeRepository.Employees.Any(e => e.SerialNumber == Employee.SerialNumber))
            //    Message = "重複";
        }

        private void HideCreateEmployeeDialog()
        {
            MessengerInstance.Send(new NotificationMessage("HideCreateEmployeeDialog"));
        }
    }
}

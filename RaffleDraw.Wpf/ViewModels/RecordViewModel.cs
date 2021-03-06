﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RaffleDraw.Common;
using RaffleDraw.Data;
using RaffleDraw.Models;

namespace RaffleDraw.Wpf.ViewModels
{
    /// <summary>
    /// 紀錄檢視模型。
    /// </summary>
    public class RecordViewModel : ViewModelBase
    {
        private EmployeeRepository employeeRepository = EmployeeRepository.Instance;
        private PrizeRepository prizeRepository = PrizeRepository.Instance;
        private Employee employee;
        private Prize prize;
        private ObservableCollection<Prize> prizes = new ObservableCollection<Prize>();
        private ObservableCollection<Employee> winners = new ObservableCollection<Employee>();
        private string searchSerialNumber = string.Empty;
        private string saveWinnerMessage = string.Empty;
        private bool shouldExport;
        private bool hasEmployee;

        /// <summary>
        /// 初始化紀錄檢視模型的執行個體。
        /// </summary>
        public RecordViewModel()
        {
            prizeRepository.Prizes.CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// 取得或設定搜尋員工命令。
        /// </summary>
        public ICommand SearchEmployeeCommand => new RelayCommand(() => SearchEmployee());

        /// <summary>
        /// 取得或設定儲存中獎者命令。
        /// </summary>
        public ICommand SaveWinnerCommand => new RelayCommand(() => SaveWinner());

        /// <summary>
        /// 取得或設定取消中獎者命令。
        /// </summary>
        public ICommand CancelWinnerCommand => new RelayCommand<Employee>(e => CancelWinner(e));

        /// <summary>
        /// 取得或設定匯出中獎者命令。
        /// </summary>
        public ICommand ExportWinnerCommand => new RelayCommand(() => ExportWinner());

        /// <summary>
        /// 取得或設定員工。
        /// </summary>
        public Employee Employee
        {
            get => employee;
            set => Set(ref employee, value);
        }

        /// <summary>
        /// 取得或設定獎項。
        /// </summary>
        public Prize Prize
        {
            get => prize;
            set => Set(ref prize, value);
        }

        /// <summary>
        /// 回傳獎項清單。
        /// </summary>
        public ObservableCollection<Prize> Prizes => prizes;

        /// <summary>
        /// 回傳中獎者清單。
        /// </summary>
        public ObservableCollection<Employee> Winners => winners;

        /// <summary>
        /// 取得或設定搜尋序號。
        /// </summary>
        public string SearchSerialNumber
        {
            get => searchSerialNumber;
            set => Set(ref searchSerialNumber, value);
        }

        /// <summary>
        /// 取得或設定儲存訊息。
        /// </summary>
        public string SaveWinnerMessage
        {
            get => saveWinnerMessage;
            set => Set(ref saveWinnerMessage, value);
        }

        /// <summary>
        /// 取得或設定是否應該匯出。
        /// </summary>
        public bool ShouldExport
        {
            get => shouldExport;
            set => Set(ref shouldExport, value);
        }

        /// <summary>
        /// 取得或設定是否有員工。
        /// </summary>
        public bool HasEmployee
        {
            get => hasEmployee;
            set => Set(ref hasEmployee, value);
        }

        /// <summary>
        /// 搜尋員工。
        /// </summary>
        private void SearchEmployee()
        {
            var employee = employeeRepository.Employees.SingleOrDefault(e => e.SerialNumber == SearchSerialNumber);
            if (employee == null)
                return;

            Employee = employee;
            HasEmployee = true;
            SaveWinnerMessage = string.Empty;
            if (employee.Prize != null)
            {
                SaveWinnerMessage = "重複中獎";
                return;
            }   
        }

        /// <summary>
        /// 儲存中獎者。
        /// </summary>
        private void SaveWinner()
        {
            if (employee == null)
            {
                SaveWinnerMessage = "請選擇員工";
                return;
            }
            if (employee.Prize != null)
            {
                SaveWinnerMessage = "重複中獎";
                return;
            }
            if (prize == null)
            {
                SaveWinnerMessage = "請選擇獎項";
                return;
            }
            if (prize.Quentity <= prize.Winners.Count)
            {
                SaveWinnerMessage = "此獎已滿額";
                return;
            }

            Prize.Winners.Add(Employee);
            Winners.Add(Employee);
            Employee.Prize = Prize;
            ShouldExport = true;
            Employee = null;
            HasEmployee = false;
            SearchSerialNumber = string.Empty;
            SaveWinnerMessage = string.Empty;
            if (prize.Quentity <= prize.Winners.Count)
                Prizes.Remove(prize);
        }

        /// <summary>
        /// 取消中獎者。
        /// </summary>
        private void CancelWinner(Employee employee)
        {
            employee.Prize.Winners.Remove(employee);
            Winners.Remove(employee);
            if (employee.Prize.Quentity > employee.Prize.Winners.Count)
            {
                Prizes.Add(employee.Prize);
                prizes = new ObservableCollection<Prize>(Prizes.OrderBy(p => p.SerialNumber.PadLeft(Constant.SerialNumberToatalWidth)));
                RaisePropertyChanged(nameof(Prizes));
            }

            employee.Prize = null;
            ShouldExport = true;
        }

        /// <summary>
        /// 匯出中獎者。
        /// </summary>
        private void ExportWinner()
        {
            var fileName = string.Empty;

            MessengerInstance.Send(new NotificationMessageAction<string>("ExportWinner", x => fileName = x));
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            SaveWinningExcel(fileName);
            MessengerInstance.Send(new NotificationMessage("ExportWinnerCompleted"));
        }

        /// <summary>
        /// 儲存中獎者 Excel。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        private void SaveWinningExcel(string fileName)
        {
            var dataTables = new List<DataTable>();
            var dataTable = new DataTable("中獎名單");
            dataTable.Columns.Add("序號", typeof(string));
            dataTable.Columns.Add("一級單位", typeof(string));
            dataTable.Columns.Add("二級單位", typeof(string));
            dataTable.Columns.Add("員工代號", typeof(string));
            dataTable.Columns.Add("姓名", typeof(string));
            dataTable.Columns.Add("獎項", typeof(string));
            dataTable.Columns.Add("獎品", typeof(string));
            dataTable.Columns.Add("提供者", typeof(string));
            dataTable.Columns.Add("備註", typeof(string));

            foreach (var prize in prizeRepository.Prizes.Where(p => p.Winners.Count > 0))
                foreach (var winner in prize.Winners)
                    dataTable.Rows.Add(new object[] { winner.SerialNumber, winner.Office, winner.Division,winner.EmployeeId, winner.Name,
                        prize.SerialNumber, prize.Content, prize.Provider, string.Empty });

            dataTables.Add(dataTable);

            dataTable = new DataTable("未中獎名單");
            dataTable.Columns.Add("序號", typeof(string));
            dataTable.Columns.Add("一級單位", typeof(string));
            dataTable.Columns.Add("二級單位", typeof(string));
            dataTable.Columns.Add("員工代號", typeof(string));
            dataTable.Columns.Add("姓名", typeof(string));

            foreach (var employee in employeeRepository.Employees.Where(e => e.Prize == null))
                dataTable.Rows.Add(new object[] { employee.SerialNumber, employee.Office, employee.Division, employee.EmployeeId, employee.Name });

            dataTables.Add(dataTable);
            ExcelUtility.Write(fileName, dataTables);
            ShouldExport = false;
        }

        /// <summary>
        /// 集合變更。
        /// </summary>
        /// <param name="sender">引發事件的物件。</param>
        /// <param name="e">事件相關資訊。</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Prize item in e.NewItems)
                        Prizes.Add(item);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (Prize item in e.OldItems)
                        Prizes.Remove(item);
                    break;
            }
        }
    }
}
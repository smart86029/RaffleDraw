using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using RaffleDraw.Common;
using RaffleDraw.Data;
using RaffleDraw.Models;

namespace RaffleDraw.Wpf.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        private EmployeeRepository employeeRepository = EmployeeRepository.Instance;
        private PrizeRepository prizeRepository = PrizeRepository.Instance;
        private Employee employee;
        private Prize prize;
        private ObservableCollection<Prize> prizes = new ObservableCollection<Prize>();
        private string searchSerialNumber = string.Empty;
        private string saveWinnerMessage = string.Empty;

        public ICommand SearchEmployeeCommand => new RelayCommand(() => SearchEmployee());

        /// <summary>
        /// 回傳儲存命令
        /// </summary>
        public ICommand SaveWinnerCommand => new RelayCommand(() => SaveWinner());

        public ICommand ExportWinnerCommand => new RelayCommand(() => ExportWinner());

        /// <summary>
        /// 回傳/設定員工
        /// </summary>
        public Employee Employee
        {
            get => employee;
            set => Set(ref employee, value);
        }

        public Prize Prize
        {
            get => prize;
            set => Set(ref prize, value);
        }

        /// <summary>
        /// 回傳獎品清單
        /// </summary>
        public ObservableCollection<Prize> Prizes => prizes;

        /// <summary>
        /// 回傳/設定搜尋序號
        /// </summary>
        public string SearchSerialNumber
        {
            get => searchSerialNumber;
            set => Set(ref searchSerialNumber, value);
        }

        /// <summary>
        /// 回傳/設定儲存訊息
        /// </summary>
        public string SaveWinnerMessage
        {
            get => saveWinnerMessage;
            set => Set(ref saveWinnerMessage, value);
        }

        /// <summary>
        /// 搜尋員工
        /// </summary>
        private void SearchEmployee()
        {
            var employee = employeeRepository.Employees.SingleOrDefault(e => e.SerialNumber == SearchSerialNumber);
            if (employee != null)
            {
                Employee = employee;
                //SaveWinnerMessage = "";
            }
        }

        /// <summary>
        /// 儲存中獎
        /// </summary>
        private void SaveWinner()
        {
            if (employee == null)
                SaveWinnerMessage = "請選擇員工";
            if (employee.Prize == null)
                SaveWinnerMessage = "重複中獎";

            if (prize == null)
                SaveWinnerMessage = "請選擇獎項";
            if (prize.Quentity <= prize.Winners.Count)
                SaveWinnerMessage = "此獎已滿額";

            Prize.Winners.Add(Employee);
            Employee.Prize = Prize;
            Employee = null;
            SearchSerialNumber = string.Empty;
            SaveWinnerMessage = string.Empty;
            if (prize.Quentity <= prize.Winners.Count)
                Prizes.Remove(prize);
        }

        private void ExportWinner()
        {
            var fileName = $"中獎名單{DateTime.Now.ToString("MMdd")}.xlsx";
            var saveFileDialog = new SaveFileDialog
            {
                FileName = fileName,
                DefaultExt = ".xlsx",
                Filter = "Excel 檔案 (*xls; *.xlsx; *.xlsm)|*xls; *.xlsx; *.xlsm|所有檔案 (*.*)|*.*"
            };
            var result = saveFileDialog.ShowDialog();
            if (result.GetValueOrDefault())
            {
                SaveWinningExcel(saveFileDialog.FileName);
            }
        }

        private void SaveWinningExcel(string fileName)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("序號", typeof(string));
            dataTable.Columns.Add("員工代號", typeof(string));
            dataTable.Columns.Add("姓名", typeof(string));
            dataTable.Columns.Add("一級單位", typeof(string));
            dataTable.Columns.Add("二級單位", typeof(string));
            dataTable.Columns.Add("獎項", typeof(string));
            dataTable.Columns.Add("獎品", typeof(string));
            dataTable.Columns.Add("提供者", typeof(string));
            dataTable.Columns.Add("備註", typeof(string));

            foreach (var prize in prizeRepository.Prizes.Where(p => p.Winners.Count > 0))
                foreach (var winner in prize.Winners)
                    dataTable.Rows.Add(new object[] { winner.SerialNumber, winner.EmployeeId, winner.Name, winner.Office, winner.Division,
                        prize.SerialNumber, prize.Content, prize.Provider, string.Empty });

            ExcelUtility.Write(fileName, dataTable);
        }
    }
}

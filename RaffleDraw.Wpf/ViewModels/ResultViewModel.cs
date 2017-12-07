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
using System.Collections.Generic;

namespace RaffleDraw.Wpf.ViewModels
{
    /// <summary>
    /// 結果檢視模型。
    /// </summary>
    public class ResultViewModel : ViewModelBase
    {
        private EmployeeRepository employeeRepository = EmployeeRepository.Instance;
        private PrizeRepository prizeRepository = PrizeRepository.Instance;
        private Employee employee;
        private Prize prize;
        private ObservableCollection<Prize> prizes = new ObservableCollection<Prize>();
        private string searchSerialNumber = string.Empty;
        private string saveWinnerMessage = string.Empty;
        private bool shouldExport;

        /// <summary>
        /// 初始化結果檢視模型的執行個體。
        /// </summary>
        public ResultViewModel()
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
        /// 搜尋員工。
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
        /// 儲存中獎者。
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
            ShouldExport = true;
            Employee = null;
            SearchSerialNumber = string.Empty;
            SaveWinnerMessage = string.Empty;
            if (prize.Quentity <= prize.Winners.Count)
                Prizes.Remove(prize);
        }

        /// <summary>
        /// 匯出中獎者。
        /// </summary>
        private void ExportWinner()
        {
            var fileName = string.Empty;

            MessengerInstance.Send(new NotificationMessageAction<string>("ExportWinner", x => fileName = x));
            if (!string.IsNullOrWhiteSpace(fileName))
                SaveWinningExcel(fileName);
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
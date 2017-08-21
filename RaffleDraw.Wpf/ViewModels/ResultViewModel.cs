using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        private string searchSerialNumber = string.Empty;

        public ResultViewModel()
        {
            SearchEmployeeCommand = new RelayCommand(() => SearchEmployee());
        }

        public ICommand SearchEmployeeCommand { get; private set; }

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
        public ObservableCollection<Prize> Prizes
        {
            get =>prizeRepository.Prizes;
        }

        /// <summary>
        /// 回傳/設定搜尋序號
        /// </summary>
        public string SearchSerialNumber
        {
            get => searchSerialNumber;
            set => Set(ref searchSerialNumber, value);
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
    }
}

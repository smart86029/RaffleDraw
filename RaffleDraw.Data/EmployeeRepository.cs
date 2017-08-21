using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RaffleDraw.Common;
using RaffleDraw.Models;

namespace RaffleDraw.Data
{
    /// <summary>
    /// 員工倉儲。
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private static EmployeeRepository employeeRepository;
        private ObservableCollection<Employee> employees;

        private EmployeeRepository()
        {
            employees = new ObservableCollection<Employee>();
        }

        public static EmployeeRepository Instance
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository();

                return employeeRepository;
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get => employees;
        }

        /// <summary>
        /// 傳回倉儲中符合指定之條件的實體。
        /// </summary>
        /// <param name="predicate">用來測試每個實體是否符合條件的函式。</param>
        /// <returns><see cref="IEnumerable{Employee}"/>，其中包含倉儲中通過函式指定之測試的實體。</returns>
        public IEnumerable<Employee> Many(Expression<Func<Employee, bool>> predicate)
        {
            var query = employees.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            return query.ToList();
        }

        /// <summary>
        /// 從指定的 URL 載入 Excel 文件。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        public void LoadExcel(string fileName)
        {
            var dataTable = ExcelUtility.Read(fileName, 0, 2, 0, 5);

            foreach (DataRow row in dataTable.Rows)
            {
                employees.Add(new Employee
                {
                    SerialNumber = Convert.ToString(row[0]),
                    Office = Convert.ToString(row[1]),
                    Division = Convert.ToString(row[2]),
                    EmployeeId = Convert.ToString(row[3]),
                    Name = Convert.ToString(row[4])
                });
            }
        }

        /// <summary>
        /// 將 Excel 文件儲存至指定的檔案。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        public void SaveExcel(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}

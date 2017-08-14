using System.Linq;
using RaffleDraw.App.Views;
using RaffleDraw.Data;

namespace RaffleDraw.App.Presenters
{
    public class EmployeePresenter
    {
        private IEmployeeView employeeView;
        private IEmployeeRepository employeeRepository;

        /// <summary>
        /// 初始化 <see cref="EmployeePresenter"/> 類別的新執行個體。
        /// </summary>
        /// <param name="employeeView">獎項視圖。</param>
        /// <param name="employeeRepository">獎項倉儲。</param>
        public EmployeePresenter(IEmployeeView employeeView, IEmployeeRepository employeeRepository)
        {
            employeeView.EmployeePresenter = this;
            this.employeeView = employeeView;
            this.employeeRepository = employeeRepository;
        }

        /// <summary>
        /// 載入 Excel。
        /// </summary>
        /// <param name="fileName">文件名稱。</param>
        public void LoadExcel(string fileName)
        {
            employeeRepository.LoadExcel(fileName);
            UpdateView();
        }

        /// <summary>
        /// 更新視圖。
        /// </summary>
        private void UpdateView()
        {
            var employees = employeeRepository.Many(null).ToList();

            employeeView.Employees = employees;
        }
    }
}
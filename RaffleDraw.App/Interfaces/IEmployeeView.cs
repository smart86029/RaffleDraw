using System.Collections.Generic;
using RaffleDraw.App.Presenters;
using RaffleDraw.Models;

namespace RaffleDraw.App.Views
{
    /// <summary>
    /// 員工視圖。
    /// </summary>
    public interface IEmployeeView
    {
        /// <summary>
        /// 員工。
        /// </summary>
        ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// 員工主持人。
        /// </summary>
        EmployeePresenter EmployeePresenter { get; set; }
    }
}
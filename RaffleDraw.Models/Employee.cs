using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaffleDraw.Models
{
    /// <summary>
    /// 員工類別。
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 取得或設定序號。
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 取得或設定員工編號。
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 取得或設定姓名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 取得或設定一級單位。
        /// </summary>
        public string Office { get; set; }

        /// <summary>
        /// 取得或設定二級單位。
        /// </summary>
        public string Division { get; set; }

        /// <summary>
        /// 取得或設定中的獎項。
        /// </summary>
        public virtual Prize Prize { get; set; }
    }
}

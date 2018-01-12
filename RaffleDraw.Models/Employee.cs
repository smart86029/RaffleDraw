using GalaSoft.MvvmLight;
using System.ComponentModel.DataAnnotations;

namespace RaffleDraw.Models
{
    /// <summary>
    /// 員工類別。
    /// </summary>
    public class Employee : ObservableObject
    {
        private string serialNumber;
        private string employeeId;
        private string name;
        private string office;
        private string division;
        private Prize prize;

        /// <summary>
        /// 取得或設定序號。
        /// </summary>
        public string SerialNumber
        {
            get => serialNumber;
            set => Set(ref serialNumber, value);
        }

        /// <summary>
        /// 取得或設定員工編號。
        /// </summary>
        public string EmployeeId
        {
            get => employeeId;
            set
            {
                
                Set(ref employeeId, value);
            }
                
        }

        /// <summary>
        /// 取得或設定姓名。
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string Name
        {
            get => name;
            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = nameof(EmployeeId) });
                Set(ref name, value);
            }
        }

        /// <summary>
        /// 取得或設定一級單位。
        /// </summary>
        public string Office
        {
            get => office;
            set => Set(ref office, value);
        }

        /// <summary>
        /// 取得或設定二級單位。
        /// </summary>
        public string Division
        {
            get => division;
            set => Set(ref division, value);
        }

        /// <summary>
        /// 取得或設定中的獎項。
        /// </summary>
        public virtual Prize Prize

        {
            get => prize;
            set => Set(ref prize, value);
        }
    }
}
using System.Collections.Generic;

namespace RaffleDraw.Models
{
    /// <summary>
    /// 獎項類別。
    /// </summary>
    public class Prize
    {
        /// <summary>
        /// 取得或設定序號。
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 取得或設定數量。
        /// </summary>
        public int Quentity { get; set; }

        /// <summary>
        /// 取得或設定內容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 取得或設定提供者。
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// 取得或設定中獎者。
        /// </summary>
        public virtual List<Employee> Winners { get; set; }
    }
}
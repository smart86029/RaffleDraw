using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace RaffleDraw.Models
{
    /// <summary>
    /// 獎項類別。
    /// </summary>
    public class Prize:ObservableObject
    {
        private string serialNumber;
        private int quentity;
        private string content;
        private string provider;

        /// <summary>
        /// 取得或設定序號。
        /// </summary>
        public string SerialNumber
        {
            get => serialNumber;
            set => Set(ref serialNumber, value);
        }

        /// <summary>
        /// 取得或設定數量。
        /// </summary>
        public int Quentity
        {
            get => quentity;
            set => Set(ref quentity, value);
        }

        /// <summary>
        /// 取得或設定內容。
        /// </summary>
        public string Content
        {
            get => content;
            set => Set(ref content, value);
        }

        /// <summary>
        /// 取得或設定提供者。
        /// </summary>
        public string Provider
        {
            get => provider;
            set => Set(ref provider, value);
        }

        /// <summary>
        /// 取得或設定中獎者。
        /// </summary>
        public virtual ObservableCollection<Employee> Winners { get; set; } = new ObservableCollection<Employee>();
    }
}
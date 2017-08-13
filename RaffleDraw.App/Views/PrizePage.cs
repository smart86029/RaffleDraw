using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MetroFramework.Controls;
using RaffleDraw.App.Presenters;
using RaffleDraw.Common;
using RaffleDraw.Models;

namespace RaffleDraw.App.Views
{
    public partial class PrizePage : MetroUserControl, IPrizeView
    {
        MetroTabPage owner;

        public PrizePage()
        {
            InitializeComponent();
        }

        public PrizePage(MetroTabPage owner) : this()
        {
            this.owner = owner;
            owner.Controls.Add(this);

            //Visible = false;
            BringToFront();
        }

        public ICollection<Prize> Prizes
        {
            get
            {
                return prizesComboBox.DataSource as List<Prize>;
            }
            set
            {
                prizesComboBox.DataSource = value;
                prizesComboBox.DisplayMember = "Content";
                prizesComboBox.ValueMember = "SerialNumber";
            }
        }

        public PrizePresenter PrizePresenter { get; set; }

        private void importPrizeLink_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "Excel 檔案 (*xls; *.xlsx; *.xlsm)|*xls; *.xlsx; *.xlsm|所有檔案 (*.*)|*.*";

            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrizePresenter.LoadExcel(openFileDialog.FileName);
                importPrizeLink.Text = "完成";
                importPrizeLink.Enabled = false;
            }
        }
    }
}

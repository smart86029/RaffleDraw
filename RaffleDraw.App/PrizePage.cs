using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MetroFramework.Controls;
using RaffleDraw.Common;
using RaffleDraw.Models;

namespace RaffleDraw.App
{
    public partial class PrizePage : MetroUserControl
    {
        MetroTabPage owner;
        private List<Prize> prizes = new List<Prize>();

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

        private void importPrizeLink_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "Excel 檔案 (*xls; *.xlsx; *.xlsm)|*xls; *.xlsx; *.xlsm|所有檔案 (*.*)|*.*";

            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadExcel(openFileDialog.FileName);
                importPrizeLink.Text = "完成";
                importPrizeLink.Enabled = false;
            }
        }

        private void LoadExcel(string fileName)
        {
           var dataTable= ExcelUtility.Read(fileName, 0, 1, 0, 4);
            foreach (DataRow row in dataTable.Rows)
            {
                prizes.Add(new Prize
                {
                    SerialNumber = Convert.ToString(row[0]),
                    Quentity = Convert.ToInt32(row[1]),
                    Content = Convert.ToString(row[2]),
                    Provider = Convert.ToString(row[3])
                });
            }

            prizesComboBox.DataSource = prizes;
            prizesComboBox.DisplayMember = "Content";
            prizesComboBox.ValueMember = "SerialNumber";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MetroFramework.Controls;
using RaffleDraw.App.Presenters;
using RaffleDraw.Models;

namespace RaffleDraw.App.Views
{
    public partial class EmployeePage : MetroUserControl, IEmployeeView
    {
        private MetroTabPage owner;

        public EmployeePage()
        {
            InitializeComponent();
        }

        public EmployeePage(MetroTabPage owner) : this()
        {
            this.owner = owner;
            owner.Controls.Add(this);

            //Visible = false;
            BringToFront();
        }

        public ICollection<Employee> Employees
        {
            get
            {
                return employeesComboBox.DataSource as List<Employee>;
            }
            set
            {
                employeesComboBox.DataSource = value;
                employeesComboBox.DisplayMember = "Name";
                employeesComboBox.ValueMember = "SerialNumber";
            }
        }

        public EmployeePresenter EmployeePresenter { get; set; }

        private void importEmployeeLink_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "Excel 檔案 (*xls; *.xlsx; *.xlsm)|*xls; *.xlsx; *.xlsm|所有檔案 (*.*)|*.*";

            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                EmployeePresenter.LoadExcel(openFileDialog.FileName);
                importEmployeeLink.Text = "完成";
                importEmployeeLink.Enabled = false;
            }
        }
    }
}
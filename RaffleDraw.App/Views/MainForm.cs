using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaffleDraw.Data;
using RaffleDraw.App.Presenters;

namespace RaffleDraw.App.Views
{
    public partial class MainForm : MetroForm
    {
        private PrizePage prizePage;
        private EmployeePage employeePage;

        public MainForm()
        {
            InitializeComponent();
            prizePage = new PrizePage(prizeTabPage);
            employeePage = new EmployeePage(employeeTabPage);

            // TODO: IOC
            var prizeRepository = new PrizeRepository();
            var prizePresenter = new PrizePresenter(prizePage, prizeRepository);
            var employeeRepository = new EmployeeRepository();
            var employeePresenter = new EmployeePresenter(employeePage, employeeRepository);
        }
    }
}

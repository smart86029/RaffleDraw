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

        public MainForm()
        {
            InitializeComponent();
            prizePage = new PrizePage(prizeTabPage);

            // TODO: IOC
            var repository = new PrizeRepository();
            var presenter = new PrizePresenter(prizePage, repository);
        }
    }
}

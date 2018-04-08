using SeleniumDataInsert.PortalUpdateManager.DataLayer;
using SeleniumDataInsert.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumDataInsert.AnalyticsModule
{
    public partial class AnalyticsPresenter : Form
    {
        public AnalyticsPresenter()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            UserLogin userLog = new UserLogin();
            userLog.Show();
        }

        private void AnalyticsPresenter_Load(object sender, EventArgs e)
        {


            AnalayticsDataLayer objAnalayticsDataLayer = new AnalayticsDataLayer();
            List<string> ListStrings = objAnalayticsDataLayer.getUniqueIds();
            foreach (var UniqueID in ListStrings)
            {

                this.chart1.Series[0].Points.AddXY("Scorpus", objAnalayticsDataLayer.GetCountsofDatabyCrawl(UniqueID));
            }


           
        }
    }
}

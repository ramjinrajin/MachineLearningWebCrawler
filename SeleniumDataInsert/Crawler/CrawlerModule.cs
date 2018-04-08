using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumDataInsert.Config;
using SeleniumDataInsert.Loop;
using SeleniumDataInsert.Models.AuthorProp.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumDataInsert.Crawler
{
  public  class CrawlerModule
    {
        List<Author> authors;
        DataTable dt = new DataTable();
        public DataTable GetFetchedDataFromWebCrawler()
        {
            IWebDriver driver = new ChromeDriver();
            authors = new List<Author>();
            DataGridView dataUrls = new DataGridView();

            int k = 0;
            DataGridViewRow rowurls;
            dataUrls.Columns.Add("Url", "Urls");

            List<long> AuthorIds = Variables.ListLoopUrlIds();
            foreach (var AuthorId in AuthorIds)
            {

                rowurls = new DataGridViewRow();
                dataUrls.Rows.Add();
                rowurls = dataUrls.Rows[k];
                rowurls.Cells["Url"].Value = AuthorId;
                k++;
            }





            bool HasErrors = false;
            Guid g;
            g = Guid.NewGuid();
            string FileName = g.ToString();

            
            dt.Columns.Add("Author name", typeof(string));
            dt.Columns.Add("University", typeof(string));
            dt.Columns.Add("H index", typeof(string));
            dt.Columns.Add("Document Name", typeof(string));
            dt.Columns.Add("Errors", typeof(string));

            try
            {
                LoopModule loopModule = new LoopModule();
                authors= loopModule.LoopWebSiteWithXpath(driver, dataUrls, dt);
                Variables.AuthorDataSource = authors;
                driver.Close();
                driver.Dispose();

                return dt;
            }
            catch  
            {

 
                HasErrors = true;
                Task.Run(() =>
                {
                    //var dialogResult = MessageBox.Show(ex.Message.ToString(), "Warning", MessageBoxButtons.OKCancel);
                    if (HasErrors)
                    {
                        //MessageBox.Show("Some error occured Unable to complete the process" + Environment.NewLine + "Collected datas saved that was collected before the error occured", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                });

                return dt;
            }
            finally
            {
                if (dt!=null)
                {
                   string SystemEnvironmentPath = Environment.CurrentDirectory.ToString();
                   string strFilePath = string.Format(@"{0}FinalReport.csv", SystemEnvironmentPath);
                   Variables.CSVDataSource = dt;
                 //  ToCSV(dt, strFilePath);
                }
                 

               
                if (!HasErrors)
                    MessageBox.Show("Data successfully collected");
                driver.Close();
                driver.Dispose();

                
            }
        }

      

      
    }
}

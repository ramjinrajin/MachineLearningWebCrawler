using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
using System.Data.SqlClient;
using SeleniumDataInsert.Models.AuthorProp.Properties;
using SeleniumDataInsert.Models.AuthorProp.DataLayer;
using SeleniumDataInsert.Crawler;
using System.Configuration;
using SeleniumDataInsert.Mailer;
using SeleniumDataInsert.Config;
using SeleniumDataInsert.Selenium;
 


namespace SeleniumDataInsert
{
    public partial class Form1 : Form
    {
        CrawlerModule objCrawlerModule;
        public Form1()
        {
            InitializeComponent();
 
            this.StartPosition = FormStartPosition.Manual;
            foreach (var scrn in Screen.AllScreens)
            {
                if (scrn.Bounds.Contains(this.Location))
                {
                    this.Location = new Point(scrn.Bounds.Right - this.Width, scrn.Bounds.Top);
                    return;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Automate();
        
        }

        DataTable SourceDataFromWebCrawler;
        private void Automate()
        {
            objCrawlerModule = new CrawlerModule();
            SourceDataFromWebCrawler = objCrawlerModule.GetFetchedDataFromWebCrawler();
            dataGridView1.DataSource = SourceDataFromWebCrawler;
            if (SourceDataFromWebCrawler!=null)
            SMTPProtocol.NotifyPartners(DateTime.Now.ToString() + " WebCrawler Report", ConvertDataTableToHTML(SourceDataFromWebCrawler, EmailConfig.MessageContent), EmailConfig.ToEmail);
        }

        private static void AuthorFetchingModel(long AuthorId)
        {
           
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(string.Format(Variables.BaseUrl, AuthorId));

            string AuthorNamePath = "//*[@id='authDetailsNameSection']/div/div[1]/div[1]/h2";
            string UniversityPath = "//*[@id='authDetailsNameSection']/div/div[1]/div[1]/div[1]";

            IWebElement AuthorElement = driver.FindElement(By.XPath(AuthorNamePath));
            IWebElement AuthorUniversityElement = driver.FindElement(By.XPath(UniversityPath));

            string AuthorName = AuthorElement.Text;
            string UniveristyAuthor = AuthorUniversityElement.Text;

            driver.Close();
        }

        List<Author> authors;
     
     



        public  string ConvertDataTableToHTML(DataTable dt, string MsgContent)
        {

            string TableCss = @"""border-collapse: collapse;width: 100%;""";
            string ThTrCss = @""" border: 1px solid #dddddd;text-align: left;padding: 5px;""";
            string TrCurrentStatusHighlight = @""" border: 1px solid #dddddd;text-align: left;padding: 5px; background-color: #53A3CD; """; //Full Dark
            string TrCurrentStatusDotted = @""" border-style: dashed; border-color:#53A3CD """; //Full Dark
            string html = MsgContent + "</br> <table style=" + TableCss + ">"; //style=""

            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName != "has_departed")//just hide the unwanted column names to be displayed
                {
                    if (dt.Columns[i].ColumnName != "CurrentStation")
                    {
                        html += "<th style=" + ThTrCss + ">" + dt.Columns[i].ColumnName + "</th>";
                    }

                }
            }


            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";

                for (int j = 0; j < dt.Columns.Count - 2; j++) //count -2 in the sence it will hide the has_departed  and CurrentStation column
                {

                    if (dt.Rows[i][0].ToString() == dt.Rows[i][4].ToString()) //make the row blue if current station is equal to binding station
                    {

                        html += "<td style=" + TrCurrentStatusHighlight + ">" + dt.Rows[i][j].ToString() + "</td>";
                    }
                    else
                    {
                        html += "<td style=" + ThTrCss + ">" + dt.Rows[i][j].ToString() + "</td>";

                    }
                }

                html += "</tr>";

            }
            html += "</table>";
            return html;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                Application.Restart();
                
            }
            catch (Exception)
            {
                
                throw;
            }
             


           // this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            authors = new List<Author>
            {
                new Author
                {
                    AuthorName="sdsd0",
                    University="asd",
                    DocumentName="sds00",
                    Hindex="sds"
                }
            };
            int CountResponse = 0; 
            AuthorDataLayer objDataLayer = new AuthorDataLayer();
            if (authors.Count>0)
            {
                foreach (var author in authors)
                {
                    if (author!=null)
                    {
                     int Result =objDataLayer.SaveAuthors(author);
                        if(Result==1)
                        {
                            CountResponse ++;
                        }
                    }
                  
                }
            }

            if(CountResponse==authors.Count)
            {
                MessageBox.Show("Data saved to database");
            }
            else
            {
                MessageBox.Show("Some error occured,This may occured due to the duplicate entires,Data saved partially.Please check your database for ensure data saved as expected");
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string SystemEnvironmentPath = Environment.CurrentDirectory.ToString();
            string strFilePath = string.Format(@"{0}FinalReport.csv", SystemEnvironmentPath);
            ToCSV(Variables.CSVDataSource, strFilePath);
            FileInfo fi = new FileInfo(strFilePath);
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(strFilePath);
            }
            else
            {
                MessageBox.Show("Report generation failed");
            }
        }

        private void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, true);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            UserLogin userLog = new UserLogin();
            userLog.Show();
        } 

    }
}

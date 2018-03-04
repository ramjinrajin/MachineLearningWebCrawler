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


namespace SeleniumDataInsert
{
    public partial class Form1 : Form
    {
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
            //List<long> AuthorIds = new List<long>
            //{
            //    56596324000,
            //    56596325000,
            //    56596326000

            //};

            //foreach (var Authorid in AuthorIds)
            //{
            //    AuthorFetchingModel(Authorid);
            //}
            //Console.ReadLine();
        }

        private static void AuthorFetchingModel(long AuthorId)
        {
            //Initialize the web driver
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(string.Format("http://scopus.com/authid/detail.uri?origin=AuthorProfile&authorId={0}", AuthorId));

            //Set the Html Element path
            string AuthorNamePath = "//*[@id='authDetailsNameSection']/div/div[1]/div[1]/h2";
            string UniversityPath = "//*[@id='authDetailsNameSection']/div/div[1]/div[1]/div[1]";

            //Fetch the Data
            IWebElement AuthorElement = driver.FindElement(By.XPath(AuthorNamePath));
            IWebElement AuthorUniversityElement = driver.FindElement(By.XPath(UniversityPath));


            string AuthorName = AuthorElement.Text;
            string UniveristyAuthor = AuthorUniversityElement.Text;

            Console.WriteLine("The author name is " + AuthorName);
            Console.WriteLine("The Univeristy name is " + UniveristyAuthor);

            driver.Close();
        }

        List<Author> authors;
        private void Automate()
        {
            IWebDriver driver = new ChromeDriver();
            authors = new List<Author>();
            DataGridView dataUrls = new DataGridView();

            int k = 0;
            DataGridViewRow rowurls;
            dataUrls.Columns.Add("Url","Urls");

            List<long> AuthorIds = new List<long>
            {
                56596324000,
                56596325000,
                56596326000

            };

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

            DataTable dt = new DataTable();
            dt.Columns.Add("Author name", typeof(string));
            dt.Columns.Add("University", typeof(string));
            dt.Columns.Add("H index", typeof(string));
            dt.Columns.Add("Document Name", typeof(string));
            dt.Columns.Add("Errors", typeof(string));

            try
            {
              

                foreach (DataGridViewRow row in  dataUrls.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() != " ")
                        {
                            driver.Navigate().GoToUrl(string.Format("http://scopus.com/authid/detail.uri?origin=AuthorProfile&authorId={0}", cell.Value.ToString()));
                           // driver.Navigate().GoToUrl(cell.Value.ToString());


                            string AuthorTextXPath = "//*[@id='authDetailsNameSection']/div/div[1]/div[1]/h2";
                            string UniversityTextXPath = "//*[@id='authDetailsNameSection']/div/div[1]/div[1]/div[1]";
                            string hindexTextXpath = "//*[@id='authorDetailsHindex']/div/div[2]/span";
                            string DocumentTextXpath = "//*[@id='authorDetailsDocumentsByAuthor']/div/div[2]/span";

                            /** Find the element **/
                            IWebElement h3Element = driver.FindElement(By.XPath(AuthorTextXPath));
                            IWebElement UniversityElement = driver.FindElement(By.XPath(UniversityTextXPath));
                            IWebElement hindexElement = driver.FindElement(By.XPath(hindexTextXpath));
                            IWebElement DocumentElement = driver.FindElement(By.XPath(DocumentTextXpath));


                            /** Grab the text **/
                            string AuthorName = h3Element.Text;
                            string Univerisity = UniversityElement.Text;
                            string Hindex = "h-index: " + hindexElement.Text;
                            string DocumentName = "Documents: " + DocumentElement.Text;
                            //driver.Close();
                            //driver.Dispose();
                            // PopulateRows(AuthorName, Univerisity, Hindex, DocumentName);
                            dt.Rows.Add(AuthorName, Univerisity, Hindex, DocumentName,"NIl");
                            Author author = new Author
                            {
                                AuthorName=AuthorName,
                                DocumentName=DocumentName,
                                Hindex=Hindex,
                                University=Univerisity
                            };

                            authors.Add(author);
                        }


                    }
                }
                driver.Close();
                driver.Dispose();


            }
            catch (Exception ex)
            {

                //string strFilePath = string.Format(@"D:\Deeja_{0}.csv", FileName);
                //ToCSV(dt, strFilePath);
                //MessageBox.Show("File sucessfully generated and save to D drive");
                HasErrors = true;
                Task.Run(() =>
                {
                    //var dialogResult = MessageBox.Show(ex.Message.ToString(), "Warning", MessageBoxButtons.OKCancel);
                    if (HasErrors)
                    {
                        //MessageBox.Show("Some error occured Unable to complete the process" + Environment.NewLine + "Collected datas saved that was collected before the error occured", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                });

            }
            finally
            {
                dataGridView1.DataSource = dt;

                //string strFilePath = string.Format(@"C:\FinalReport_{0}.csv", FileName);
                //ToCSV(dt, strFilePath);
                if (!HasErrors)
                    MessageBox.Show("Data successfully collected");
                driver.Close();
                driver.Dispose();

                    
            }
        }

        private void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
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
            
        }

    }
}

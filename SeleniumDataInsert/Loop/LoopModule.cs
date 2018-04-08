using OpenQA.Selenium;
using SeleniumDataInsert.Config;
using SeleniumDataInsert.Models.AuthorProp.DataLayer;
using SeleniumDataInsert.Models.AuthorProp.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumDataInsert.Loop
{
    class LoopModule
    {
        public List<Author> LoopWebSiteWithXpath(IWebDriver driver, DataGridView dataUrls, DataTable dt)
        {
            List<Author> outPutAuthors = new List<Author>();
            foreach (DataGridViewRow row in dataUrls.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() != " ")
                    {
                        driver.Navigate().GoToUrl(string.Format(Variables.BaseUrl, cell.Value.ToString()));
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
                        dt.Rows.Add(AuthorName, Univerisity, Hindex, DocumentName, "NIl");
                        Author author = new Author
                        {
                            AuthorName = AuthorName,
                            DocumentName = DocumentName,
                            Hindex = Hindex,
                            University = Univerisity
                        };

                        AuthorDataLayer onbjAuthorDataLayer = new AuthorDataLayer();
                        onbjAuthorDataLayer.SaveAuthors(author);
                        outPutAuthors.Add(author);
                    }


                }
            }
            return outPutAuthors;
        }
    }
}

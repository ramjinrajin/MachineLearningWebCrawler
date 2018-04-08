using SeleniumDataInsert.Models.AuthorProp.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumDataInsert.Config
{
    public static class  EmailConfig
    {
        public static string ToEmail = "ramjinrajin@gmail.com";
        public static string MessageContent = "Web crawler report generated based on the automation tool";
    }

    public static class Variables
    {
        public static DataTable CSVDataSource;

        public static List<Author> AuthorDataSource;

        public static string BaseUrl
        {
            get
            {
                return "http://scopus.com/authid/detail.uri?origin=AuthorProfile&authorId={0}";
            }
        }

        public static List<long> ListLoopUrlIds()
        {
            List<long> loopurlIds = new List<long> 
            {
                56596325000,56596326000
               
                 
            };

            return loopurlIds;
        }

    }
}

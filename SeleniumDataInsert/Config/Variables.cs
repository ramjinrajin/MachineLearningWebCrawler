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
        public static string MessageContent = "This mail is auto generated based on the data collected from the selenium";
    }

    public static class Variables
    {
        public static DataTable CSVDataSource;
        

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
                56596324000
                
            };

            return loopurlIds;
        }

    }
}

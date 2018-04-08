using SeleniumDataInsert.Models.AuthorProp.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumDataInsert.Models.AuthorProp.DataLayer
{
    public class AuthorDataLayer
    {
        public int SaveAuthors(Author objAuthor)
        {
   
            string db = Environment.CurrentDirectory.ToString();
            using (SqlConnection con = new SqlConnection(string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0}\MasterDb.mdf;Integrated Security=True", db)))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spInsertAuthor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AuthorName",objAuthor.AuthorName);
                    cmd.Parameters.AddWithValue("@University", objAuthor.University);
                    cmd.Parameters.AddWithValue("@Hindex", objAuthor.Hindex);
                    cmd.Parameters.AddWithValue("@DocumentName", objAuthor.DocumentName);
                    cmd.Parameters.AddWithValue("@UniqueCode", UniqueCodeGenerator());
                    return (int)cmd.ExecuteScalar();


                }
                catch
                {

                    return 0;
                }
                finally
                {
                    con.Close();
                }
            }



         
        }

        private string UniqueCodeGenerator()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumDataInsert.PortalUpdateManager.DataLayer
{
   public class AnalayticsDataLayer
    {
        public int GetCountsofDatabyCrawl(string UniqueId)
        {
            int Count = 0;
            string db = Environment.CurrentDirectory.ToString();
            using (SqlConnection con = new SqlConnection(string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0}\MasterDb.mdf;Integrated Security=True", db)))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) AS COUNT from AuthorDetails where UniqueCode=@UniqueCode", con);
                    cmd.Parameters.AddWithValue("@UniqueCode", UniqueId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            Count = int.Parse(rdr["COUNT"].ToString());
                        }
                    }

                    return Count;
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


        public List<string> getUniqueIds()
        {
            List<string> Ids = new List<string>();

            string db = Environment.CurrentDirectory.ToString();
            using (SqlConnection con = new SqlConnection(string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0}\MasterDb.mdf;Integrated Security=True", db)))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select UniqueCode from UniqueCodeMaster", con);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Ids.Add(rdr["UniqueCode"].ToString());
                        }
                    }

                    return Ids;
                }
                catch
                {

                    return Ids;
                }
                finally
                {
                    con.Close();
                }
            }
        }

    }
}

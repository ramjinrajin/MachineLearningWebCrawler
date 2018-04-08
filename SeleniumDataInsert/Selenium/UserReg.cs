using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumDataInsert.Selenium
{
    public partial class UserReg : Form
    {
        public UserReg()
        {
            InitializeComponent();
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string db = Environment.CurrentDirectory.ToString();
            using (SqlConnection con = new SqlConnection(string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0}\MasterDb.mdf;Integrated Security=True", db)))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into [User] Values (@username,@password)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                  
                    cmd.ExecuteNonQuery();

                }
                catch(Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}

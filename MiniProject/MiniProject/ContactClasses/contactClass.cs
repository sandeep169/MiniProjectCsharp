using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.ContactClasses
{
    class contactClass
    {
        //o reffrences 
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public DataTable Select()
        {
            /// step 1: database connection
           
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from tbl_contact";
                //creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex) 
            { 
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //inserting data into database

        public bool Insert (contactClass c)
        {
            //creating defualt return type abd setting uts value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO tbl_contact(FirstName,LastName,ContactNo,Address,Gender) VALUES(@FirstName,@LastName,@ContactNo,@Address,@Gender)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                //CREATE PARAMETER TO ADD DATA
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query run successfully then the value of rows will be greater than zero else its will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool Update(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "UPDATE tbl_contact SET FirstName =@FirstName,LastName=@LastName,ContactNo=@ContactNo,Address=@Address,Gender=@Gender WHERE ContactID=@ContactID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query run successfully then the value of rows will be greater than zero else its will be 0
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
             return isSuccess;            
        }
       public bool Delete (contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;                    
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

    }
}

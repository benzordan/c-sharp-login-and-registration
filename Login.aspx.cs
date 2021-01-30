using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Drawing;

namespace as_assignment
{
    public partial class Login : System.Web.UI.Page
    {
        string libraryDB = System.Configuration.ConfigurationManager.ConnectionStrings["libraryDB"].ConnectionString;
        string errorMsg = "Email and/or password not found";
        static int loginAttempt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // -----------------------------------------------------------------------------------
                //                      Mitigate Cross Site Scripting by encoding 
                // -----------------------------------------------------------------------------------
                tb_email.Text = HttpUtility.HtmlEncode(tb_email.Text);
                tb_PW.Text = HttpUtility.HtmlEncode(tb_PW.Text);

                
                string dbEmail = getDBEmail(tb_email.Text);
                string dbStatus = getDBStatus(tb_email.Text);
                if (dbEmail != null && dbEmail.Length > 0)
                {

                    if (dbStatus == "unlocked")
                    {

                    // Check if password is valid
                        SHA512Managed hashing = new SHA512Managed();
                        string dbHash = getDBHash(tb_email.Text);
                        string dbSalt = getDBSalt(tb_email.Text);

                        if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                        {
                            // Convert tb_PW (input password)
                            string pwdWithSalt = tb_PW.Text + dbSalt;
                            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                            string userHash = Convert.ToBase64String(hashWithSalt);
                            if (userHash.Equals(dbHash))
                            {
                                Session["LoggedIn"] = tb_email.Text.Trim();
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                                Response.Redirect("Homepage.aspx", false);
                            } else if (loginAttempt < 3) { 
                                lb_Error.Text = (3-loginAttempt) + " more attempts remaining";
                                lb_Error.ForeColor = Color.Red;
                                loginAttempt++;

                            } else if (loginAttempt ==  3) {
                                setLockStatus(tb_email.Text.ToString());
                                lb_Error.Text = "Account has been locked.";
                                lb_Error.ForeColor = Color.Red;
                                loginAttempt = 0;
                            }
                        }
                    } else
                    {
                        lb_Error.Text = "Your account has been locked. Please contact adminstrator";
                        lb_Error.ForeColor = Color.Red;
                    }
                } else
                {
                    lb_Error.Text = errorMsg;
                    lb_Error.ForeColor = Color.Red;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.ToString());
            }
        }
        protected string getDBEmail(string email)
        {
            string h = null;

            SqlConnection connection = new SqlConnection(libraryDB);
            string sql = string.Format("SELECT Email FROM Account WHERE Email=@email");
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Email"] != null)
                        {
                            if (reader["Email"] != DBNull.Value)
                            {
                                h = reader["Email"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return h;
        }
        protected string getDBHash(string email) {
            string h = null;

            SqlConnection connection = new SqlConnection(libraryDB);
            string sql = string.Format("SELECT PasswordHash FROM Account WHERE Email=@email");
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            } 
            finally
            {
                connection.Close();
            }
            return h;
        }
        protected string getDBSalt(string email)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(libraryDB);
            string sql = string.Format("SELECT PasswordSalt From ACCOUNT WHERE Email=@Email");
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Email", email);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                s = reader["PasswordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return s;
        }
        protected string getDBStatus(string email)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(libraryDB);
            string sql = string.Format("SELECT Status From Account WHERE Email=@Email");
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Email", email);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Status"] != null)
                        {
                            if (reader["Status"] != DBNull.Value)
                            {
                                s = reader["Status"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return s;
        }
        private void setLockStatus(string email)
        {
            SqlConnection connection = new SqlConnection(libraryDB);
            string sql = string.Format("UPDATE Account SET Status=@status WHERE Email=@mail");
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@mail", email);
                cmd.Parameters.AddWithValue("@status", "locked");
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }
        public class MyObject
        {
            public string success { get; set; }
            public List<String> ErrorMessage { get; set; }
        }
        public bool validateCaptcha()
        {
            bool result = true;
            string captchaResponse = Request.Form["g-recaptcha"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6Lcx40EaAAAAAMvU6_1GLQyAh338pDOYmZgjsZrm &response=" + captchaResponse);
            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using ( StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {

                        string jsonResponse = readStream.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            } catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace as_assignment
{
    public partial class Registration : System.Web.UI.Page
    {
        string libraryDB = System.Configuration.ConfigurationManager.ConnectionStrings["libraryDB"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        // -----------------------------------------------------------------------------------
        //  ENCRYPT DATA
        //  This function encrypts any data passed as a parameter
        // -----------------------------------------------------------------------------------
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null; 
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }
        // -----------------------------------------------------------------------------------
        //  INSERT ACCOUNT INTO DATABASE
        //  This function inserts the account details into the database
        // -----------------------------------------------------------------------------------
        protected void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(libraryDB))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName,@LastName,@Email,@PasswordHash,@PasswordSalt,@DOB,@CreditCardNumber,@Status)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            string accStatus = "unlocked";
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", tb_firstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", tb_lastName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", tb_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(tb_DOB.Text.Trim()));
                            cmd.Parameters.AddWithValue("@CreditCardNumber", encryptData(tb_creditCardNumber.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Status", accStatus.Trim());

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        // -----------------------------------------------------------------------------------
        //  CHECK PASSWORD STRENGTH
        //  This function checks the password strength and returns the score of the password
        //  Score starts at 1 
        //  Score increments everytime it fulfills a regex expression
        // -----------------------------------------------------------------------------------
        private int checkPassword(string password)
        {
            int score = 0;

            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[^0-9]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                score++;
            }
            return score;
        }

        // -----------------------------------------------------------------------------------
        //  REGISTER
        //  This function executes when the user clicks on the register button
        // -----------------------------------------------------------------------------------
        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {
                lbFNError.Text = "";
                lbLNError.Text = "";
                lbEmailError.Text = "";
                lbDOB.Text = "";
                lbPWError.Text = "";
                lbCardError.Text = "";
                bool validate = false;
                int validationScore = 8;
                {
                    // -----------------------------------------------------------------------------------
                    //                      Mitigate Cross Site Scripting by encoding 
                    // -----------------------------------------------------------------------------------
                    tb_firstName.Text = HttpUtility.HtmlEncode(tb_firstName.Text);
                    tb_lastName.Text = HttpUtility.HtmlEncode(tb_lastName.Text);
                    tb_email.Text = HttpUtility.HtmlEncode(tb_email.Text);
                    tb_password.Text = HttpUtility.HtmlEncode(tb_password.Text);
                    tb_DOB.Text = HttpUtility.HtmlEncode(tb_DOB.Text);
                    tb_creditCardNumber.Text = HttpUtility.HtmlEncode(tb_creditCardNumber.Text);

                    string dbEmail = getDBEmail(tb_email.Text);
                    // Validate inputs
                    if (tb_firstName.Text == "")
                    {
                        lbFNError.Text = "Please enter your first Name";
                        lbFNError.ForeColor = Color.Red;
                        validationScore--;
                    }
                    if (tb_lastName.Text == "")
                    {
                        lbLNError.Text = "Please enter your last Name";
                        lbLNError.ForeColor = Color.Red;
                        validationScore--;
                    }
                    if (tb_email.Text == "" || !Regex.IsMatch(tb_email.Text, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                    {
                        lbEmailError.Text = "Please enter a valid email";
                        lbEmailError.ForeColor = Color.Red;
                        validationScore--;
                    }
                    if (dbEmail != null)
                    {
                        lbEmailError.Text = "Email exists!!";
                        lbEmailError.ForeColor = Color.Red;
                        validationScore--;
                    }
                    if (tb_DOB.Text == "")
                    {
                        lbDOB.Text = "Please select a valid DOB";
                        lbDOB.ForeColor = Color.Red;
                        validationScore--;
                    }
                    if (tb_password.Text == "")
                    {
                        lbPWError.Text = "Please enter valid password";
                        lbPWError.ForeColor = Color.Red;
                        validationScore--;
                    }
                    if (tb_creditCardNumber.Text == "" && tb_creditCardNumber.Text.Length != 16)
                    {
                        lbCardError.Text = "Please enter a valid credit card number";
                        lbCardError.ForeColor = Color.Red;
                        validationScore--;
                    }
                    if (validationScore == 8) {
                        validate = true;
                    }

                    if (validate)
                    {
                        string pwd = tb_password.Text.ToString().Trim();
                        //Generate random "salt" 
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] saltByte = new byte[8];
                        //Fills array of bytes with a cryptographically strong sequence of random values.
                        rng.GetBytes(saltByte);
                        salt = Convert.ToBase64String(saltByte);
                        SHA512Managed hashing = new SHA512Managed();
                        string pwdWithSalt = pwd + salt;
                        byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        //  Hash account password with salt
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        finalHash = Convert.ToBase64String(hashWithSalt);
                        RijndaelManaged cipher = new RijndaelManaged();
                        cipher.GenerateKey();
                        Key = cipher.Key;
                        IV = cipher.IV;
                        createAccount();
                        Response.Redirect("Login.aspx", false);
                    }
                }   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void checkPW_Click(object sender, EventArgs e)
        {
            tb_password.Text = HttpUtility.HtmlEncode(tb_password.Text);
            int scores = checkPassword(tb_password.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Very Strong";
                    break;
                default:
                    break;
            }
            lbPW.Text = "Status: " + status;
            if (scores < 4)
            {
                lbPW.ForeColor = Color.Red;
                return;
            }
            lbPW.ForeColor = Color.Green;
            tb_password.Text = tb_password.Text;
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
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {

                        string jsonResponse = readStream.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
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
    }
}
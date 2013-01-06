using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Online_Notes
{
    public partial class Register : System.Web.UI.Page
    {

        const string connectionString = "server=mysql.lrk.si;User Id=ns7827;password=pass123;database=ns7827";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                string SQLcommand = "SELECT * FROM USERS WHERE username LIKE '" + UsernameInput.Text.ToLower() + "' OR email LIKE '" + EmailInput.Text.ToLower() + "';";

                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CommonErrorMessage.Text = "Account with this username or email already exists.";
                }
                else
                {
                    reader.Close();
                    SQLcommand = "INSERT INTO USERS (Username, Email, Password) VALUES (?username , ?email , ?password);";
                    command = new MySqlCommand(SQLcommand, connection);

                    command.CommandType = System.Data.CommandType.Text;

                    command.Parameters.Add("?username", MySqlDbType.VarChar).Value = UsernameInput.Text;
                    command.Parameters.Add("?email", MySqlDbType.VarChar).Value = EmailInput.Text;
                    command.Parameters.Add("?password", MySqlDbType.VarChar).Value = PasswordInput.Text;

                    command.ExecuteNonQuery();
                    reader.Close();

                    Response.Redirect("Login.aspx");
                }
                    
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                CommonErrorMessage.Text = "Problem with database connection accured.";
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
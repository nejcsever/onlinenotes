using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Online_Notes
{
    public partial class Login : System.Web.UI.Page
    {
        const string connectionString = "server=mysql.lrk.si;User Id=ns7827;password=pass123;database=ns7827";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Response.Redirect("Default.aspx");
                return;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlConnection adminConnection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string SQLcommand = "SELECT * FROM USERS WHERE email LIKE '" + EmailInput.Text.ToLower() + "' AND password LIKE '" + PasswordInput.Text.ToLower() + "';";

                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CommonErrorMessage.Text = "";

                    /* Check if admin */
                    string adminSQLcommand = "SELECT * FROM ADMINS WHERE UserId LIKE '" + reader.GetInt32(0) + "';";
                    adminConnection.Open();
                    MySqlCommand admincommand = new MySqlCommand(adminSQLcommand, adminConnection);
                    MySqlDataReader adminReader = admincommand.ExecuteReader();
                    Boolean admin = false;
                    if (adminReader.Read())
                        admin = true;

                    User newSessionUser = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), admin);

                    Session["user"] = newSessionUser;

                    Response.Redirect("Default.aspx");
                }
                else
                {
                    CommonErrorMessage.Text = "Wrong email or password.";
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
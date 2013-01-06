using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Online_Notes
{
    public partial class Completed : System.Web.UI.Page
    {
        const string connectionString = "server=mysql.lrk.si;User Id=ns7827;password=pass123;database=ns7827;Allow Zero Datetime=true;Convert Zero Datetime=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            checkUserLoggedIn((User)Session["user"]);
            checkIsUserAdmin((User)Session["user"]);
            showCompletedContent(CompletedNotesPlaceHolder);
            addCountryName(Request.UserHostAddress);
             
        }

        private void addCountryName(string ip)
        {
            GeoWebService.GeoIPService s = new GeoWebService.GeoIPService();
            GeoWebService.GeoIP geoIp = s.GetGeoIP(Request.UserHostAddress);
            WelcomeUserLabel.Text += " you're from " + geoIp.CountryName;
        }

        private void checkIsUserAdmin(User u)
        {
            if (u.isUserAdmin())
                AdminPlaceHolder.Controls.Add(new LiteralControl("<li><a id=\"menu-admin\" href=\"AdminSite.aspx\">Admin</a></li>"));
        }

        private void showCompletedContent(PlaceHolder placeHolder)
        {
 	        MySqlConnection connection = new MySqlConnection(connectionString);
            string content = "";
            Boolean empty = true;
            try
            {
                connection.Open();
                string SQLcommand = "SELECT * FROM NOTES WHERE CategoryId IN (SELECT Id FROM CATEGORIES WHERE UserId LIKE '" + ((User) Session["user"]).getId() + "') AND Completed='1' ORDER BY DateDeadline DESC";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    empty = false;
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(2);
                    string dateCreated = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                    string dateDeadline = (reader.IsDBNull(4) ? "" : reader.GetDateTime(4).ToString("yyyy-MM-dd"));
                    int priority = reader.GetInt32(5);
                    string description = reader.GetString(6);

                    content += "<div class=\"note\" id=\"note" + id + "\"><div class=\"note-title\">" +
                    "<p class=\"note-title-p\">" + title + "</p>"+
                    "</div><div id=\"descriptionnote" + id + "\" class=\"note-description completed\"><table class=\"note-properties\"><tr><td class=\"" + 
                    "first-column\"><label class=\"description\">Title:</label></td><td><label class=\"description\">" + title + "</label></td>" +
                    "</tr><tr><td class=\"first-column\"><label class=\"description\">Completed:</label></td><td><label class=\"description\">" + dateDeadline + "</label></td></tr><tr>" +
                    "<td class=\"first-column\"><label class=\"description\">Priority:</label></td><td><label class=\"description\">" + ((priority == 2)? "High" : (priority == 1)? "Medium" : "Low"  ) + "</label></td>" +
                    "</tr><tr><td class=\"first-column\"><label class=\"description\">Description:</label></td><td>" +
                    "<textarea readonly class=\"edit-note-input\">" + description + "</textarea></td></tr></table>" +
                    "<p class=\"note-created\">Created: " + dateCreated + "</p></div>" +
                    "</div>";
                }
            }
            catch (Exception ex)
            {
                content += "<p>" + ex.Message + "</p>";
            }
            finally
            {
                connection.Close();
            }

            if (empty)
                content = "<p>You have no completed notes.</p>";
            placeHolder.Controls.Add(new LiteralControl(content));
        }

        protected void checkUserLoggedIn(User user)
        {
            if (user == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            else
            {
                WelcomeUserLabel.Text = user.getUsername();
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            User sessionUser = (User)Session["user"];
            if (sessionUser != null)
            {
                Session.Remove("user");
                Response.Redirect("Login.aspx");
            }
        }    
    }
}
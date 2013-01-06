using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Online_Notes
{
    public partial class Top5 : System.Web.UI.Page
    {
        const string connectionString = "server=mysql.lrk.si;User Id=ns7827;password=pass123;database=ns7827;Allow Zero Datetime=true;Convert Zero Datetime=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            checkUserLoggedIn((User)Session["user"]);
            checkIsUserAdmin((User)Session["user"]);
            showTopFiveContent(NotePlaceHolder);
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

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            User sessionUser = (User)Session["user"];
            if (sessionUser != null)
            {
                Session.Remove("user");
                Response.Redirect("Login.aspx");
            }
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

        private void showTopFiveContent(PlaceHolder placeHolder)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string content = "";
            Boolean empty = true;
            try
            {
                connection.Open();
                string SQLcommand = "SELECT * FROM NOTES WHERE CategoryId IN (SELECT Id FROM CATEGORIES WHERE UserId LIKE '" + ((User) Session["user"]).getId() + "') AND DateDeadline='" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND Completed='0' ORDER BY Priority DESC LIMIT 5";
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

                    content += "<div class=\"note\" id=\"note" + id + "\"><div class=\"note-title\"><table><tr><td><p class=\"note-title-p\">" + title +
                    "</p></td><td class=\"second-column\"><button value=\"note" + id +
                    "\" class=\"finish-note basic-button\" title=\"finish note\"></button></td></tr></table></div><div id=\"descriptionnote" + id + "\" class=\"note-description\"><table class=\"note-properties\"><tr><td class=\"" +
                    "first-column\"><label class=\"description\">Title:</label></td><td><input type=\"text\" class=\"note-title-input edit-note-input\" value=\"" + title +
                    "\" /></td></tr><tr><td class=\"first-column\"><label class=\"description\">Deadline:</label></td><td><input value=\"" + dateDeadline + "\" class=\"datepicker edit-note-input\"/></td></tr><tr>" +
                    "<td class=\"first-column\"><label class=\"description\">Priority:</label></td><td>" +
                    "<label><input type=\"radio\" name=\"prioritynote" + id + "\" value=\"2\"" + ((priority == 2) ? " checked " : " ") + ">High</label>" +
                    "<label><input type=\"radio\" name=\"prioritynote" + id + "\" value=\"1\"" + ((priority == 1) ? " checked " : " ") + ">Medium</label>" +
                    "<label><input type=\"radio\" name=\"prioritynote" + id + "\" value=\"0\"" + ((priority == 0) ? " checked " : " ") + ">Low</label>" +
                    "</td></tr><tr><td class=\"first-column\"><label class=\"description\">Description:</label></td><td>" +
                    "<textarea class=\"edit-note-input\">" + description + "</textarea></td></tr></table>" +
                    "<p class=\"note-created\">Created: " + dateCreated + "</p><button class=\"save-note-changes basic-button\">Save changes</button> <button class=\"delete-note basic-button\">Delete note</button></div></div>";
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
                content = "<p>You have no notes due today.</p>";
            placeHolder.Controls.Add(new LiteralControl(content));
        }
    }
}
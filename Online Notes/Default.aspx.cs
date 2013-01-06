using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Web.Services;
using System.Text.RegularExpressions;
using Online_Notes.GeoWebService;

namespace Online_Notes
{
    public partial class Default : System.Web.UI.Page
    {
        const string connectionString = "server=mysql.lrk.si;User Id=ns7827;password=pass123;database=ns7827;Allow Zero Datetime=true;Convert Zero Datetime=true";

        private User sessionUser;
        private PlaceHolder placeHolder;

        protected void Page_Load(object sender, EventArgs e)
        {
            checkUserLoggedIn((User)Session["user"]);
            checkIsUserAdmin((User)Session["user"]);
            addCountryName(Request.UserHostAddress);
            generateDinamicUpdatePanel();
        }

        private void checkIsUserAdmin(User u)
        {
            if (u.isUserAdmin())
                AdminPlaceHolder.Controls.Add(new LiteralControl("<li><a id=\"menu-admin\" href=\"AdminSite.aspx\">Admin</a></li>"));
        }

        private void addCountryName(string ip)
        {
            GeoWebService.GeoIPService s = new GeoWebService.GeoIPService();
            GeoWebService.GeoIP geoIp = s.GetGeoIP(Request.UserHostAddress);
            WelcomeUserLabel.Text += " you're from " + geoIp.CountryName;
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

        public void generateDinamicUpdatePanel()
        {
            Button dinamicButton;
            AsyncPostBackTrigger dinamicTrigger;

            ArrayList categoryList = getCategoryListFromDB(((User) Session["user"]).getId());

            /* Generiramo gumbe za kategorije. */
            foreach (Category c in categoryList)
            {
                dinamicButton = new Button();
                dinamicButton.ID = "category" + c.getId();
                dinamicButton.Text = c.getTitle();
                dinamicButton.CssClass = "category-item";
                dinamicButton.Click += new EventHandler(CategoryButton_Click);

                dinamicTrigger = new AsyncPostBackTrigger();
                dinamicTrigger.ControlID = dinamicButton.ID;
                dinamicTrigger.EventName = "Click";

                CategoryList.Controls.Add(dinamicButton);
            }

            /* Placeholder za rezultat. */
            placeHolder = new PlaceHolder();

            /* Update panel */
            UpdatePanel notesUpdatePanel = new UpdatePanel();
            notesUpdatePanel.ID = "updatePanelDynamic";
            notesUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            notesUpdatePanel.ContentTemplateContainer.Controls.Add(placeHolder);

            /* Generiranje prožilcev ajax zahtev. */
            foreach (Category c in categoryList)
            {
                AsyncPostBackTrigger trigAsynPostback = new AsyncPostBackTrigger();
                trigAsynPostback.ControlID = "category" + c.getId();
                trigAsynPostback.EventName = "Click";

                notesUpdatePanel.Triggers.Add(trigAsynPostback);
                CategoryTitlePanel.Triggers.Add(trigAsynPostback);
            }

            /* Dodamo update panel. */
            NoteList.Controls.Add(notesUpdatePanel);
        }

        private ArrayList getCategoryListFromDB(int userId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            ArrayList result = new ArrayList();
            try
            {
                connection.Open();
                string SQLcommand = "SELECT * FROM CATEGORIES WHERE UserId LIKE '" + userId + "';";

                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Category(reader.GetInt32(0), reader.GetString(2), reader.GetString(3)));
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            sessionUser = (User)Session["user"];
            if (sessionUser != null)
            {
                Session.Remove("user");
                Response.Redirect("Login.aspx");
            }
        }

        protected void CategoryButton_Click(object sender, EventArgs e)
        {
            /* Napolnimo ime in opis kategorije */
            CategorySubname.Text = getCategorySubname(((Button)sender).ID.Substring(8));
            CategoryName.Text = ((Button)sender).Text;
            /* Napolnimo seznam opravil */
            string content = getCategoryContentFromDB(((Button)sender).ID.Substring(8));
            placeHolder.Controls.Add(new LiteralControl(content));

        }

        private string getCategorySubname(string categoryId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string result = "";
            try
            {
                connection.Open();
                string SQLcommand = "SELECT * FROM CATEGORIES WHERE Id LIKE '" + categoryId + "';";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetString(3);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        private string getCategoryContentFromDB(string categoryId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string content = "<div id=\"note-list\">";
            try
            {
                connection.Open();
                string SQLcommand = "SELECT * FROM NOTES WHERE CategoryId LIKE '" + categoryId + "' AND Completed LIKE 0;";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
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
                Response.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            content += "</div>";
            return content;
        }

        [WebMethod]
        public static void DeleteNote(string id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string SQLcommand = "DELETE FROM NOTES WHERE ID LIKE '" + id + "';";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                connection.Close();
            }
        }

        [WebMethod]
        public static void CompleteNote(string id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string SQLcommand = "UPDATE NOTES SET DateDeadline='" + DateTime.Now.ToString("yyyy-MM-dd") + "', Completed='1' WHERE Id='" + id + "'";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                connection.Close();
            }
        }

        [WebMethod]
        public static void UpdateNote(string id, string title, string deadline, string description, string priority)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                /* Preuredimo rok koncanja. */
                DateTime datetime;
                if (DateTime.TryParseExact(deadline, new string[] { "yyyy-MM-dd" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                {
                    deadline = "'" + deadline + "'";
                } else {
                    deadline = "NULL";
                }

                connection.Open();
                string SQLcommand = "UPDATE NOTES SET Title='" + title + "', DateDeadline=" + deadline + ", Description='" + description + "', Priority='" + priority + "' WHERE ID='" + id + "'";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                connection.Close();
            }
        }

        protected void AddCategoryButton_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string SQLcommand = "INSERT INTO CATEGORIES (UserId, Title, Description) VALUES ('" + ((User) Session["user"]).getId() + "', '" + CategoryTitle.Text + "', '" + CategoryDescription.Text + "')";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                connection.Close();
            }
            Response.Redirect("Default.aspx");
        }

        protected void DeleteCategory_Click(object sender, CommandEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string SQLcommand = "DELETE FROM CATEGORIES WHERE ID LIKE '" + SelectedCategory.Value + "';";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                connection.Close();
            }
            Response.Redirect("Default.aspx");
        }

        protected void AddNoteButton_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                /* Preuredimo rok koncanja. */
                DateTime datetime;
                string deadline = datepicker.Text;
                if (DateTime.TryParseExact(deadline, new string[] { "yyyy-MM-dd" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                {
                    deadline = "'" + deadline + "'";
                }
                else
                {
                    deadline = "NULL";
                }
                string SQLcommand = "INSERT INTO NOTES (CategoryId, DateCreated, Title, DateDeadline, Description, Priority, Completed) VALUES ('" + ShownCategory.Value + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + NoteTitle.Text + "', " + deadline +", '" + NoteDescription.Text + "', '" + PriorityList.SelectedValue + "', '0')";
                MySqlCommand command = new MySqlCommand(SQLcommand, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                connection.Close();
            }
            Response.Redirect("Default.aspx");
        }
    }
}
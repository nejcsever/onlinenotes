using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Online_Notes
{
    public partial class AdminSite : System.Web.UI.Page
    {

        const string connectionString = "server=mysql.lrk.si;User Id=ns7827;password=pass123;database=ns7827;Allow Zero Datetime=true;Convert Zero Datetime=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            checkUserLoggedIn((User)Session["user"]);
            checkUserAdmin((User)Session["user"]);
              addCountryName(Request.UserHostAddress);
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

        private void checkUserAdmin(User u)
        {
            if (!u.isUserAdmin())
                Response.Redirect("Default.aspx");
            else
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
    }
}
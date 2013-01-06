using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Notes
{
    public class User
    {
        private int id;
        private string username;
        private string email;
        private Boolean isAdmin;

        public User(int id, string username, string email, Boolean isAdmin)
        {
            this.id = id;
            this.username = username;
            this.email = email;
            this.isAdmin = isAdmin;
        }

        public int getId()
        {
            return id;
        }
        public string getUsername()
        {
            return username;
        }

        public Boolean isUserAdmin()
        {
            return isAdmin;
        }
    }
}
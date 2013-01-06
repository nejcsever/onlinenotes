using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Notes
{
    public class Category
    {
        private int id;
        private string title;
        private string description;

        public Category(int id, string title, string description)
        {
            this.id = id;
            this.title = title;
            this.description = description;
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public string getTitle()
        {
            return title;
        }

        public void setTitle(string title)
        {
            this.title = title;
        }

        public string getDescription()
        {
            return description;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }
    }
}
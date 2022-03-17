using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCareApp.Model
{
    public class Photo
    {
        public int PhotoID { get; set; }
        //public IEnumerable<HttpPostedFile> Picture { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<User> Users { get; set; }


    }
}


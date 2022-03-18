using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SQLite;

namespace HomeCareApp.Model
{
    public class Visit
    {
        [PrimaryKey, AutoIncrement]
        public int VisitID { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public string VisitName { get; set; }

        public List<string> User { get; set; }
        public List<string> Effort { get; set; }

        public List<string> Patient { get; set; }
        public List<string> Medicine { get; set; }

        public string Content { get; set; }
    }
}

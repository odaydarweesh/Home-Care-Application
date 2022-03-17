using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeCareApp.Model
{
    public class MedicineVisit
    {
        [Key, ForeignKey("Visit"), Column(Order = 0)]
        public int VisitID { get; set; }
        [Key, ForeignKey("Medicine"), Column(Order = 1)]
        public int MedicineID { get; set; }
        public virtual User User { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}

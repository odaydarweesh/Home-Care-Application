using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeCareApp.Model
{
    public class EffortVisit
    {
        [Key, ForeignKey("Visit"), Column(Order = 0)]
        public int VisitID { get; set; }
        [Key, ForeignKey("Effort"), Column(Order = 1)]
        public int EffortID { get; set; }
        public virtual User User { get; set; }
        public virtual Effort Effort { get; set; }
    }
}
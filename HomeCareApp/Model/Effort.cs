using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeCareApp.Model
{
    public class Effort
    {
        [Key]
        public int EffortID { get; set; }
        [Required]
        public string EffortName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }


    }
}

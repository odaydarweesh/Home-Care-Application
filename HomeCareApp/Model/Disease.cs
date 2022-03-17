using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeCareApp.Model
{
    public class Disease
    {
        [Key]
        public int DiseaseID { get; set; }
        [Required]
        public string DiseaseName { get; set; }
        [Required]
        public virtual ICollection<User> Users { get; set; }


    }
}

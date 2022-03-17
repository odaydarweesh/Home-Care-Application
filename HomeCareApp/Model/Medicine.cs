using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeCareApp.Model
{
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public string DescriptionName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}

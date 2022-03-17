using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeCareApp.Model
{
    public class Patient
    {
        [PrimaryKey, AutoIncrement]
        public int IdPatient { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        public string Personnummer { get; set; }
        public string Adress { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }
        [Required]
        public byte[] Photo { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}


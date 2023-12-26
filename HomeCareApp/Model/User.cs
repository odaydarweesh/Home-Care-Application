using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static HomeCareApp.Model.Enums;

namespace HomeCareApp.Model
{
    public class User
    {
        //[Table("User")]

        [PrimaryKey, AutoIncrement]
        public Guid UserId { get; set; }//ID-propertie är markerad med PrimaryKey och AutoIncrement-attribut
                                        //för att säkerställa att varje Note-instans i SQLite.NET-databasen
                                        //kommer att ha ett unikt ID från SQLite.NET.
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        public string Personnummer { get; set; }
        public string Adress { get; set; }

        [EmailAddress] [Required]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }

        public byte[] Photo { get; set; }

        //[Required]
        //public Gender Gender { get; set; }
        public Role Role { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserRole { get; set; }
       
        [OneToMany]
        public List<Visit> Visits { get; set; }// En till många relation
        [OneToMany]
        public List<Medicine> Medicines { get; set; }// En till många relation
        [OneToMany]
        public List<Effort> Efforts { get; set; }// En till många relation

    }
}

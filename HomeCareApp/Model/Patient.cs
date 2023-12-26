using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TableAttribute = SQLite.TableAttribute;

namespace HomeCareApp.Model
{
    //[Table("Patient")]

    public class Patient
    {
        [PrimaryKey, AutoIncrement]
        public int IdPatient { get; set; }//ID-propertie är markerad med PrimaryKey och AutoIncrement-attribut
                                          //för att säkerställa att varje Note-instans i SQLite.NET-databasen
                                          //kommer att ha ett unikt ID från SQLite.NET.
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
        public string Description { get; set; }


        [OneToMany]
        public List<Disease> Diseases { get; set; }// En till många relation
        [OneToMany]
        public List<Medicine> Medicines { get; set; }// En till många relation
        [OneToMany]
        public List<Visit> Visits { get; set; }// En till många relation
        [OneToMany]
        public List<Effort> Efforts { get; set; }// En till många relation

    }
}


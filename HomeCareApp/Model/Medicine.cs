using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeCareApp.Model
{
    public class Medicine
    {
        [PrimaryKey, AutoIncrement]
        public int MedicineID { get; set; }//ID-propertie är markerad med PrimaryKey och AutoIncrement-attribut
                                           //för att säkerställa att varje Note-instans i SQLite.NET-databasen
                                           //kommer att ha ett unikt ID från SQLite.NET.
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public string DescriptionName { get; set; }
        public string Time  { get; set; }

        public int Signed { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Patient))]//En främmande nyckel IdPatient refererar till en annan klasss primärnyckel (Patient).
        public int IdPatient { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(User))]//En främmande nyckel UserId refererar till en annan klasss primärnyckel (User).
        public Guid UserId { get; set; }
       
    }
}

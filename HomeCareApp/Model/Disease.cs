using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace HomeCareApp.Model
{
    //[Table("Disease")]

    public class Disease
    {
        [PrimaryKey, AutoIncrement]
        public int DiseaseID { get; set; }//ID-propertie är markerad med PrimaryKey och AutoIncrement-attribut
                                          //för att säkerställa att varje Note-instans i SQLite.NET-databasen
                                          //kommer att ha ett unikt ID från SQLite.NET.
        [Required]
        public string DiseaseName { get; set; }
        public string DiseaseDescription { get; set; }
       
        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Patient))]//En främmande nyckel IdPatient refererar till en annan klasss primärnyckel (Patient).
        public int IdPatient { get; set; }

    }
}
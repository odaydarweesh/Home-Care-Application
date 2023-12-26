using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace HomeCareApp.Model
{
    public class Visit
    {
        [PrimaryKey, AutoIncrement]
        public int VisitID { get; set; }//ID-propertie är markerad med PrimaryKey och AutoIncrement-attribut
                                        //för att säkerställa att varje Note-instans i SQLite.NET-databasen
                                        //kommer att ha ett unikt ID från SQLite.NET.
        public string StartTime { get; set; }

        public string EndTime { get; set; }
        public string VisitName { get; set; }

        public int Signed { get; set; }
        public string VisitType { get; set; }

     
        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Patient))]//En främmande nyckel idPatient refererar till en annan klasss primärnyckel (Patient).
        public int idPatient { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(User))] //En främmande nyckel UserId refererar till en annan klasss primärnyckel(User).
        public Guid UserId { get; set; }


    }
}

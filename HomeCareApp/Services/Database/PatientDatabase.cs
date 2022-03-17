using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Linq;
using HomeCareApp.Model;

namespace HomeCareApp.Services.Database
{
    public class PatientDatabase
    {
        private readonly SQLiteConnection conn;

        //CREATE  
        public PatientDatabase(string dbPath)
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Patient>();
        }

        //READ  
        public IEnumerable<Patient> GetPatient()
        {
            var patient = (from div in conn.Table<Patient>() select div);
            return patient.ToList();
        }
        //INSERT  
        public string AddPatient(Patient patient)
        {
            conn.Insert(patient);
            return "success";
        }
        //DELETE  
        public string DeletePatient(Patient patient)
        {
            conn.Delete(patient);
            return "success";
        }
        public string UpdatePatient(Patient patient)
        {
            conn.Update(patient);
            return "success";
        }
    }
}


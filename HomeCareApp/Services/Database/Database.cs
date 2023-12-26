using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Linq;
using HomeCareApp.Model;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Firebase.Database;

namespace HomeCareApp.Services.Database
{
    public class Database 

    {
      
        private static SQLiteAsyncConnection conn;
       
        public Database()
        {
           
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");// dbPath innehåller en giltig sökväg för databasfilen som lagras
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("HomeCareApp.HomeCareDatabase.db3");
                if (!File.Exists(dbPath))
            {
                FileStream fileStreamToWrite = File.Create(dbPath);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStreamToWrite);
                fileStreamToWrite.Close();
            }
       
            conn = new SQLiteAsyncConnection(dbPath);//När vi har definierat entity kan vi automatiskt generera tabeller i databas
                                                     //genom att anropa CreateTable
            conn.CreateTableAsync<Visit>();
            conn.CreateTableAsync<Patient>();
            conn.CreateTableAsync<User>();
            conn.CreateTableAsync<Medicine>();
            conn.CreateTableAsync<Effort>();
            conn.CreateTableAsync<Disease>();
          

        }

        ////CREATE
        public Task<int> CreatePatient(Patient patient)//// skapar en ny patient
                                                       //// tabellen innehåller en automatiskt inkrementerad primärnyckel, 

        {
            return conn.InsertAsync(patient);
        }
        ////READ  
        public Task<List<Patient>> ReadPatients()            //hämta alla patienter.

        {
            return conn.Table<Patient>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdatePatient(Patient patient)
        {
            return conn.UpdateAsync(patient);
        }
        ////DELETE 
        public Task<int> DeletePatient(Patient patient)// ta bort en spesifik patient
        {
            return conn.DeleteAsync(patient);
        }
        ////SEARCH
        public Task<List<Patient>> SearchPatient(string search)// söka efter en spesifik patient
        {
            return conn.Table<Patient>().Where(p => p.FirstName.StartsWith(search)).ToListAsync();
        }




        ////CREATE
        public Task<int> CreateUser(User user)//// skapar en ny användare
                                              //// tabellen innehåller en automatiskt inkrementerad primärnyckel, 
        {
            return conn.InsertAsync(user);
        }
        ////READ  
        public Task<List<User>> ReadUsers() //hämta alla user.
        {
            return conn.Table<User>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateUser(User user)
        {
            return conn.UpdateAsync(user);
        }
        ////DELETE 
        public Task<int> DeleteUser(User user)// ta bort en spesifik användare
        {
            return conn.DeleteAsync(user);
        }
        //////SEARCH
        public Task<List<User>> SearchUser(string search)
        {
            return conn.Table<User>().Where(p => p.FirstName.StartsWith(search)).ToListAsync();
        }


        ////CREATE
        public Task<int> CreateVisit(Visit visit)//// skapar ett nytt besök
                                                 //// tabellen innehåller en automatiskt inkrementerad primärnyckel, 
        {
            return conn.InsertAsync(visit);
        }
        ////READ  
        public Task<List<Visit>> ReadVisits() //hämta alla besök.
        {
            return conn.Table<Visit>().ToListAsync();
        }

        public Task<List<Visit>> ReadVisitsWithIdUser(Guid idUser)// Metod som visar besök för en specifik användare
        {

            return conn.Table<Visit>().Where(x => x.UserId == idUser).ToListAsync();
        }
        public Task<List<Visit>> ReadVisitsWithIdPatient(int patient)// Metod som visar besök för en specifik patient
        {

            return conn.Table<Visit>().Where(x => x.idPatient == patient).ToListAsync();
        }
        

        ////UPDATE
        public Task<int> UpdateVisit(Visit visit)
        {
            return conn.UpdateAsync(visit);
        }
        ////DELETE 
        public Task<int> DeleteVisit(Visit visit) // ta bort en spesifik besök
        {
            return conn.DeleteAsync(visit);
        }
        ////SEARCH
        public Task<List<Visit>> SearchVisit(string search)
        {
            return conn.Table<Visit>().Where(p => p.VisitName.StartsWith(search)).ToListAsync();
        }

        ////CREATE
        public Task<int> CreateMedicine(Medicine medicine)//// skapar en ny medicine
                                                          //// tabellen innehåller en automatiskt inkrementerad primärnyckel, 
        {
            return conn.InsertAsync(medicine);
        }
        ////READ  
        public Task<List<Medicine>> ReadMedicines() //hämta alla mediciner.
        {
            return conn.Table<Medicine>().ToListAsync();
        }

        public Task<List<Medicine>> ReadMedicinesWithIdUser(Guid _idUser)// Metod som visar läkemedel som en specifik användare kommer att ge till patienten
        {

            return conn.Table<Medicine>().Where(x => x.UserId == _idUser).ToListAsync();
        }
        public Task<List<Medicine>> ReadMedicinesWithIdPatient(int _idPatient)// Metod som visar medicin för en specifik patient
        {

            return conn.Table<Medicine>().Where(x => x.IdPatient == _idPatient).ToListAsync();
        }

        ////UPDATE
        public Task<int> UpdateMedicine(Medicine medicine)
        {
            return conn.UpdateAsync(medicine);
        }
        ////DELETE 
        public Task<int> DeleteMedicine(Medicine medicine) // ta bort en spesifik medicine
        {
            return conn.DeleteAsync(medicine);
        }
        ////SEARCH
        public Task<List<Medicine>> SearchMedicine(string search)
        {
            return conn.Table<Medicine>().Where(p => p.MedicineName.StartsWith(search)).ToListAsync();
        }

        ////CREATE
        public Task<int> CreateEffort(Effort effort)//// skapar en ny insats
                                                    //// tabellen innehåller en automatiskt inkrementerad primärnyckel, 
        {
            return conn.InsertAsync(effort);
        }
        ////READ  
        public Task<List<Effort>> ReadEfforts() //hämta alla insatser.
        {
            return conn.Table<Effort>().ToListAsync();
        }
        public Task<List<Effort>> ReadEffortsWithIdPatient(int _idPatient)// Metod som visar insatser för en specifik patient
        {

            return conn.Table<Effort>().Where(x => x.idPatient == _idPatient).ToListAsync();
        }
        public Task<List<Effort>> ReadEffortsWithIdUser(Guid idUser)// Metod som visar insatser för en specifik användare
        {

            return conn.Table<Effort>().Where(x => x.UserId == idUser).ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateEffort(Effort effort)
        {
            return conn.UpdateAsync(effort);
        }
        ////DELETE 
        public Task<int> DeleteEffort(Effort effort)// ta bort es spesifik insats
        {
            return conn.DeleteAsync(effort);
        }
        ////SEARCH
        public Task<List<Effort>> SearchEffort(string search)// söka efter en specifik insats
        {
            return conn.Table<Effort>().Where(p => p.EffortName.StartsWith(search)).ToListAsync();
        }


        ////CREATE
        public Task<int> CreateDisease(Disease disease)//// skapar en ny sjukdom
                                                       //// tabellen innehåller en automatiskt inkrementerad primärnyckel, 
        {
            return conn.InsertAsync(disease);
        }
        ////READ  
        public Task<List<Disease>> ReadDiseases() //hämta alla sjukdomar.
        {
            return conn.Table<Disease>().ToListAsync();
        }
        public Task<List<Disease>> ReadDiseasesWithIdPatient(int _idPatient) //Metod som visar sjukdomar för en specifik patient
        {

            return conn.Table<Disease>().Where(x => x.IdPatient == _idPatient).ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateDisease(Disease disease)
        {
            return conn.UpdateAsync(disease);
        }
        ////DELETE 
        public Task<int> DeleteDisease(Disease disease)// ta bort en spesifik sjukdom
        {
            return conn.DeleteAsync(disease);
        }
        ////SEARCH
        public Task<List<Disease>> SearchDisease(string search)// söka efter en spesifik disease
        {
            return conn.Table<Disease>().Where(p => p.DiseaseName.StartsWith(search)).ToListAsync();
        }

        
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Linq;
using HomeCareApp.Model;
using System.Threading.Tasks;

namespace HomeCareApp.Services.Database
{
   public class Database 
    {
        private readonly SQLiteAsyncConnection conn;
        public Database(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            //conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTableAsync<Visit>();
            conn.CreateTableAsync<Patient>();
            conn.CreateTableAsync<User>();
            conn.CreateTableAsync<Medicine>();
            conn.CreateTableAsync<Effort>();


        }

        ////CREATE
        public Task<int>CreatePatient(Patient patient) 
        {
            return conn.InsertAsync(patient);           
        }
        ////READ  
        public Task<List<Patient>>ReadPatients()
        {
            return conn.Table<Patient>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdatePatient(Patient patient)
        {
            return conn.UpdateAsync(patient);
        }
        ////DELETE 
        public Task<int> DeletePatient(Patient patient)
        {
            return conn.DeleteAsync(patient);
        }
        ////SEARCH
        public Task<List<Patient>> SearchPatient(string search)
        {
            return conn.Table<Patient>().Where(p=> p.FirstName.StartsWith(search)).ToListAsync();
        }




        ////CREATE
        public Task<int> CreateUser(User user)
        {
            return conn.InsertAsync(user);
        }
        ////READ  
        public Task<List<User>> ReadUsers()
        {
            return conn.Table<User>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateUser(User user)
        {
            return conn.UpdateAsync(user);
        }
        ////DELETE 
        public Task<int> DeleteUser(User user)
        {
            return conn.DeleteAsync(user);
        }
        //////SEARCH
        public Task<List<User>> SearchUser(string search)
        {
            return conn.Table<User>().Where(p => p.FirstName.StartsWith(search)).ToListAsync();
        }


        ////CREATE
        public Task<int> CreateVisit(Visit visit)
        {
            return conn.InsertAsync(visit);
        }
        ////READ  
        public Task<List<Visit>> ReadVisits()
        {
            return conn.Table<Visit>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateVisit(Visit visit)
        {
            return conn.UpdateAsync(visit);
        }
        ////DELETE 
        public Task<int> DeleteVisit(Visit visit)
        {
            return conn.DeleteAsync(visit);
        }
        ////SEARCH
        public Task<List<Visit>> SearchVisit(string search)
        {
            return conn.Table<Visit>().Where(p => p.VisitName.StartsWith(search)).ToListAsync();
        }

        ////CREATE
        public Task<int> CreateMedicine(Medicine medicine)
        {
            return conn.InsertAsync(medicine);
        }
        ////READ  
        public Task<List<Medicine>> ReadMedicines()
        {
            return conn.Table<Medicine>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateMedicine(Medicine medicine)
        {
            return conn.UpdateAsync(medicine);
        }
        ////DELETE 
        public Task<int> DeleteMedicine(Visit medicine)
        {
            return conn.DeleteAsync(medicine);
        }
        ////SEARCH
        public Task<List<Medicine>> SearchMedicine(string search)
        {
            return conn.Table<Medicine>().Where(p => p.MedicineName.StartsWith(search)).ToListAsync();
        }

        ////CREATE
        public Task<int> CreateEffort(Effort effort)
        {
            return conn.InsertAsync(effort);
        }
        ////READ  
        public Task<List<Effort>> ReadEfforts()
        {
            return conn.Table<Effort>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateEffort(Effort effort)
        {
            return conn.UpdateAsync(effort);
        }
        ////DELETE 
        public Task<int> DeleteEffort(Effort effort)
        {
            return conn.DeleteAsync(effort);
        }
        ////SEARCH
        public Task<List<Effort>> SearchEffort(string search)
        {
            return conn.Table<Effort>().Where(p => p.EffortName.StartsWith(search)).ToListAsync();
        }


        ////CREATE
        public Task<int> CreateDisease(Disease disease)
        {
            return conn.InsertAsync(disease);
        }
        ////READ  
        public Task<List<Disease>> ReadDiseases()
        {
            return conn.Table<Disease>().ToListAsync();
        }
        ////UPDATE
        public Task<int> UpdateDisease(Disease disease)
        {
            return conn.UpdateAsync(disease);
        }
        ////DELETE 
        public Task<int> DeleteDisease(Disease disease)
        {
            return conn.DeleteAsync(disease);
        }
        ////SEARCH
        public Task<List<Disease>> SearchDisease(string search)
        {
            return conn.Table<Disease>().Where(p => p.DiseaseName.StartsWith(search)).ToListAsync();
        }
    }
}


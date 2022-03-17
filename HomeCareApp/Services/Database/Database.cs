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

        //CREATE  
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

        ////READ  
        //public IEnumerable<Visit> GetVisit()
        //{
        //    var visit = (from div in conn.Table<Visit>() select div);
        //    return visit.ToList();
        //}

        

        ////INSERT  
        //public string AddVisit(Visit visit)
        //{
        //    conn.Insert(visit);
        //    return "success";
        //}
        ////DELETE  
        //public string DeleteVisit(Visit visit)
        //{
        //    conn.Delete(visit);
        //    return "success";
        //}
        //public string UpdateVisit(Visit visit)
        //{
        //    conn.Update(visit);
        //    return "success";
        //}

        public Task<int>CreatePatient(Patient patient) 
        {
            return conn.InsertAsync(patient); 
                
        }
        public Task<List<Patient>>ReadPatients()
        {
            return conn.Table<Patient>().ToListAsync();

        }
        public Task<int> UpdatePatient(Patient patient)
        {
            return conn.UpdateAsync(patient);

        }
        public Task<int> DeletePatient(Patient patient)
        {
            return conn.DeleteAsync(patient);

        }
        public Task<List<Patient>> Search(string search)
        {
            return conn.Table<Patient>().Where(p=> p.FirstName.StartsWith(search)).ToListAsync();

        }


        ////READ  
        //public IEnumerable<Patient> GetPatient()
        //{
        //    var patient = (from div in conn.Table<Patient>() select div);
        //    return patient.ToList();
        //}
        ////INSERT  
        //public string AddPatient(Patient patient)
        //{
        //    conn.Insert(patient);
        //    return "success";
        //}
        ////DELETE  
        //public string DeletePatient(Patient patient)
        //{
        //    conn.Delete(patient);
        //    return "success";
        //}
        //public string UpdatePatient(Patient patient)
        //{
        //    conn.Update(patient);
        //    return "success";
        //}

        ////READ  
        //public IEnumerable<User> GetUser()
        //{
        //    var user = (from div in conn.Table<User>() select div);
        //    return user.ToList();
        //}
        ////INSERT  
        //public string AddUser(User user)
        //{
        //    conn.Insert(user);
        //    return "success";
        //}
        ////DELETE  
        //public string DeleteUser(User user)
        //{
        //    conn.Delete(user);
        //    return "success";
        //}
        //public string UpdateUser(User user)
        //{
        //    conn.Update(user);
        //    return "success";
        //}
    }
}


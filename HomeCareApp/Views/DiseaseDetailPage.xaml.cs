using HomeCareApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiseaseDetailPage : ContentPage
    {
        
        public DiseaseDetailPage(Disease diseasedetails) // En konstruktör har en parameter diseasedetails typ objekt Disease som visar diseaseinformation
                                                         // såsom namnet på disease och dess beskrivning och namnet på patienten som drabbats av sjukdomen
        {
            var diseaseName = diseasedetails.DiseaseName;
            var diseaseDescription = diseasedetails.DiseaseDescription;
            int idPatient = diseasedetails.IdPatient;
          

            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
           

            var data = db.Table<Patient>().Where(u => u.IdPatient == idPatient).FirstOrDefault();
           

            string patienName = data.FirstName;
           

            InitializeComponent();
            DiseaseNameShow.Text = diseaseName;
            DiseaseDescriptionShow.Text = diseaseDescription;
            PatientNameShow.Text = patienName;
          
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        public DiseaseDetailPage(List<Disease> diseases)
        {
        }

        public DiseaseDetailPage(string diseaseName, string diseaseDescription)
        {
        }
    }
}
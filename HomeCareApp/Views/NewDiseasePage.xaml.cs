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
    public partial class NewDiseasePage : ContentPage
    {
        public NewDiseasePage()
        {
            InitializeComponent();
        }
        Disease _disease;
        public NewDiseasePage(Disease disease)
        {
            InitializeComponent();
            Title = "Edit Information";
            _disease = disease;
            EntryDiseaseName.Text = disease.DiseaseName;
            EntryDiseaseDescription.Text = disease.DiseaseDescription;
            EntryDiseaseName.Focus();
        }

        private async void SelectedPatient(object sender, EventArgs e)
        {
            var items = await App.MyDatabase.ReadPatients();
            EntryPatientFirstName.ItemsSource = (System.Collections.IList)items;

        }
        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryDiseaseName.Text) || string.IsNullOrWhiteSpace(EntryDiseaseDescription.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
            else if (_disease != null)
            {
                EditDisease();
            }
            else
            {
                AddNewDisease();

            }

        }

        async void AddNewDisease()
        {
            
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
            string patientFirstNameEntry = EntryPatientFirstName.Items[EntryPatientFirstName.SelectedIndex];

            var data = db.Table<Patient>().Where(u => u.FirstName == patientFirstNameEntry).FirstOrDefault();
           
             int idPatient = data.IdPatient;
            await App.MyDatabase.CreateDisease(new Model.Disease
            {
                DiseaseName = EntryDiseaseName.Text,
                DiseaseDescription = EntryDiseaseDescription.Text,
                IdPatient = idPatient,

            });
            await Navigation.PushAsync(new DiseasePage());
        }

        async void EditDisease()
        {
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
            string patientFirstNameEntry = EntryPatientFirstName.Items[EntryPatientFirstName.SelectedIndex];

            var data = db.Table<Patient>().Where(u => u.FirstName == patientFirstNameEntry).FirstOrDefault();

            int idPatient = data.IdPatient;
            _disease.DiseaseName = EntryDiseaseName.Text;
            _disease.DiseaseDescription = EntryDiseaseDescription.Text;
            _disease.IdPatient = idPatient;
            await App.MyDatabase.UpdateDisease(_disease);
            await Navigation.PopAsync();

        }
    }
}
using HomeCareApp.Model;
using HomeCareApp.ViewModel;
using SQLite;
using Syncfusion.XForms.ComboBox;
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
    public partial class NewPatientPage : ContentPage
    {
        private PatientViewModel _viewModel;
        private int medicineId;
        private int idPatient;
        private Guid userId;
        private int diseaseId;
        public NewPatientPage()
        {


            StackLayout layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Padding = new Thickness(30)
            };
                 
            InitializeComponent();
        }
        Patient _patient;
        User _user;
        Disease _disease;
        Medicine _medicine;

        public NewPatientPage(Patient patient)
        {
            InitializeComponent();
            Title = "Edit Information";
            _patient = patient;
            EntryFirstName.Text = patient.FirstName;
            EntryLastName.Text = patient.LastName;
            EntryPersonnummer.Text = patient.Personnummer;
            EntryAdress.Text = patient.Adress;
            EntryPhoneNumber.Text = patient.PhoneNumber;
            EntryDescription.Text = patient.Description;
            EntryEmail.Text = patient.Email;

            EntryFirstName.Focus();
        }

        private async void SelectedDisease(object sender, EventArgs e)
        {
            var items = await App.MyDatabase.ReadDiseases();
            EntryDiseaseName.ItemsSource = (System.Collections.IList)items;

        }

        private async void SelectedMedicine(object sender, EventArgs e)
        {
            var items = await App.MyDatabase.ReadMedicines();
            EntryMedicineName.ItemsSource = (System.Collections.IList)items;

        }

        async void Handle_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryFirstName.Text) || string.IsNullOrWhiteSpace(EntryLastName.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
            else if (_patient != null)
            {
                EditPatient();
            }
            else
            {
                AddNewPatient();

            }
            await Navigation.PushAsync(new PatientPage());
            await Navigation.PopAsync();

        }

        async void AddNewPatient()
        {
            Medicine medicine = new Medicine();
            Disease disease = new Disease();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataMedicine = db.Table<Medicine>().ToList();
            string medicineNameEntry = EntryMedicineName.Items[EntryMedicineName.SelectedIndex];
            var dataDisease = db.Table<Disease>().ToList();
            string diseaseNameEntry = EntryDiseaseName.Items[EntryDiseaseName.SelectedIndex];

            var data = db.Table<Medicine>().Where(u => u.MedicineName == medicineNameEntry).FirstOrDefault();
            var data1 = db.Table<Disease>().Where(u => u.DiseaseName == diseaseNameEntry).FirstOrDefault();
            int medicineID = data.MedicineID;
            int diseaseID = data1.DiseaseID;
            await App.MyDatabase.CreatePatient(new Model.Patient
            {

                FirstName = EntryFirstName.Text,
                LastName = EntryLastName.Text,
                Personnummer = EntryPersonnummer.Text,
                Adress = EntryAdress.Text,
                PhoneNumber = EntryPhoneNumber.Text,
                Email = EntryEmail.Text,
                Description = EntryDescription.Text,
                Medicines = dataMedicine,
                Diseases = dataDisease,

            }); ;

            await Navigation.PushAsync(new PatientPage());

            await Navigation.PopAsync();
    }

        async void EditPatient()
        {
            _patient.FirstName = EntryFirstName.Text;
            _patient.LastName = EntryLastName.Text;
            _patient.Personnummer = EntryPersonnummer.Text;
            _patient.Adress = EntryAdress.Text;
            _patient.PhoneNumber = EntryPhoneNumber.Text;
            _patient.Email = EntryEmail.Text;
            _patient.Description = EntryDescription.Text;
            await App.MyDatabase.UpdatePatient(_patient);
            await Navigation.PopAsync();

        }
        
    }
}
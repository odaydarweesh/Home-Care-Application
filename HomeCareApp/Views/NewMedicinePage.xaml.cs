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
    public partial class NewMedicinePage : ContentPage
    {
        //private int idPatient;
        
        public NewMedicinePage()
        {
            InitializeComponent();
        }
        Medicine _medicine;
        
        public NewMedicinePage(Medicine medicine)
        {
            InitializeComponent();

            Title = "Edit Information";
            _medicine = medicine;
            EntryMedicineName.Text = medicine.MedicineName;
            EntryDescriptionName.Text = medicine.DescriptionName;
            EntryMedicineName.Focus();
        }


        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryMedicineName.Text) || string.IsNullOrWhiteSpace(EntryDescriptionName.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
            else if (_medicine != null)
            {
                EditMedicine();
            }
            else
            {
                AddNewMedicine();

            }

        }
        private async void SelectedPatient(object sender, EventArgs e)
        {
            var items = await App.MyDatabase.ReadPatients();
            EntryPatientFirstName.ItemsSource = (System.Collections.IList)items;

        }
        private async void SelectedUser(object sender, EventArgs e)
        {
            var items = await App.MyDatabase.ReadUsers();
            EntryUserFirstName.ItemsSource = (System.Collections.IList)items;

        }


        async void AddNewMedicine()
        {
            User user = new User();
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
            string patientFirstNameEntry = EntryPatientFirstName.Items[EntryPatientFirstName.SelectedIndex];
            var dataUser = db.Table<User>().ToList();
            string userFirstNameEntry = EntryUserFirstName.Items[EntryUserFirstName.SelectedIndex];

            var data = db.Table<Patient>().Where(u => u.FirstName == patientFirstNameEntry).FirstOrDefault();
            var data1 = db.Table<User>().Where(u => u.FirstName == userFirstNameEntry).FirstOrDefault();
            int idPatient = data.IdPatient;
            Guid userId = data1.UserId;


            await App.MyDatabase.CreateMedicine(new Model.Medicine
            {

            MedicineName = EntryMedicineName.Text,
            DescriptionName = EntryDescriptionName.Text,
            Time = EntryTime.Time.ToString(),
            IdPatient = idPatient,
            UserId = userId,                      

            });
            
            await Navigation.PushAsync(new MedicinePage());
        }

        async void EditMedicine()
        {
            User user = new User();
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
            string patientFirstNameEntry = EntryPatientFirstName.Items[EntryPatientFirstName.SelectedIndex];
            var dataUser = db.Table<User>().ToList();
            string userFirstNameEntry = EntryUserFirstName.Items[EntryUserFirstName.SelectedIndex];
            var data = db.Table<Patient>().Where(u => u.FirstName == patientFirstNameEntry).FirstOrDefault();
            var data1 = db.Table<User>().Where(u => u.FirstName == userFirstNameEntry).FirstOrDefault();
            int idPatient = data.IdPatient;
            Guid userId = data1.UserId;

            _medicine.MedicineName = EntryMedicineName.Text;
            _medicine.DescriptionName = EntryDescriptionName.Text;
            _medicine.Time = EntryTime.Time.ToString();
            _medicine.IdPatient = idPatient;
            _medicine.UserId = userId;
            await App.MyDatabase.UpdateMedicine(_medicine);
            await Navigation.PopAsync();
        }
    }
}
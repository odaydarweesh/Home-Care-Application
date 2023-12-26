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
    public partial class NewEffortPage : ContentPage
    {
        public NewEffortPage()
        {
            InitializeComponent();
        }
        Effort _effort;
        Patient _patient;
        Disease _disease;
        public NewEffortPage(Effort effort)
        {
            InitializeComponent();

            Title = "Edit Information";
            _effort = effort;
            EntryEffortName.Text = effort.EffortName;
            EntryDescription.Text = effort.Description;
            EntryEffortName.Focus();
        }


        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryEffortName.Text) || string.IsNullOrWhiteSpace(EntryDescription.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
            
            else
            {
                AddNewEffort();
                
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

        async void AddNewEffort()
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


            await App.MyDatabase.CreateEffort(new Model.Effort
            {

                EffortName = EntryEffortName.Text,
                Description = EntryDescription.Text,
                idPatient = idPatient,
                UserId = userId,

            });
           
            await Navigation.PushAsync(new EffortPage());
        }

        async void EditEffort()
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

            _effort.EffortName = EntryEffortName.Text;
            _effort.Description = EntryDescription.Text;
            _effort.idPatient = idPatient;
            _effort.UserId = userId;
            await App.MyDatabase.UpdateEffort(_effort);
            await Navigation.PopAsync();
        }
    }
}
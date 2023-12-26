using HomeCareApp.Model;
using HomeCareApp.Services.Database;
using HomeCareApp.ViewModel;
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
    public partial class NewVisitPage : ContentPage
    {
        private int medicineId;
        private int idPatient;
        private Guid userId;
        private int effortId;

        private Database _context;

        User User { get; set; } = new User();
        Medicine Medicine { get; set; } = new Medicine();
        Effort Effort { get; set; } = new Effort();
        Patient Patient { get; set; } = new Patient();


        public NewVisitPage()
        {
            InitializeComponent();
            
        }
        Visit _visit;
        User _user;
        Patient _patient;
        Effort _effort;
        Medicine _medicine;
        public NewVisitPage(Visit visit)
        {

            InitializeComponent();
            Title = "Edit Information";
            _visit = visit;
            var visitType = VisitType();
            EntryVisitName.Focus();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(EntryVisitName.ToString()))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
            else if (_visit != null)
            {
                EditVisit();
            }
            else
            {
                AddNewVisit();
            }
                await Navigation.PopAsync();
        }
        
        private async void SelectedUser(object sender, EventArgs e)
        {
            var items = await App.MyDatabase.ReadUsers();
            EntryUserFirstName.ItemsSource = (System.Collections.IList)items;

        }

        private async void SelectedPatient(object sender, EventArgs e)
        {
            var items = await App.MyDatabase.ReadPatients();
            EntryPatientFirstName.ItemsSource = (System.Collections.IList)items;

        }
        
        private IList<Visit> VisitType()
        {

            return new List<Visit>
                {
                    new Visit { VisitType = "Morgon Besök"},
                    new Visit { VisitType = "Förmidag Besök"},
                    new Visit { VisitType = "Lunck Besök"},
                    new Visit { VisitType = "Eftermidag Besök"},
                    new Visit { VisitType = "Kvälls Besök1"},
                    new Visit { VisitType = "Kvälls Besök2"},
                    new Visit { VisitType = "Natt Besök"},
                 
                };
        }

     async void AddNewVisit()
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

            await App.MyDatabase.CreateVisit(new Model.Visit
            {

            StartTime = EntryStartTime.Time.ToString(),
            EndTime = EntryEndTime.Time.ToString(),
            VisitName = (string)EntryVisitName.SelectedItem,
            idPatient = idPatient,
            UserId = userId,
        });
            await Navigation.PushAsync(new VisitPage());
        }


        async void EditVisit()
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

            _visit.StartTime = EntryStartTime.Time.ToString();
            _visit.EndTime = EntryEndTime.Time.ToString();
            _visit.VisitName = (string)EntryVisitName.SelectedItem;
            _visit.idPatient = idPatient;
            _visit.UserId = userId;
           
            await App.MyDatabase.UpdateVisit(_visit);
            await Navigation.PopAsync();

        }
       
    }
}
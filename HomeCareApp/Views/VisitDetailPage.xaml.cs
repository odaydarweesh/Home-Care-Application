using HomeCareApp.Model;
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
    public partial class VisitDetailPage : ContentPage
    {
        int _signVisit;

        private int _IDPatient;
        int _idVisit;
        Visit _visit;
        public ListView ListView;
        public ListView ListView1;
        public static int _idPatient { get; set; }


        public VisitDetailPage(Visit visitdetails)// En konstruktör har en parameter visitdetails typ objekt Visit som visar Visitinformation
                                                  // såsom visitName, startTime, endTime, patienFirstName, patienLastName,  userFirstName
        {

            var visitName = visitdetails.VisitName;
            var startTime = visitdetails.StartTime;
            var endTime = visitdetails.EndTime;
            int idVisit = visitdetails.VisitID;
            int signVisit = visitdetails.Signed;


            int idPatient = visitdetails.idPatient;
            var userId = visitdetails.UserId;

            User user = new User();
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
            var dataUser = db.Table<User>().ToList();

            var data = db.Table<Patient>().Where(u => u.IdPatient == idPatient).FirstOrDefault();
            var data1 = db.Table<User>().Where(u => u.UserId == userId).FirstOrDefault();

            string patienFirstName = data.FirstName;
            string patienLastName = data.LastName;
            string userFirstName = data1.FirstName;

            InitializeComponent();
           
            _IDPatient = idPatient;
            _idVisit = idVisit;
            _visit = visitdetails;
            _signVisit = signVisit;
            VisitNameShow.Text = visitName;
            StartTimeShow.Text = startTime;
            EndTimeShow.Text = endTime;
            PatientNameShow.Text = patienFirstName;
            PatientLastNameShow.Text = patienLastName;
            UserFirstNameShow.Text = userFirstName;
            string signedVisit = "Signed";
            string unSignedVisit = "UnSigned";
            if (signVisit == 1)
            {


                SignVisitShow.Text = signedVisit;

            }
            else
            {
                SignVisitShow.Text = unSignedVisit;

            }

        }


        async void OnToggled(object sender, ToggledEventArgs e)// OnToggled-metod som låter oss signera Visit. Om mySwitch1.IsToggled ger metoden värdet 1 till variabeln _signVisit
                                                               // och nästa metod exekveras som är SignVisit() som uppdaterar värdet inuti databasen från 0 till 1
                                                               // vilket betyder att Visit har signerats

        {
            Switch mySwitch1 = (Switch)sender;
            if (mySwitch1.IsToggled)
            {
                Console.WriteLine("Toggled on");
                if (!(sender is Switch s)) return;

                s.Toggled += OnToggled;
                _signVisit = 1;
                SignVisit();

            }

            else
            {
                Console.WriteLine("Toggled off");
                _signVisit = 0;
                UnSignVisit();

            }
        }

        private async void SignMedicine(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MedicinePage(_IDPatient));

        }


        async void SignVisit()// uppdaterar värdet inuti databasen från 0 till 1
                                 // vilket betyder att Visit har signerats

        {
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataVisit = db.Table<Visit>().ToList();
            var data = db.Table<Visit>().Where(u => u.VisitID == _idVisit).FirstOrDefault();
            int idPatient = data.idPatient;
            _visit = data;
            _visit.Signed = 1;
            await App.MyDatabase.UpdateVisit(_visit);

        }

        async void UnSignVisit()//uppdaterar värdet inuti databasen från 1 till 0
                                   // vilket betyder att Visit har osignerats

        {
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataVisit = db.Table<Visit>().ToList();
            var data = db.Table<Visit>().Where(u => u.VisitID == _idVisit).FirstOrDefault();
            int idPatient = data.idPatient;
            _visit = data;
            _visit.Signed = 0;
            await App.MyDatabase.UpdateVisit(_visit);
        }


        private async void SeAllPatientsDetails(object sender, EventArgs e)// // Metod som visar alla detalier för den här patienten
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var patientdetails = db.Table<Patient>().Where(u => u.IdPatient == _IDPatient).FirstOrDefault();
            
            await Navigation.PushAsync(new PatientDetailPage (patientdetails));

        }
        private async void SignEffort(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EffortPage(_IDPatient));

        }

       
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        public VisitDetailPage(List<Visit> visits)
        {
        }

        public VisitDetailPage(string visitName, string startTime, string endTime)
        {
        }
    }
}
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
            InitializeComponent();

            var visitName = visitdetails.VisitName;
            var startTime = visitdetails.StartTime;
            var endTime = visitdetails.EndTime;
            int idVisit = visitdetails.VisitID;
            int signVisit = visitdetails.Signed;

            int idPatient = visitdetails.idPatient;
            var userId = visitdetails.UserId;
           
            _IDPatient = idPatient;
            _idVisit = idVisit;

            // Load data asynchronously after initialization
            Device.BeginInvokeOnMainThread(async () => {
                await LoadVisitDetails(idPatient, userId);
            });
            _visit = visitdetails;
            _signVisit = signVisit;
            VisitNameShow.Text = visitName;
            StartTimeShow.Text = startTime;
            EndTimeShow.Text = endTime;
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
            // Use centralized database service instead of creating new connection
            var visits = await App.MyDatabase.ReadVisits();
            var data = visits.Where(u => u.VisitID == _idVisit).FirstOrDefault();
            if (data != null)
            {
                _visit = data;
                _visit.Signed = 1;
                await App.MyDatabase.UpdateVisit(_visit);
            }
        }

        async void UnSignVisit()//uppdaterar värdet inuti databasen från 1 till 0
                                   // vilket betyder att Visit har osignerats

        {
            // Use centralized database service instead of creating new connection
            var visits = await App.MyDatabase.ReadVisits();
            var data = visits.Where(u => u.VisitID == _idVisit).FirstOrDefault();
            if (data != null)
            {
                _visit = data;
                _visit.Signed = 0;
                await App.MyDatabase.UpdateVisit(_visit);
            }
        }


        private async void SeAllPatientsDetails(object sender, EventArgs e)// // Metod som visar alla detalier för den här patienten
        {
            // Use centralized database service instead of creating new connection
            var patients = await App.MyDatabase.ReadPatients();
            var patientdetails = patients.Where(u => u.IdPatient == _IDPatient).FirstOrDefault();
            
            if (patientdetails != null)
            {
                await Navigation.PushAsync(new PatientDetailPage(patientdetails));
            }
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

        private async Task LoadVisitDetails(int idPatient, Guid userId)
        {
            try
            {
                // Use the centralized database service instead of creating new connections
                var patients = await App.MyDatabase.ReadPatients();
                var users = await App.MyDatabase.ReadUsers();

                var patient = patients.Where(u => u.IdPatient == idPatient).FirstOrDefault();
                var user = users.Where(u => u.UserId == userId).FirstOrDefault();

                if (patient != null && user != null)
                {
                    PatientNameShow.Text = patient.FirstName;
                    PatientLastNameShow.Text = patient.LastName;
                    UserFirstNameShow.Text = user.FirstName;
                }
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                await DisplayAlert("Error", "Failed to load visit details", "OK");
            }
        }
    }
}
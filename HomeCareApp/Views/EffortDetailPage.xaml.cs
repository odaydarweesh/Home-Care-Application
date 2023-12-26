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
    public partial class EffortDetailPage : ContentPage
    {
        int _signEffort;
        int _idEffort;
        Effort _effort;
        public EffortDetailPage(Effort effortdetails)  // En konstruktör har en parameter effortdetails typ objekt Effort som visar effortinformation
                                                       // såsom effortName och dess beskrivning och namnet på patienten som drabbats av sjukdomenm, userID
        {
            var effortName = effortdetails.EffortName;
            var description = effortdetails.Description;
            var idPatient = effortdetails.idPatient;
            var userId = effortdetails.UserId;
            int signEffort = effortdetails.Signed;
            int idEffort = effortdetails.EffortID;

            User user = new User();
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
            var dataUser = db.Table<User>().ToList();

            var data = db.Table<Patient>().Where(u => u.IdPatient == idPatient).FirstOrDefault();
            var data1 = db.Table<User>().Where(u => u.UserId == userId).FirstOrDefault();

            string patienFirstName = data.FirstName;
            string userID = data1.FirstName;

            InitializeComponent();
            EffortNameShow.Text = effortName;
            DescriptionShow.Text = description;
            PatientFirstNameShow.Text = patienFirstName;
            UserFirstNameShow.Text = userID;
            _signEffort = signEffort;
            _idEffort = idEffort;
            _effort = effortdetails;
            string signedEffort = "Signed";
            string unSignedEffort = "UnSigned";
            if (signEffort == 1)
            {


                SignEffortShow.Text = signedEffort;
               
            }
            else
            {
                SignEffortShow.Text = unSignedEffort;

            }
        }

        async void OnToggled(object sender, ToggledEventArgs e)// OnToggled-metod som låter oss signera Effort. Om mySwitch1.IsToggled ger metoden värdet 1 till variabeln _signEffort
                                                               // och nästa metod exekveras som är SignEffort() som uppdaterar värdet inuti databasen från 0 till 1
                                                               // vilket betyder att Effort har signerats 
        {
            Switch mySwitch1 = (Switch)sender;
            if (mySwitch1.IsToggled)
            {
                Console.WriteLine("Toggled on");
                if (!(sender is Switch s)) return;

                s.Toggled += OnToggled;
                _signEffort = 1;
                SignEffort();
            }

            else
            {
                Console.WriteLine("Toggled off");
                _signEffort = 0;
                UnSignEffort();
            }
        }
        async void SignEffort()// uppdaterar värdet inuti databasen från 0 till 1
                               // vilket betyder att Effort har signerats 
        {
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataEffort = db.Table<Effort>().ToList();
            var data = db.Table<Effort>().Where(u => u.EffortID == _idEffort).FirstOrDefault();
            int idPatient = data.idPatient;
            _effort = data;
            _effort.Signed = 1;
            await App.MyDatabase.UpdateEffort(_effort);
        }

        async void UnSignEffort()//uppdaterar värdet inuti databasen från 1 till 0
                                 // vilket betyder att Effort har osignerats
        {
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataEffort = db.Table<Effort>().ToList();
            var data = db.Table<Effort>().Where(u => u.EffortID == _idEffort).FirstOrDefault();
            int idPatient = data.idPatient;
            _effort = data;
            _effort.Signed = 0;
            await App.MyDatabase.UpdateEffort(_effort);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        public EffortDetailPage(List<Effort> efforts)
        {
        }

        public EffortDetailPage(string effortName, string description)
        {
        }
    }
}
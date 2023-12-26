using HomeCareApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicineDetailPage : ContentPage
    {
        Switch switchControl = new Switch { IsToggled = true };

        int _signMedicine;
        int _idMedicine;
        Medicine _medicine;

        public MedicineDetailPage(Medicine medicinedetails)// En konstruktör har en parameter medicinedetails typ objekt Medicine som visar medicineinformation
                                                           // såsom medicineName och dess beskrivning och patienFirstName som drabbats av sjukdomenm, userID

        {

            var medicineName = medicinedetails.MedicineName;
            var descriptionName = medicinedetails.DescriptionName;
            var time = medicinedetails.Time;
            int signMedicine = medicinedetails.Signed;
            int idPatient = medicinedetails.IdPatient;
            int idMedicine = medicinedetails.MedicineID;

            var userId = medicinedetails.UserId;
            
            User user = new User();
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
            var dataUser = db.Table<User>().ToList();
          
            var data = db.Table<Patient>().Where(u => u.IdPatient == idPatient).FirstOrDefault();
            var data1 = db.Table<User>().Where(u => u.UserId == userId).FirstOrDefault();

            string patienFirstName = data.FirstName;
            string userFirstName = data1.FirstName;

            InitializeComponent();
            MedicineNameShow.Text = medicineName;
            TimeShow.Text = time;
             DescriptionNameShow.Text = descriptionName;
            PatientNameShow.Text = patienFirstName;
            UserFirstNameShow.Text = userFirstName;
            _signMedicine = signMedicine;
            _idMedicine = idMedicine;
            //bool signMedicine ;
            _medicine = medicinedetails;
            string signedMedicine = "Signed";
            string unSignedMedicine = "UnSigned";
            if (signMedicine == 1)
            {
                

                SignMedicineShow.Text = signedMedicine;
               
            }
            else
            {
                SignMedicineShow.Text = unSignedMedicine;

            }

        }

        async void OnToggled(object sender, ToggledEventArgs e)// OnToggled-metod som låter oss signera Medicine. Om mySwitch1.IsToggled ger metoden värdet 1 till variabeln _signMedicine
                                                               // och nästa metod exekveras som är SignMedicine() som uppdaterar värdet inuti databasen från 0 till 1
                                                               // vilket betyder att Medicine har signerats

        {
            Switch mySwitch1 = (Switch)sender;
            if (mySwitch1.IsToggled)
            {
                Console.WriteLine("Toggled on");
                if (!(sender is Switch s)) return;

                s.Toggled += OnToggled;
                _signMedicine = 1;
                SignMedicine();

            }

            else
            {
                Console.WriteLine("Toggled off");
                _signMedicine = 0;
                UnSignMedicine();

            }
        }


        async void SignMedicine()// uppdaterar värdet inuti databasen från 0 till 1
                                 // vilket betyder att Medicine har signerats

        {
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataMedicine = db.Table<Medicine>().ToList();
            var data = db.Table<Medicine>().Where(u => u.MedicineID == _idMedicine).FirstOrDefault();
            int idPatient = data.IdPatient;
            _medicine = data;
            _medicine.Signed = 1;
            await App.MyDatabase.UpdateMedicine(_medicine);

        }

        async void UnSignMedicine()//uppdaterar värdet inuti databasen från 1 till 0
                                   // vilket betyder att Medicine har osignerats

        {
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataMedicine = db.Table<Medicine>().ToList();
            var data = db.Table<Medicine>().Where(u => u.MedicineID == _idMedicine).FirstOrDefault();
            int idPatient = data.IdPatient;
            _medicine = data;
            _medicine.Signed = 0;
            await App.MyDatabase.UpdateMedicine(_medicine);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        public MedicineDetailPage(List<Medicine> medicines)
        {
        }

        public MedicineDetailPage(string medicineName, string descriptionName)
        {
        }
    }
}
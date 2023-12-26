using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCareApp.Model;
using HomeCareApp.ViewModel;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicinePage : ContentPage
    {
        public static readonly ObservableCollection<Medicine> CurrentSelectedFile = new ObservableCollection<Medicine>();

        public ListView ListView;
        Medicine _medicine = new Medicine();
        int _IdPatient;
        private Guid idUser;
        public MedicinePage()//I den här klassen har vi fyra olika konstruktörer.
                             //Den första har ingen parameter, den andra har _idUser-parameter typen guid,
                             //den tredje har idPatient-parameter typen int.
                             //den fjärde har medicinedetails-parameter typen objekt Medicine.
                             //Var och en av dem visar olika data beroende på vilken klass den användes i.
        {
            BindingContext = this;
            idUser = App.UserId;
            InitializeComponent();
            ListView = MedicineList;
        }
        public MedicinePage(int idPatient)
        {
            _IdPatient = idPatient;
            BindingContext = this;

            InitializeComponent();
             _IdPatient = idPatient;

            ListView = MedicineList;
        }

        public MedicinePage(Guid _idUser)
        {
            BindingContext = this;
            idUser = _idUser;

            InitializeComponent();

            ListView = MedicineList;
        }

        public MedicinePage(Medicine medicinedetails)
        {
            var medicineName = medicinedetails.MedicineName;
            var descriptionName = medicinedetails.DescriptionName;
            var time = medicinedetails.Time;
            int idPatient = medicinedetails.IdPatient;
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();
         
            var data = db.Table<Patient>().Where(u => u.IdPatient == _IdPatient).FirstOrDefault();
            string patienFirstName = data.FirstName;
            InitializeComponent();
            
            _IdPatient = idPatient;

            ListView = MedicineList;
        }
      
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_IdPatient == 0)
            {
                MedicineList.ItemsSource = await App.MyDatabase.ReadMedicinesWithIdUser(idUser);
            }
            else
            {
                MedicineList.ItemsSource = await App.MyDatabase.ReadMedicinesWithIdPatient(_IdPatient);

            }
        }


       
        async void Button_OnClicked(object sender, EventArgs e)
        {
            if (App.UserRole == "Nurse")
            {
                await Navigation.PushAsync(new NewMedicinePage());

            }
            else
            {
                await this.DisplayAlert("Error", "You do not have permission to do this action. Only the nurse can add a new medicine.", "Yes", "Cancle");
            }

        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
           var medicinedetails = e.Item as Medicine;
           await Navigation.PushAsync(new MedicineDetailPage(medicinedetails));

        }
        async void OnMore_Edit(object sender, EventArgs e)
        {
            if (App.UserRole == "Nurse")//Vi använde if statement för att säkerställa att bara sjuksköterskan kan updatera medicinen.
            {
                var item = sender as MenuItem;
            var medici = item.CommandParameter as Medicine;
            await Navigation.PushAsync(new NewMedicinePage(medici));
            }
            else
            {
                await this.DisplayAlert("Error", "You do not have permission to do this action. Only the nurse can edit medicine.", "Yes", "Cancle");
            }
        }

        async void OnDelete(object sender, EventArgs e)// Vi använder async metoder för att hantera asynchronous executions.
        {
            if (App.UserRole == "Nurse")// Vi använde if statement för att säkerställa att bara sjuksköterskan kan ta bort medicinen.
            {
                var item = sender as MenuItem;
            var medicine = item.CommandParameter as Medicine;
            var result = await DisplayAlert("Delete", $"Delete { medicine.MedicineName}  from the database", "Yes", "No");//Vi använder await-operator till Task  i en metod markerad som async.

                if (result)
            {
                await App.MyDatabase.DeleteMedicine(medicine);// 
                MedicineList.ItemsSource = await App.MyDatabase.ReadMedicines();
            }
            }
            else
            {
                await this.DisplayAlert("Error", "You do not have permission to do this action. Only the nurse can delete medicine.", "Yes", "Cancle");
            }

        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)//söka efter en specifik medicine
        {
            MedicineList.ItemsSource = await App.MyDatabase.SearchMedicine(e.NewTextValue);
        }
    }
}
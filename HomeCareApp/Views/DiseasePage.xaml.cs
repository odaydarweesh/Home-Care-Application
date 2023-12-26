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
    public partial class DiseasePage : ContentPage
    {
        private Guid idUser;
        public static readonly ObservableCollection<Disease> CurrentSelectedFile = new ObservableCollection<Disease>();
        int _IdPatient;
        public ListView ListView;
        public DiseasePage()//I den här klassen har vi fyra olika konstruktörer.
                            //Den första har ingen parameter, den andra har _idUser-parameter typen guid,
                            //den tredje har idPatient-parameter typen int.
                            //den fjärde har diseasedetails-parameter typen objekt Disease.
                            //Var och en av dem visar olika data beroende på vilken klass den användes i.
        {
            BindingContext = this;
            idUser = App.UserId;
            InitializeComponent();
            ListView = DiseaseList;
        }
        public DiseasePage(int idPatient)
        {
            _IdPatient = idPatient;
            BindingContext = this;
            //idUser = App.UserId;

            InitializeComponent();
            _IdPatient = idPatient;
            ListView = DiseaseList;
        }
        public DiseasePage(Guid _idUser)
        {
            BindingContext = this;
            idUser = _idUser;

            InitializeComponent();

            ListView = DiseaseList;
        }
        public DiseasePage(Disease diseasedetails)
        {
            var diseaseName = diseasedetails.DiseaseName;
            var descriptionName = diseasedetails.DiseaseDescription;
            //var time = diseasedetails.Time;
            int idPatient = diseasedetails.IdPatient;
            Patient patient = new Patient();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var dataPatient = db.Table<Patient>().ToList();

            var data = db.Table<Patient>().Where(u => u.IdPatient == _IdPatient).FirstOrDefault();
            string patienFirstName = data.FirstName;
            InitializeComponent();

            _IdPatient = idPatient;

            ListView = DiseaseList;
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();                      
                DiseaseList.ItemsSource = await App.MyDatabase.ReadDiseasesWithIdPatient(_IdPatient);
        }



        async void Button_OnClicked(object sender, EventArgs e)
        {
            if (App.UserRole == "Nurse")
            {
                await Navigation.PushAsync(new NewDiseasePage());

            }
            else
            {
                await this.DisplayAlert("Error", "You do not have permission to do this action. Only the nurse can add or edit a new disease.", "Yes", "Cancle");
            }

        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var diseasedetails = e.Item as Disease;
            await Navigation.PushAsync(new DiseaseDetailPage(diseasedetails));////Vi använder await-operator till Task  i en metod markerad som async.

        }
        async void OnMore_Edit(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var dise = item.CommandParameter as Disease;
            await Navigation.PushAsync(new NewDiseasePage(dise));////Vi använder await-operator till Task  i en metod markerad som async.
        }

        async void OnDelete(object sender, EventArgs e)//// Vi använder async metoder för att hantera asynchronous executions.
        {
            var item = sender as MenuItem;
            var dise = item.CommandParameter as Disease;
            var result = await DisplayAlert("Delete", $"Delete { dise.DiseaseName}  from the database", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteDisease(dise);
                DiseaseList.ItemsSource = await App.MyDatabase.ReadDiseases();
            }
        }



        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)// söka efter en specifik sjukdom
        {
            DiseaseList.ItemsSource = await App.MyDatabase.SearchDisease(e.NewTextValue);
        }
    }
}
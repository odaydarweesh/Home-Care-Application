using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCareApp.Model;
using HomeCareApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientPage : ContentPage
    {
        public static readonly ObservableCollection<Patient> CurrentSelectedFile = new ObservableCollection<Patient>();
        public ListView ListView;

        public PatientPage()
        {            
            BindingContext = this;

            InitializeComponent();
            ListView = PatientList;

        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                PatientList.ItemsSource = await App.MyDatabase.ReadPatients();

            }
            catch { }
        }


        async void Button_OnClicked(object sender, EventArgs e)
        {
            if (App.UserRole == "Manager")
            {
                await Navigation.PushAsync(new NewPatientPage());

            }
            else
            {
                await this.DisplayAlert("Error", "You do not have permission to do this action. Only the manager can add or edit a new patient.", "Yes", "Cancle");
            }

        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var patientdetails = e.Item as Patient;
            await Navigation.PushAsync(new PatientDetailPage(patientdetails));////Vi använder await-operator till Task  i en metod markerad som async.

        }
        async void OnMore_Edit(object sender, EventArgs e)//// Vi använder async metoder för att hantera asynchronous executions.
        {
            var item = sender as MenuItem;
            var patie = item.CommandParameter as Patient;
            await Navigation.PushAsync(new NewPatientPage(patie));
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var patie = item.CommandParameter as Patient;
            var result = await DisplayAlert("Delete", $"Delete { patie.FirstName}  from the database", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeletePatient(patie);////Vi använder await-operator till Task  i en metod markerad som async.
                PatientList.ItemsSource = await App.MyDatabase.ReadPatients();
            }
        }


        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)//söka efter en specifik patient
        {
            PatientList.ItemsSource = await App.MyDatabase.SearchPatient(e.NewTextValue);
        }
    }
}
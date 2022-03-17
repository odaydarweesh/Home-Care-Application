using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCareApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientPageDetails : ContentPage
    {
        public PatientPageDetails()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                myPatient.ItemsSource = await App.MyDatabase.ReadPatients();
            }
            catch { }
        }
        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PatientDetail());

        }
       async void SwipeItem_Invoke_Edit(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var pati = item.CommandParameter as Patient;
            await Navigation.PushAsync(new PatientDetail(pati));
        }
        async void SwipeItem_Invoke_Delete(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var pati = item.CommandParameter as Patient;
            var result = await DisplayAlert("Delete", $"Delete { pati.FirstName}  from the database", "Yes", "No");
            if(result)
            {
                await App.MyDatabase.DeletePatient(pati);
                myPatient.ItemsSource= await App.MyDatabase.ReadPatients();
            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            myPatient.ItemsSource = await App.MyDatabase.Search(e.NewTextValue);
        }
    }
}
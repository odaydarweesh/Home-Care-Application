using HomeCareApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitPageDetails : ContentPage
    {
        public VisitPageDetails()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                myVisits.ItemsSource = await App.MyDatabase.ReadVisits();
            }
            catch { }
        }
        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VisitDetail());

        }
        async void SwipeItem_Invoke_Edit(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var visi = item.CommandParameter as Visit;
            await Navigation.PushAsync(new VisitDetail(visi));
        }
        async void SwipeItem_Invoke_Delete(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var visi = item.CommandParameter as Visit;
            var result = await DisplayAlert("Delete", $"Delete { visi.StartTime}  from the database", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteVisit(visi);
                myVisits.ItemsSource = await App.MyDatabase.ReadVisits();
            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            myVisits.ItemsSource = await App.MyDatabase.SearchVisit(e.NewTextValue);
        }
    }
}
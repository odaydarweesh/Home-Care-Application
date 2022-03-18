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
    public partial class UserPageDetails : ContentPage
    {
        public UserPageDetails()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                myUser.ItemsSource = await App.MyDatabase.ReadUsers();
            }
            catch { }
        }
        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDetail());

        }
        async void SwipeItem_Invoke_Edit(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var use = item.CommandParameter as User;
            await Navigation.PushAsync(new UserDetail(use));
        }
        async void SwipeItem_Invoke_Delete(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var use = item.CommandParameter as User;
            var result = await DisplayAlert("Delete", $"Delete { use.FirstName}  from the database", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteUser(use);
                myUser.ItemsSource = await App.MyDatabase.ReadUsers();
            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            myUser.ItemsSource = await App.MyDatabase.SearchUser(e.NewTextValue);
        }
    }
}
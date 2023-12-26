using HomeCareApp.Model;
using HomeCareApp.Services.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private Guid idUser;
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public static readonly ObservableCollection<User> CurrentSelectedFile = new ObservableCollection<User>();
        public ListView ListView;
        public UserPage()
        {
            InitializeComponent();
            ListView = UserList;
            BindingContext = this;
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                UserList.ItemsSource = await App.MyDatabase.ReadUsers();
                string firstName = App.FirstName;
                string lastNmae = App.LastName;

          
            }
            catch { }
        }
      
        async void AddUserOnClicked(object sender, EventArgs e)
        {
            if (App.UserRole == "Manager")
            {
                await Navigation.PushAsync(new NewUserPage());

            }
            else
            {
                await this.DisplayAlert("Error", "You do not have permission to do this action. Only the nurse can add or edit a new Employee.", "Yes", "Cancle");
            }

        }
        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var userdetails = e.Item as User;
            await Navigation.PushAsync(new UserDetailPage(userdetails));////Vi använder await-operator till Task  i en metod markerad som async.

        }
        async void OnMoreEdit(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var use = item.CommandParameter as User;
            await Navigation.PushAsync(new NewUserPage(use));////Vi använder await-operator till Task  i en metod markerad som async.
        }

        async void OnDelete(object sender, EventArgs e)//// Vi använder async metoder för att hantera asynchronous executions.
        {
            var item = sender as SwipeItem;
            var use = item.CommandParameter as User;
            var result = await DisplayAlert("Delete", $"Do you want to delete {use.FirstName} {use.LastName} from the Databasen?", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteUser(use);
                UserList.ItemsSource = await App.MyDatabase.ReadUsers();
            }
        }
       
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)//söka efter en specifik user eller medarbetare
        {
            UserList.ItemsSource = await App.MyDatabase.SearchUser(e.NewTextValue);
        }

        
    }
}
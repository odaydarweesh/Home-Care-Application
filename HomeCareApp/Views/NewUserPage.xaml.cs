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
    public partial class NewUserPage : ContentPage
    {
        public NewUserPage()
        {
            InitializeComponent();
        }
        User _user;

        public NewUserPage(User user)
        {
            InitializeComponent();
            Title = "Edit Information";
            _user = user;
            EntryFirstName.Text = user.FirstName;
            EntryLastName.Text = user.LastName;
            EntryFirstName.Focus();
            var roles = UserRoles();
            EntryUserName.Text = user.UserName;
            EntryUserPassword.Text = user.Password;
            EntryPersonnummer.Text = user.Personnummer;
            EntryAdress.Text = user.Adress;
            EntryUserPhoneNumber.Text = user.PhoneNumber;
            EntryUserEmaile.Text = user.Email;
        }

  
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryFirstName.Text) || string.IsNullOrWhiteSpace(EntryLastName.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
            else if (_user != null)
            {
                EditUser();
            }
            else
            {
                AddNewUser();
                await Navigation.PushAsync(new UserPage());

            }

        }
        private IList<User> UserRoles()
        {

            return new List<User>
                {
                    new User { UserRole = "Manager"},
                    new User { UserRole = "Nurse"},
                    new User { UserRole = "AssistantNurse"}
                };
        }

        async void AddNewUser()
        {
            
            string userRoleEntry = EntryUserRole.Items[EntryUserRole.SelectedIndex];
            await App.MyDatabase.CreateUser(new Model.User
            {
                UserRole = userRoleEntry,
                UserName = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                FirstName = EntryFirstName.Text,
                LastName = EntryLastName.Text,
                Personnummer = EntryPersonnummer.Text,
                Adress = EntryAdress.Text,
                PhoneNumber = EntryUserPhoneNumber.Text,
                Email = EntryUserEmaile.Text

            });
            await Navigation.PushAsync(new UserPage());
            await Navigation.PopAsync();
        }

        async void EditUser()
        {
            _user.UserRole = (string)EntryUserRole.SelectedItem;
            _user.UserName = EntryUserName.Text;
            _user.FirstName = EntryFirstName.Text;
            _user.LastName = EntryLastName.Text;
            _user.Personnummer = EntryPersonnummer.Text;
            _user.Adress = EntryAdress.Text;
            _user.PhoneNumber = EntryUserPhoneNumber.Text;
            _user.Email = EntryUserEmaile.Text;
            await App.MyDatabase.UpdateUser(_user);
            await Navigation.PopAsync();

        }
    }
}
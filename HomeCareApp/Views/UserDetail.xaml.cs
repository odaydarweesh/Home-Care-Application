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
    public partial class UserDetail : ContentPage
    {
        public UserDetail()
        {
            InitializeComponent();
        }
        User _user;

            public UserDetail(User user)
            {
                InitializeComponent();
                Title = "Edit Information";
                _user = user;
                EntryFirstName.Text = user.FirstName;
                EntryLastName.Text = user.LastName;
                EntryFirstName.Focus();
            }


            async void Handle_Clicked(object sender, EventArgs e)
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
                }

            }

            async void AddNewUser()
            {
                await App.MyDatabase.CreateUser(new Model.User
                {
                    FirstName = EntryFirstName.Text,
                    LastName = EntryLastName.Text,
                    Personnummer = EntryPersonnummer.Text,
                    Adress = EntryAdress.Text,
                    PhoneNumber = EntryPhoneNumber.Text,
                    Email = EntryEmail.Text

                });
            }

            async void EditUser()
            {
            _user.FirstName = EntryFirstName.Text;
            _user.LastName = EntryLastName.Text;
            _user.Personnummer = EntryPersonnummer.Text;
            _user.Adress = EntryAdress.Text;
            _user.PhoneNumber = EntryPhoneNumber.Text;
            _user.Email = EntryEmail.Text;
                await App.MyDatabase.UpdateUser(_user);
                await Navigation.PopAsync();

            }
        }
    }
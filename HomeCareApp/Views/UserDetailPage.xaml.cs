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
    public partial class UserDetailPage : ContentPage
    {
        Guid _idUser;
       
        public UserDetailPage(User userdetails)//// En konstruktör har en parameter userdetails typ objekt User som visar Userinformation
                                               // såsom firstName, lastName, email, personnummer, phoneNumber, adress, UserId 
        {
            var firstName = userdetails.FirstName;
            var lastName = userdetails.LastName;
            var email = userdetails.Email;
            var personnummer = userdetails.Personnummer;
            var phoneNumber = userdetails.PhoneNumber;
            var adress = userdetails.Adress;
            Guid idUser = userdetails.UserId;

            InitializeComponent();
            _idUser = idUser;
            UserFirstNameShow.Text = firstName;
            UserLastNameShow.Text = lastName;
            EmailShow.Text = email;
            PhoneNumberShow.Text = phoneNumber;
            AdressShow.Text = adress;
            if (App.UserRole == "Manager")// if satsement som visar personnummer för bara Manager
            {
                PersonnummerShow.Text = personnummer;
            }
            else
            {
                PersonnummerShow.Text = "*****";
            }
        }




        private async void SeAllEmployeesVisit(object sender, EventArgs e)// Metod som visar alla besök för den här medarbetare
        {
            await Navigation.PushAsync(new VisitPage(_idUser));
        }

        private async void SeAllEmployeesEffort(object sender, EventArgs e)// Metod som visar alla insatser för den här medarbetare
        {
            await Navigation.PushAsync(new EffortPage(_idUser));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
       
    }
}
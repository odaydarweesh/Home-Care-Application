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
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<User>();

            var item = new User()
            {
                UserName = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                FirstName = EntryFirstName.Text,
                LastName = EntryLaststName.Text,
                Personnummer = EntryPersonnummer.Text,
                Adress = EntryAdress.Text,
                Email = EntryUserEmaile.Text,
                PhoneNumber = EntryUserPhoneNumber.Text,


            };
            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Congratulation", "User Regidtration Sucessfull", "Yes", "Cancle");
                if (result)
                    await Navigation.PushAsync(new LoginPage());
        });
        }
    }
}
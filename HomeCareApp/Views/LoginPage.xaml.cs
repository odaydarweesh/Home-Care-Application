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
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
        }
        async  void Handle_Clicked_SignUp(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewUserPage());
        }

        async void Handle_Clicked_ForgotPassword(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new ForgotPasswordPage());

        }
        async void Handle_Clicked_Login(object sender, System.EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var user = db.Table<User>().Where(u => u.UserName.Equals(EntryUserName.Text)).FirstOrDefault();
            if (user != null && HomeCareApp.Services.Database.Database.VerifyPassword(EntryUserPassword.Text, user.Password))
            {
                App.UserId = user.UserId;
                App.UserName = user.UserName;
                App.UserEmail = user.Email;
                App.UserRole = user.UserRole;
                App.Personnummer = user.Personnummer;
                App.IsUserLoggedIn = true;
                App.Current.MainPage = new NavigationPage(new FlyoutPageMenu());


            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Failed User Name and Password", "Yes", "Cancle");
                    if (result)
                        await Navigation.PushAsync(new LoginPage());
                    else
                    {
                        await Navigation.PushAsync(new LoginPage());

                    }
                });
            }

        }
    }
}
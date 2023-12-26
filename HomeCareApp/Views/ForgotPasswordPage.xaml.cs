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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);

            InitializeComponent();
            string name = App.UserName;
            string userRole = App.UserRole;
            string email = App.UserEmail;


        }
        async void ButtonSendLink_Clicked(object sender, EventArgs e)

        {

            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<User>().Where(u => u.FirstName.Equals(EntryFirstName.Text) && u.Personnummer.Equals(EntryPersonnummer.Text)).FirstOrDefault();
            if (myquery != null)
            {
                App.UserId = myquery.UserId;
                App.UserName = myquery.UserName;
                App.UserEmail = myquery.Email;
                App.UserRole = myquery.UserRole;
                App.Personnummer = myquery.Personnummer;


                App.Current.MainPage = new NavigationPage(new ResetPasswordPage());


            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Failed FirstName or Personnummer", "Yes", "Cancle");
                    if (result)
                        await Navigation.PushAsync(new ForgotPasswordPage());
                    else
                    {
                        //await Navigation.PushAsync(new ForgotPasswordPage());

                    }
                });
            }

        }
    }
}
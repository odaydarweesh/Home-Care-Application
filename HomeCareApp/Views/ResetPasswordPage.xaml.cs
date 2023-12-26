using HomeCareApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        private string name = "Some User Name";
        private string email = "Some User Email";
        private string personnummer = "Some User personnummer";

        public ResetPasswordPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);

            InitializeComponent();
            this.BindingContext = this;
            TheUserName = GetCurrentUserName();
            TheUserEmail = GetCurrentUserEmail();
            TheUserPersonnummer = GetCurrentUserPersonnummer();
            string name = App.UserName;
            string userRole = App.UserRole;
            string personnummer = App.Personnummer;
        }
        public ICommand GoBackComman => new Command(async () => await Shell.Current.DisplayAlert("Back Button", "Back Button", "Ok"));
        async void ButtonChangPass_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (EntryNewPassword.Text.Equals(EntryConfirmPassword.Text))
                {

                    var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeCareDatabase.db3");
                    var db = new SQLiteConnection(dbpath);
                    var data = db.Table<User>();
                    var data1 = (from User in data where User.Personnummer == personnummer select User).Single();
                    data1.Password = EntryNewPassword.Text;
                    db.Update(data1);
                    var result = await this.DisplayAlert("Congratulation", "Password changed successfully", "Yes", "Cancle");
                    await Navigation.PushAsync(new LoginPage());

                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var result = await this.DisplayAlert("Error", "Passwords did not match", "Yes", "Cancle");
                        if (result)
                            await Navigation.PushAsync(new ResetPasswordPage());
                        else
                        {
                            //await Navigation.PushAsync(new LoginPage());

                        }
                    });
                }
            }
            catch (Exception ex)
            {

            }
            
        }



        private string GetCurrentUserName()
        {
            string name = App.UserName;

            return name;
        }
        private string GetCurrentUserEmail()
        {
            string email = App.UserEmail;

            return email;
        }
        private string GetCurrentUserPersonnummer()
        {
            string personnummer = App.Personnummer;

            return personnummer;
        }
        public string TheUserName
        {
            get
            {
                return name;

            }
            set
            {
                name = value;
                OnPropertyChanged("TheUserName");
            }
        }
        public string TheUserEmail
        {
            get
            {
                return email;

            }
            set
            {
                email = value;
                OnPropertyChanged("TheUserEmail");
            }
        }
        public string TheUserPersonnummer
        {
            get
            {
                return personnummer;

            }
            set
            {
                personnummer = value;
                OnPropertyChanged("TheUserPersonnummer");
            }
        }
        async void ResetPass(User _user)
        {

            _user.Password = EntryNewPassword.Text;
            await App.MyDatabase.UpdateUser(_user);
            await Navigation.PopAsync();

        }
    }


}

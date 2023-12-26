using HomeCareApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using HomeCareApp.Services.Database;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using HomeCareApp.Model;
using HomeCareApp.ViewModel;

namespace HomeCareApp
{
    public partial class App : Application
    {
        public static string UserName { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string UserEmail { get; set; }
        public static string UserRole { get; set; }
        public static string Personnummer { get; set; }
        public static bool Signed = true;
        public static bool IsUserLoggedIn { get; set; }

        private static Database db;
        public static Database MyDatabase
        {
            get 
            {              
                if(db == null)
                {
                    db = new Database();
                }
                return db; 
            }

        }
    
        public static Guid UserId { get; internal set; }

        public App()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

            InitializeComponent();


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

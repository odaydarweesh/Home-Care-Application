using HomeCareApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using HomeCareApp.Services.Database;

namespace HomeCareApp
{
    public partial class App : Application
    {
        public static string UserName { get; set; }
        public static bool IsUserLoggedIn { get; set; }

        private static Database db;
        public static Database MyDatabase
        {
            get 
            {              
                if(db == null)
                {
                    db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MyStore.db3"));
                }
                return db; 
            }

        }

        public App()
        {
            //MainPage = new NavigationPage(new LoginPage());
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new FlyoutPageMenu());
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

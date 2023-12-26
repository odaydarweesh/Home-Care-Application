using HomeCareApp.Model;
using HomeCareApp.ViewModel;
using HomeCareApp.ViewModels;
using HomeCareApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPageMenuFlyout : ContentPage
    {
        private string name = "Some User Name";
        private string email = "Some User Email";
        private string userRole = "Some UserRole";

        public ListView ListView;
        public ListView ListView1;

        public FlyoutPageMenuFlyout()
        {
            BindingContext = this;
            TheUserName = GetCurrentUserName();//Dessa tre metoder placeras i konstruktor 
            TheUserEmail = GetCurrentUserEmail(); //så att användarnamn, email och roll visas 
            TheUserRole = GetCurrentUserRole();//varje gång någon användare loggar in.

            InitializeComponent();
            list1.BindingContext = new FlyoutPageMenuFlyoutViewModel();

            ListView = MenuItemsListView;
            ListView1 = list1;


        }
        private string GetCurrentUserName()// Detta är en metod som ger användarnamnet på den användare som har loggat in 
        {
            string name = App.UserName;

            return name;
        }
        private string GetCurrentUserEmail()//Detta är en metod som ger email på den användare som har loggat in
        {
            string email = App.UserEmail;

            return email;
        }
        private string GetCurrentUserRole()// Detta är en metod som ger userRole på den användare som har loggat in
        {
            string userRole = App.UserRole;

            return userRole;
        }
        public string TheUserName
        {
            get
            {
                return "Hello " + name;

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
        public string TheUserRole
        {
            get
            {
                return userRole;

            }
            set
            {
                userRole = value;
                OnPropertyChanged("TheUserRole");
            }
        }


    }
}
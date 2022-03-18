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
        public ListView ListView;

        public FlyoutPageMenuFlyout()
        {

            InitializeComponent();
            string name = App.UserName;

            BindingContext = new FlyoutPageMenuFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        //public string TheUserName
        //{
        //    get
        //    {
        //        return "Hello " + name;
        //    }
        //    set
        //    {
        //        name = value;
        //        OnPropertyChanged("TheUserName");
        //    }
        //}



        private class FlyoutPageMenuFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutPageMenuFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutPageMenuFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutPageMenuFlyoutMenuItem>(new[]
                {
                    new FlyoutPageMenuFlyoutMenuItem { Id = 0, Title = "Home" ,IconSource="home.png" , TargetType=typeof(HomePage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 1, Title = "Patients" ,IconSource="patient.png", TargetType=typeof(PatientPageDetails)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 2, Title = "Colleages" ,IconSource="nurse.png", TargetType=typeof(UserPageDetails)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 3, Title = "Visits" ,IconSource="calendar.png", TargetType=typeof(VisitPageDetails)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 3, Title = "About us" ,IconSource="info.png", TargetType=typeof(AboutPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 4, Title = "Log out" ,IconSource="sign.png", TargetType=typeof(LoginPage)},
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}
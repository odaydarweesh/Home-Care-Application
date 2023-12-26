using HomeCareApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace HomeCareApp.ViewModel
{
    public class FlyoutPageMenuFlyoutViewModel : FlyoutPage
    {
        public ObservableCollection<FlyoutPageMenuFlyoutMenuItem> MenuItems { get; set; }

        public FlyoutPageMenuFlyoutViewModel()
        {
            if (App.UserRole == "Manager")
            {
                MenuItems = new ObservableCollection<FlyoutPageMenuFlyoutMenuItem>(new[]
        {


                    //new FlyoutPageMenuFlyoutMenuItem { Id = 0, Title = "Home" ,IconSource="home.png" , TargetType=typeof(MissionPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 1, Title = "Patients" ,IconSource="patient.png", TargetType=typeof(PatientPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 2, Title = "Colleages" ,IconSource="nurse.png", TargetType=typeof(UserPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 3, Title = "Medicine" ,IconSource="medicine.png", TargetType=typeof(MedicinePage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 4, Title = "Effort" ,IconSource="effort.png", TargetType=typeof(EffortPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 5, Title = "Disease" ,IconSource="disease.png", TargetType=typeof(DiseasePage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 6, Title = "Visits" ,IconSource="calendar.png", TargetType=typeof(VisitPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 7, Title = "About us" ,IconSource="info.png", TargetType=typeof(AboutPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 8, Title = "Log out" ,IconSource="sign.png", TargetType=typeof(LoginPage)},
                });
            }

            else if (App.UserRole == "Nurse")
            {
                MenuItems = new ObservableCollection<FlyoutPageMenuFlyoutMenuItem>(new[]
            {


                    //new FlyoutPageMenuFlyoutMenuItem { Id = 0, Title = "Home" ,IconSource="home.png" , TargetType=typeof(MissionPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 1, Title = "Patients" ,IconSource="patient.png", TargetType=typeof(PatientPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 2, Title = "Colleages" ,IconSource="nurse.png", TargetType=typeof(UserPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 3, Title = "Medicine" ,IconSource="medicine.png", TargetType=typeof(MedicinePage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 4, Title = "Effort" ,IconSource="effort.png", TargetType=typeof(EffortPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 5, Title = "Disease" ,IconSource="disease.png", TargetType=typeof(DiseasePage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 6, Title = "Visits" ,IconSource="calendar.png", TargetType=typeof(VisitPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 7, Title = "About us" ,IconSource="info.png", TargetType=typeof(AboutPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 8, Title = "Log out" ,IconSource="sign.png", TargetType=typeof(LoginPage)},
                });
            }
            else if (App.UserRole == "AssistantNurse")
            {
                MenuItems = new ObservableCollection<FlyoutPageMenuFlyoutMenuItem>(new[]
            {


                   //new FlyoutPageMenuFlyoutMenuItem { Id = 0, Title = "Home" ,IconSource="home.png" , TargetType=typeof(MissionPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 1, Title = "Patients" ,IconSource="patient.png", TargetType=typeof(PatientPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 2, Title = "Colleages" ,IconSource="nurse.png", TargetType=typeof(UserDetailPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 3, Title = "Medicine" ,IconSource="medicine.png", TargetType=typeof(MedicinePage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 4, Title = "Effort" ,IconSource="effort.png", TargetType=typeof(EffortPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 5, Title = "Disease" ,IconSource="disease.png", TargetType=typeof(DiseasePage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 6, Title = "Visits" ,IconSource="calendar.png", TargetType=typeof(VisitPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 7, Title = "About us" ,IconSource="info.png", TargetType=typeof(AboutPage)},
                    new FlyoutPageMenuFlyoutMenuItem { Id = 8, Title = "Log out" ,IconSource="sign.png", TargetType=typeof(LoginPage)},
                });
            }
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

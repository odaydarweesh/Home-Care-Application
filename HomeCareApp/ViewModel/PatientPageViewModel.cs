using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HomeCareApp;
using HomeCareApp.Model;
using HomeCareApp.Views;

namespace HomeCareApp.ViewModel
{
    public class PatientPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Patient> Patients = new ObservableCollection<Patient>();
        public ObservableCollection<Patient> patientList = new ObservableCollection<Patient>();
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PatientPageViewModel()
        {
            GetPatients();
        }

        public async void GetPatients()
        {
            var patients = await App.MyDatabase.ReadPatients();

            ObservableCollection<Patient> Patients = new ObservableCollection<Patient>();
            foreach (var patient in patients)
            {
                Patients.Add(patient);
            }

            if (Patients != null)
            {

                await App.Current.MainPage.Navigation.PushAsync(new PatientDetailPage(patients));

            }
        }
    }
}






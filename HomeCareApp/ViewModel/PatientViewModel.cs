using HomeCareApp.Model;
using HomeCareApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HomeCareApp.ViewModel
{
    public class PatientViewModel : BaseViewModel
    {
        private Patient _selectedPatient;
        private ObservableCollection<Disease> diseaseCollection;
        public ObservableCollection<Disease> DiseaseCollection
        {
            get { return diseaseCollection; }
            set { diseaseCollection = value; }
        }
        public PatientViewModel()
        {

            diseaseCollection = new ObservableCollection<Disease>(); 
            diseaseCollection.Add(new Disease() { DiseaseName = "Diabetes" }); 
            diseaseCollection.Add(new Disease() { DiseaseName = "ADHD"});
        }


    //}

    public void OnAppearing()
        {
            IsBusy = true;
            SelectedPatient = null;
        }
        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                SetProperty(ref _selectedPatient, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(Patient patient)
        {
            if (patient == null)
                return;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

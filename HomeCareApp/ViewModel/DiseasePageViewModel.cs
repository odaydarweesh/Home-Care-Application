using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HomeCareApp;
using HomeCareApp.Model;
using HomeCareApp.Views;

namespace HomeCareApp.ViewModel
{
    public class DiseasePageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Disease> Diseases = new ObservableCollection<Disease>();
        public ObservableCollection<Disease> diseaseList = new ObservableCollection<Disease>();
        public ObservableCollection<Disease> DiseaseList
        {
            get { return diseaseList; }
            set { diseaseList = value; OnPropertyChanged("DiseaseList"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DiseasePageViewModel()
        {
            GetDiseases();
        }

        public async void GetDiseases()
        {
            var diseases = await App.MyDatabase.ReadDiseases();

            ObservableCollection<Disease> Diseases = new ObservableCollection<Disease>();
            foreach (var disease in diseases)
            {
                Diseases.Add(disease);
            }

            if (Diseases != null)
            {

                await App.Current.MainPage.Navigation.PushAsync(new DiseaseDetailPage(diseases));
                //Diseases.Add(new Disease());

            }
        }
    }
}






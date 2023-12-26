using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HomeCareApp;
using HomeCareApp.Model;
using HomeCareApp.Views;

namespace HomeCareApp.ViewModel
{
    public class VisitPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Visit> Visits = new ObservableCollection<Visit>();
        public ObservableCollection<Visit> visitList = new ObservableCollection<Visit>();
        public ObservableCollection<Visit> VisitList
        {
            get { return visitList; }
            set { visitList = value; OnPropertyChanged("VisitList"); }
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

        public VisitPageViewModel()
        {
            GetVisits();
        }

        public async void GetVisits()
        {
            var visits = await App.MyDatabase.ReadVisits();

            ObservableCollection<Visit> Visits = new ObservableCollection<Visit>();
            foreach (var visit in visits)
            {
                Visits.Add(visit);
            }

            if (Visits != null)
            {

                await App.Current.MainPage.Navigation.PushAsync(new VisitDetailPage(visits));

            }
        }
    }
}






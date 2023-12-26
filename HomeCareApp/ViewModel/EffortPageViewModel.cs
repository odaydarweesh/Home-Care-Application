using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HomeCareApp;
using HomeCareApp.Model;
using HomeCareApp.Views;

namespace HomeCareApp.ViewModel
{
    public class EffortPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Effort> Efforts = new ObservableCollection<Effort>();
        public ObservableCollection<Effort> effortList = new ObservableCollection<Effort>();
        public ObservableCollection<Effort> EffortList
        {
            get { return effortList; }
            set { effortList = value; OnPropertyChanged("EffortList"); }
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

        public EffortPageViewModel()
        {
            GetEfforts();
        }

        public async void GetEfforts()
        {
            var efforts = await App.MyDatabase.ReadEfforts();

            ObservableCollection<Effort> Efforts = new ObservableCollection<Effort>();
            foreach (var effort in efforts)
            {
                Efforts.Add(effort);
            }

            if (Efforts != null)
            {

                await App.Current.MainPage.Navigation.PushAsync(new EffortDetailPage(efforts));

            }
        }
    }
}






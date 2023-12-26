using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HomeCareApp;
using HomeCareApp.Model;
using HomeCareApp.Views;

namespace HomeCareApp.ViewModel
{
    public class MedicinePageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Medicine> Medicines = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> medicineList = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> MedicineList
        {
            get { return medicineList; }
            set { medicineList = value; OnPropertyChanged("MedicineList"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
       
        private bool swithOne;
        public bool SwithOne
        {
            set
            {
                if (swithOne != value)
                {
                    swithOne = value;
                    OnPropertyChanged("SwithOne");
                }
            }
            get
            {
                return swithOne;
            }
        }

        public MedicinePageViewModel()
        {
            SwithOne = true; // assign a value for `SwithOne `
            GetMedicines();
        }



        public async void GetMedicines()
        {
            var medicines = await App.MyDatabase.ReadMedicines();

            ObservableCollection<Medicine> Medicines = new ObservableCollection<Medicine>();
            foreach (var medicine in medicines)
            {
                Medicines.Add(medicine);
            }

            if (Medicines != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new MedicineDetailPage(medicines));
            }
        }
    }
}






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
    public class UserViewModel : BaseViewModel
    {
        private User _selectedUser;
        public UserViewModel()
        {

            
        }
       

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedUser = null;
        }
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(User user)
        {
            if (user == null)
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

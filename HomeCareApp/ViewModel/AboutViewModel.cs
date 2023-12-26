using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.ViewModels
{

    public class AboutViewModel : ContentPage
    {
        public AboutViewModel()
        {

            Title = "Who we are?";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://ranyyasso.wixsite.com/grupp23/about"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
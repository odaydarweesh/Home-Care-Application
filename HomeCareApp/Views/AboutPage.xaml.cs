using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            //SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                //myCollectionview.ItemSource = await App.MyDatabase.GetPatient();
            }
            catch { }
        }

        private async void OpenBrowser(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var url = button.ClassId;

            await Browser.OpenAsync(url);
        }
    }
}
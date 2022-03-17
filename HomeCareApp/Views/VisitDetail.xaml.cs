using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitDetail : ContentPage
    {
        public VisitDetail()
        {
            InitializeComponent();
        }
       
        async void Button_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(StartTime.Text) || string.IsNullOrWhiteSpace(EndTime.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhiteSpace value is Invalid", "Ok");
            }
            else
                AddNewVisit();
        }
        async void AddNewVisit()
        {

        }
    }
}
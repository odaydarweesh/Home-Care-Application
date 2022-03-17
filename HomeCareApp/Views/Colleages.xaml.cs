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
    public partial class Colleages : ContentPage
    {
        public Colleages()
        {
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
    }
}
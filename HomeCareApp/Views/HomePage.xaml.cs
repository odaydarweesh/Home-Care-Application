﻿using HomeCareApp.ViewModel;
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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            //SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            //BindingContext = new HomePageViewModel();
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
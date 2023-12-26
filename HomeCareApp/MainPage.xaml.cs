using HomeCareApp.Model;
using HomeCareApp.Services.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeCareApp
{
    public partial class MainPage : FlyoutPage 
        ///Du kanske inte tycker att det är en perfekt applikation,
        ///men jag är nöjd med det jag gjorde. Det här är en app som liknar de tre appar jag använder i mitt
        //////jobb i hemtjänsten i Orebro kommun. Faktum är att denna applikation innehåller de viktigaste 
        //////funktionerna i de tre applikationerna..... Oday
    {
        private SQLiteConnection _sqLiteConnection;


        public MainPage()
        {
            InitializeComponent();
        }
        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageMenuFlyoutMenuItem;
        if(item!= null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                flyout.listview.SelectedItem = null;
                IsPresented = false;
            }
        
        }
    }
}

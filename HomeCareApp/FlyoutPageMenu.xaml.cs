using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPageMenu : FlyoutPage
    {
        public FlyoutPageMenu()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            FlyoutPage.ListView1.ItemSelected += ListView_ItemSelected;

            string name = App.UserName;
            string userRole = App.UserRole;
            string email = App.UserEmail;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageMenuFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}
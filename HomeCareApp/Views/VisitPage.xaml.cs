using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCareApp.Model;
using HomeCareApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitPage : ContentPage
    {
        private Guid idUser ;
        private int _IdPatient;

        public static readonly ObservableCollection<Visit> CurrentSelectedFile = new ObservableCollection<Visit>();
        public ListView ListView;
        public VisitPage()//I den här klassen har vi tre olika konstruktörer.
                          //Den första har ingen parameter, den andra har _idUser-parameter typen guid,
                          //den tredje har idPatient-parameter typen int.
                          //Var och en av dem visar olika data beroende på vilken klass den användes i.
        {
            BindingContext = this;
            idUser = App.UserId;

            InitializeComponent();
            ListView = VisitList;
        }

        public VisitPage(Guid _idUser)
        {
            BindingContext = this;
            idUser = _idUser;

            InitializeComponent();
            //App.Current.MainPage = new NavigationPage(new FlyoutPageMenu());
            ListView = VisitList;
        }
        public VisitPage(int idPatient)
        {
            BindingContext = this;
            _IdPatient = idPatient;

            InitializeComponent();
            _IdPatient = idPatient;
            ListView = VisitList;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(_IdPatient == 0)
            {
            VisitList.ItemsSource = await App.MyDatabase.ReadVisitsWithIdUser(idUser);

            }
            else
            {
             VisitList.ItemsSource = await App.MyDatabase.ReadVisitsWithIdPatient(_IdPatient);

            }
        }



        async void AddVisitOnClicked(object sender, EventArgs e)
        {
                await Navigation.PushAsync(new NewVisitPage());
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var visitdetails = e.Item as Visit;
            await Navigation.PushAsync(new VisitDetailPage(visitdetails));

        }
        async void OnMoreEdit(object sender, EventArgs e)//// Vi använder async metoder för att hantera asynchronous executions.
        {
            var item = sender as MenuItem;
            var visi = item.CommandParameter as Visit;
            await Navigation.PushAsync(new NewVisitPage(visi));// //Vi använder await-operator till Task  i en metod markerad som async.
        }

        async void OnDelete(object sender, EventArgs e)//// Vi använder async metoder för att hantera asynchronous executions.
        {
            var item = sender as MenuItem;
            var visi = item.CommandParameter as Visit;
            var result = await DisplayAlert("Delete", $"Delete { visi.VisitName}  from the database", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteVisit(visi);////Vi använder await-operator till Task  i en metod markerad som async.
                VisitList.ItemsSource = await App.MyDatabase.ReadVisits();
            }
        }


        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)//söka efter en specifik besök
        {
            VisitList.ItemsSource = await App.MyDatabase.SearchVisit(e.NewTextValue);
        }
    }
}
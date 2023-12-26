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
    public partial class EffortPage : ContentPage
    {
        private Guid idUser;

        public static readonly ObservableCollection<Effort> CurrentSelectedFile = new ObservableCollection<Effort>();

        public ListView ListView;
        int _IdPatient;

        public EffortPage(int idPatient)//I den här klassen har vi tre olika konstruktörer.
                                        //Den första har ingen parameter, den andra har _idUser-parameter typen guid,
                                        //den tredje har idPatient-parameter typen int.
                                        //Var och en av dem visar olika data beroende på vilken klass den användes i.
        {
            _IdPatient = idPatient;
            BindingContext = this;
            idUser = App.UserId;

            InitializeComponent();
            _IdPatient = idPatient;
            ListView = EffortList;
        }

        public EffortPage()
        {
            BindingContext = this;

            InitializeComponent();
            ListView = EffortList;
        }

        public EffortPage(Guid _idUser)
        {
            BindingContext = this;
            idUser = _idUser;

            InitializeComponent();
            ListView = EffortList;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_IdPatient == 0) 
            { 
            EffortList.ItemsSource = await App.MyDatabase.ReadEffortsWithIdUser(idUser);
            }
            else
            {
                EffortList.ItemsSource = await App.MyDatabase.ReadEffortsWithIdPatient(_IdPatient);

            }
        }



        async void Button_OnClicked(object sender, EventArgs e)
        {
                await Navigation.PushAsync(new NewEffortPage());

        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var effortdetails = e.Item as Effort;
             await Navigation.PushAsync(new EffortDetailPage(effortdetails));

        }
        async void OnMore_Edit(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var effo = item.CommandParameter as Effort;
            await Navigation.PushAsync(new NewEffortPage(effo));////Vi använder await-operator till Task  i en metod markerad som async.
        }

        async void OnDelete(object sender, EventArgs e)//// Vi använder async metoder för att hantera asynchronous executions.
        {
            var item = sender as MenuItem;
            var effo = item.CommandParameter as Effort;
            var result = await DisplayAlert("Delete", $"Delete { effo.EffortName}  from the database", "Yes", "No");
            if (result)
            {
                await App.MyDatabase.DeleteEffort(effo);////Vi använder await-operator till Task  i en metod markerad som async.
                EffortList.ItemsSource = await App.MyDatabase.ReadEfforts();
            }
        }


        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)//söka efter en specifik insats
        {
            EffortList.ItemsSource = await App.MyDatabase.SearchEffort(e.NewTextValue);
        }
    }
}
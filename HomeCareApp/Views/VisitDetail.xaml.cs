using HomeCareApp.Model;
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
        Visit _visit;
        public VisitDetail(Visit visit)
        {
            //InitializeComponent();
            //Title = "Edit Information";
            //_visit = visit;
            //EntryUser.Text = visit.User;
            //EntryPatient.Text = visit.Patient;
            //EntryEffort.Text = visit.Effort;
            //EntryStartTime.Text = visit.StartTime;
            //EntryEndTime.Text = visit.EndTime;
            //EntryMedicine.Text = visit.Medicine;

            //EntryPatient.Focus();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryUser.Text) || string.IsNullOrWhiteSpace(EntryPatient.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
            else if (_visit != null)
            {
                EditVisit();
            }
            else
            {
                AddNewVisit();
            }

        }

        async void AddNewVisit()
        {
            //await App.MyDatabase.CreateVisit(new Model.Visit
            //{
            //    User = EntryUser.Text,
            //    Patient = EntryPatient.Text,
            //    Effort = EntryEffort.Text,
            //    StartTime = EntryStartTime.Text,
            //    EndTime = EntryEndTime.Text,
            //    Medicine = EntryMedicine.Text

            //});
        }

        async void EditVisit()
        {
            //_visit.User = EntryUser.Text;
            //_visit.Patient = EntryPatient.Text;
            //_visit.Effort = EntryEffort.Text;
            //_visit.StartTime = EntryStartTime.Text;
            //_visit.EndTime = EntryEndTime.Text;
            //_visit.Medicine = EntryMedicine.Text;
            //await App.MyDatabase.UpdateVisit(_visit);
            //await Navigation.PopAsync();

        }
    }
}
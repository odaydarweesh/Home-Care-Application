using HomeCareApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientDetail : ContentPage
    {
        public PatientDetail()
        {
            InitializeComponent();
        }
        Patient _patient;
        public PatientDetail(Patient patient)
        {
            InitializeComponent();
            Title = "Edit Information";
            _patient = patient;
            EntryFirstName.Text = patient.FirstName;
            EntryLastName.Text = patient.LastName;
            EntryFirstName.Focus();
        }
         

        async void Handle_Clicked(object sender, EventArgs e)
        {
           if (string.IsNullOrWhiteSpace(EntryFirstName.Text)|| string.IsNullOrWhiteSpace(EntryLastName.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhitSpace value is Invalid", "OK");
            }
           else if(_patient != null)
            {
                EditPatient();
            }
            else
            {
                AddNewPatient();
            }
          
        }

        async void AddNewPatient()
        {
            await App.MyDatabase.CreatePatient(new Model.Patient
            {
                FirstName = EntryFirstName.Text,
                LastName = EntryLastName.Text,
                Personnummer = EntryPersonnummer.Text,
                Adress = EntryAdress.Text,
                PhoneNumber = EntryPhoneNumber.Text,
                Email = EntryEmail.Text

            });
        }

        async void EditPatient()
        {
            _patient.FirstName = EntryFirstName.Text;
            _patient.LastName = EntryLastName.Text;
            _patient.Personnummer = EntryPersonnummer.Text;
            _patient.Adress = EntryAdress.Text;
            _patient.PhoneNumber = EntryPhoneNumber.Text;
            _patient.Email = EntryEmail.Text;
            await App.MyDatabase.UpdatePatient(_patient);
            await Navigation.PopAsync();

        }
    }
}
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
    public partial class PatientDetailPage : ContentPage
    {
        int _idPatient;

        public PatientDetailPage(Patient patientdetails)//// En konstruktör har en parameter patientdetails typ objekt Patient som visar Patientinformation
                                                        // såsom firstName, lastName, email, personnummer, phoneNumber, adress, description  och namnet på patienten 
        {
            var firstName = patientdetails.FirstName;
            var lastName = patientdetails.LastName;
            var email = patientdetails.Email;
            var personnummer = patientdetails.Personnummer;
            var phoneNumber = patientdetails.PhoneNumber;
            var adress = patientdetails.Adress;
            var description = patientdetails.Description;
            int idPatient = patientdetails.IdPatient;


            InitializeComponent();
            PatientFirstNameShow.Text = firstName;
            PatientLastNameShow.Text = lastName;
            EmailShow.Text = email;
            PersonnummerShow.Text = personnummer;
            PhoneNumberShow.Text = phoneNumber;
            AdressShow.Text = adress;
            DescriptionShow.Text = description;
            _idPatient = idPatient;

        }
        private async void SeAllPatientsVisit(object sender, EventArgs e)// Metod som visar alla besök för den här patienten
        {
            await Navigation.PushAsync(new VisitPage(_idPatient));
        }

        private async void SeAllPatientsEffort(object sender, EventArgs e)// Metod som visar alla insatser för den här patienten
        {
            await Navigation.PushAsync(new EffortPage(_idPatient));
        }
        private async void SeAllPatientsDisease(object sender, EventArgs e)// Metod som visar alla sjukdomer för den här patienten
        {
            await Navigation.PushAsync(new DiseasePage(_idPatient));
        }
        private async void SeAllPatientsMedicine(object sender, EventArgs e)// Metod som visar alla medicin för den här patienten
        {
            await Navigation.PushAsync(new MedicinePage(_idPatient));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        public PatientDetailPage(List<Patient> patients)
        {
        }

        public PatientDetailPage(string firstName, string lastName, string personnummer, string email, string phoneNumber, string adress)
        {
        }
    }
}
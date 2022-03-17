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
        //async void Button_Clicked(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(EntryFirstName.Text) || string.IsNullOrWhiteSpace(EntryLastName.Text))
        //    {
        //        await DisplayAlert("Invalid", "Blank or WhiteSpace value is Invalid", "Ok");
        //    }
        //    else
        //        AddNewPatient();
        //}
        //async void AddNewPatient()
        //{
        //    //await App.MyDatabase.AddPatient(new Model.Patient
        //    //{
        //    //    FirstName = FirstNameEntry.Text,
        //    //    LastName = LastNameEntry.Text
        //    //});
        //    await Navigation.PopAsync();
        //}




        //async void Handle_Clicked(object sender, System.EventArgs e)
        //{
        //    var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
        //    var db = new SQLiteConnection(dbpath);
        //    db.CreateTable<User>();

        //    var item = new User()
        //    {
        //        FirstName = EntryFirstName.Text,
        //        LastName = EntryLastName.Text,
        //        Personnummer = EntryPersonnummer.Text,
        //        Adress = EntryAdress.Text,
        //        PhoneNumber = EntryPhoneNumber.Text,
        //        Email = EntryEmaile.Text,


        //    };
        //    db.Insert(item);
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        var result = await this.DisplayAlert("Congratulation", "New patient added successfully", "Yes", "Cancle");
        //        if (result)
        //            await Navigation.PushAsync(new PatientPage_Details());
        //    });
        //}

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
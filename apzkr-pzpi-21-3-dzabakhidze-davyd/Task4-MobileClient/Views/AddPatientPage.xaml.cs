using CareWatch.Mobile.Models.Requests;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views;

public partial class AddPatientPage : ContentPage
{
        public AddPatientPage()
        {
            InitializeComponent();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var newPatientRequest = new PatientRequest
            {
                Contact = new ContactRequest()
                {
                    FirstName = patientCtrl.FirstName,
                    LastName = patientCtrl.LastName,
                    Phone = patientCtrl.Phone,       
                    Email = patientCtrl.Email,
                    Address = patientCtrl.Address,
                    DateOfBirth = patientCtrl.DateOfBirth.ToUniversalTime(),
                    MiddleName = patientCtrl.MiddleName,
                    Gender = 0
                }
            };

            var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<PatientApiRepository>();
            var createdPatient = await apiRepository.CreatePatientAsync(newPatientRequest);

            if (createdPatient != null)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                Console.WriteLine("Failed to create a new patient.");
            }
        }

    private void patientCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}
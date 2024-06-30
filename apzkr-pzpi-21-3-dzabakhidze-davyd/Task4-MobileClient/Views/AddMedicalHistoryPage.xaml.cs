using CareWatch.Mobile.Models.Requests;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    public partial class AddMedicalHistoryPage : ContentPage
    {
        public AddMedicalHistoryPage()
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
            var newMedicalHistoryRequest = new MedicalHistoryRequest
            {
                Disease = medicalHistoryCtrl.Disease,
                Treatment = medicalHistoryCtrl.Treatment
            };

            var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<MedicalHistoryApiRepository>();
            var createdMedicalHistory = await apiRepository.CreateMedicalHistoryAsync(newMedicalHistoryRequest);

            if (createdMedicalHistory != null)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                Console.WriteLine("Failed to create a new medical history.");
            }
        }



        private void medicalHistoryCtrl_OnError(object sender, string e)
        {
            DisplayAlert("Error", e, "OK");
        }
    }
}
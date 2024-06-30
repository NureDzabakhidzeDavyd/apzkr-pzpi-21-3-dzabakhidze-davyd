using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Requests;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    [QueryProperty(nameof(MedicalHistoryId), "Id")]
    public partial class EditMedicalHistoryPage : ContentPage
    {
        private MedicalHistory _medicalHistory;
        private MedicalHistoryApiRepository apiRepository;

        public EditMedicalHistoryPage()
        {
            InitializeComponent();
            apiRepository = Application.Current.Handler.MauiContext.Services.GetService<MedicalHistoryApiRepository>();
        }

        public string MedicalHistoryId
        {
            set
            {
                InitializeMedicalHistoryAsync(value);
            }
        }

        private async void InitializeMedicalHistoryAsync(string value)
        {
            if (!string.IsNullOrEmpty(value) && Guid.TryParse(value, out Guid medicalHistoryGuid))
            {
                _medicalHistory = await apiRepository.GetMedicalHistoryByIdAsync(medicalHistoryGuid);

                if (_medicalHistory != null)
                {
                    medicalHistoryCtrl.Disease = _medicalHistory.Disease;
                    medicalHistoryCtrl.Treatment = _medicalHistory.Treatment;
                    // Assuming you have other properties to bind to in the MedicalHistoryControl
                }
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            _medicalHistory.Disease = medicalHistoryCtrl.Disease;
            _medicalHistory.Treatment = medicalHistoryCtrl.Treatment;
            // Update other properties based on your MedicalHistoryControl

            var newMedicalHistoryRequest = new MedicalHistoryRequest
            {
                // Map properties from your MedicalHistoryControl to the request object
                Disease = medicalHistoryCtrl.Disease,
                Treatment = medicalHistoryCtrl.Treatment
            };

            await apiRepository.UpdateMedicalHistoryAsync(_medicalHistory.Id, newMedicalHistoryRequest);
            await Shell.Current.Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}

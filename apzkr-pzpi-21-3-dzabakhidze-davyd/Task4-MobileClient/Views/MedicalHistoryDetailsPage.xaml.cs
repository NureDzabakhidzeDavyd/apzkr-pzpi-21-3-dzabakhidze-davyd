using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    [QueryProperty(nameof(MedicalHistoryId), "Id")]
    public partial class MedicalHistoryDetailsPage : ContentPage
    {
        private MedicalHistory _medicalHistory;
        private readonly MedicalHistoryApiRepository _apiRepository;

        public MedicalHistoryDetailsPage()
        {
            InitializeComponent();
            _apiRepository = Application.Current.Handler.MauiContext.Services.GetService<MedicalHistoryApiRepository>();
        }

        public string MedicalHistoryId
        {
            set
            {
                LoadMedicalHistoryAsync(value);
            }
        }

        private async void LoadMedicalHistoryAsync(string value)
        {
            if (Guid.TryParse(value, out Guid medicalHistoryGuid))
            {
                _medicalHistory = await _apiRepository.GetMedicalHistoryByIdAsync(medicalHistoryGuid);

                if (_medicalHistory != null)
                {
                    BindingContext = _medicalHistory;
                }
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}

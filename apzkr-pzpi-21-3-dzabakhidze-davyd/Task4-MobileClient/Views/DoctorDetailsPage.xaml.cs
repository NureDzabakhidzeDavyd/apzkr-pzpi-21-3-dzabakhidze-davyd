using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    [QueryProperty(nameof(DoctorId), "Id")]
    public partial class DoctorDetailsPage : ContentPage
    {
        private Doctor _doctor;

        public DoctorDetailsPage()
        {
            InitializeComponent();
        }

        private string _doctorId;
        public string DoctorId
        {
            set
            {
                _doctorId = value;
                LoadDoctorDetailsAsync();
            }
        }

        private async void LoadDoctorDetailsAsync()
        {
            if (Guid.TryParse(_doctorId, out Guid doctorGuid))
            {
                var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<DoctorApiRepository>();

                try
                {
                    _doctor = await apiRepository.GetDoctorByIdAsync(doctorGuid);
                    BindingContext = _doctor;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading doctor details: {ex.Message}");
                }
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views;

[QueryProperty(nameof(EmergencyRequestId), "Id")]
public partial class EmergencyRequestDetailsPage : ContentPage
{
    private EmergencyRequest _emergencyRequest;
    private readonly EmergencyRequestApiRepository _apiRepository;

    public EmergencyRequestDetailsPage()
    {
        InitializeComponent();
        _apiRepository = Application.Current.Handler.MauiContext.Services.GetService<EmergencyRequestApiRepository>();
    }

    public string EmergencyRequestId
    {
        set
        {
            LoadEmergencyRequestAsync(value);
        }
    }

    private async void LoadEmergencyRequestAsync(string value)
    {
        if (Guid.TryParse(value, out Guid emergencyRequestGuid))
        {
            _emergencyRequest = await _apiRepository.GetEmergencyRequestByIdAsync(emergencyRequestGuid);

            if (_emergencyRequest != null)
            {
                BindingContext = _emergencyRequest;
            }
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
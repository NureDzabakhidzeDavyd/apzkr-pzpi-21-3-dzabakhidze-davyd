using CareWatch.Mobile.Models;
using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views;

[QueryProperty(nameof(PatientId), "Id")]
public partial class PatientDetailsPage : ContentPage
{
    private Patient _patient;

    public PatientDetailsPage()
	{
		InitializeComponent();
	}

    public string PatientId
    {
        set
        {
            if (Guid.TryParse(value, out Guid patientGuid))
            {
                var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<PatientApiRepository>();
                _patient = apiRepository.GetPatientByIdAsync(patientGuid).Result;
                BindingContext = _patient;
            }
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void EditPatientButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button editButton && editButton.BindingContext is Patient selectedPatient)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }

}
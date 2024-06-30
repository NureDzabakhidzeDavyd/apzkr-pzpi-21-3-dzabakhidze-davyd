using CareWatch.Mobile.Models.Requests;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views;

public partial class AddEmergencyRequestPage : ContentPage
{
    public AddEmergencyRequestPage()
    {
        InitializeComponent();
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var newEmergencyRequest = new EmergencyRequestRequest
        {
            Type = emergencyRequestCtrl.Type,
            Location = emergencyRequestCtrl.Location,
            Status = emergencyRequestCtrl.Status,
            // Set the appropriate doctor ID based on the selected doctor
            //AssignedDoctorId = Guid.Parse(emergencyRequestCtrl.AssignedDoctor)
        };

        var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<EmergencyRequestApiRepository>();
        var createdEmergencyRequest = await apiRepository.CreateEmergencyRequestAsync(newEmergencyRequest);

        if (createdEmergencyRequest != null)
        {
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            Console.WriteLine("Failed to create a new emergency request.");
        }
    }

    private void emergencyRequestCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}
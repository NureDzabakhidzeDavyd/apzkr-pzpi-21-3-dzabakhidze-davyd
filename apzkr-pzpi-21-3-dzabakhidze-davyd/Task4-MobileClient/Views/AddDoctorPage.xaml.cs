using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Requests;
using CareWatch.Mobile.Models.Services;
using Contact = CareWatch.Mobile.Models.Entities.Contact;

namespace CareWatch.Mobile.Views;

public partial class AddDoctorPage : ContentPage
{
    public AddDoctorPage()
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
        var newDoctorRequest = new DoctorRequest
        {
           Contact = new ContactRequest
           {
               FirstName = doctorCtrl.FirstName,
               LastName = doctorCtrl.LastName,
               Phone = doctorCtrl.Phone,
               Email = doctorCtrl.Email,
               Address = doctorCtrl.Address,
               DateOfBirth = doctorCtrl.DateOfBirth.ToUniversalTime(),
               MiddleName = doctorCtrl.MiddleName,
               Gender = 0 // Set the appropriate gender value for the doctor
           }
        };

        var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<DoctorApiRepository>();
        var createdDoctor = await apiRepository.CreateDoctorAsync(newDoctorRequest);

        if (createdDoctor != null)
        {
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            Console.WriteLine("Failed to create a new doctor.");
        }
    }

    private void doctorCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}
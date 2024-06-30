using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CareWatch.Mobile.Views;

public partial class EmergencyRequestsPage : ContentPage
{
    public EmergencyRequestsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadEmergencyRequests();
    }

    private async void emergencyRequestsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var selectedEmergencyRequest = (EmergencyRequest)e.SelectedItem;
            await Shell.Current.GoToAsync($"{nameof(EmergencyRequestDetailsPage)}?Id={selectedEmergencyRequest.Id}");
            emergencyRequestsList.SelectedItem = null;
        }
    }

    private void emergencyRequestsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        emergencyRequestsList.SelectedItem = null;
    }

    private async void EditEmergencyRequestButton_Clicked(object sender, EventArgs e)
    {
        if (sender is MenuItem menuItem && menuItem.BindingContext is EmergencyRequest selectedEmergencyRequest)
        {
            // Navigate to the edit page
            // await Shell.Current.GoToAsync($"{nameof(EditEmergencyRequestPage)}?Id={selectedEmergencyRequest.Id}");
        }
    }

    private async void DeleteEmergencyRequestButton_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var emergencyRequest = menuItem.BindingContext as EmergencyRequest;

        bool userConfirmation = await DisplayAlert("Confirm Deletion", $"Are you sure you want to delete this emergency request?", "Yes", "No");

        if (userConfirmation)
        {
            var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<EmergencyRequestApiRepository>();
            try
            {
                await apiRepository.DeleteEmergencyRequestAsync(emergencyRequest.Id);
                await LoadEmergencyRequests();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }

    private async Task LoadEmergencyRequests()
    {
        var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<EmergencyRequestApiRepository>();
        var emergencyRequests = await apiRepository.GetAllEmergencyRequestsAsync();
        emergencyRequestsList.ItemsSource = new ObservableCollection<EmergencyRequest>(emergencyRequests);
    }

    private async void AddEmergencyRequestButton_Clicked(object sender, EventArgs e)
    {
        // Navigate to the page to add a new emergency request
        // await Shell.Current.GoToAsync(nameof(AddEmergencyRequestPage));
    }
}
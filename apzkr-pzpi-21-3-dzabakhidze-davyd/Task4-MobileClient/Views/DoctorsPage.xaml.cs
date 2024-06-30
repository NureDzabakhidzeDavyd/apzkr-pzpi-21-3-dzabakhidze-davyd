using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CareWatch.Mobile.Views;

public partial class DoctorsPage : ContentPage
{
	public DoctorsPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDoctors();
    }

    private async void doctorsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var selectedDoctor = (Doctor)e.SelectedItem;
            await Shell.Current.GoToAsync($"{nameof(DoctorDetailsPage)}?Id={selectedDoctor.Id}");
            doctorsList.SelectedItem = null;
        }
    }

    private void doctorsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        doctorsList.SelectedItem = null;
    }

    private async void EditDoctorButton_Clicked(object sender, EventArgs e)
    {
        if (sender is MenuItem menuItem && menuItem.BindingContext is Doctor selectedDoctor)
        {
            await Shell.Current.GoToAsync($"{nameof(EditDoctorPage)}?Id={selectedDoctor.Id}");
        }
    }

    private async void DeleteDoctorButton_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var doctor = menuItem.BindingContext as Doctor;

        bool userConfirmation = await DisplayAlert("Confirm Deletion", $"Are you sure you want to delete Dr. {doctor.Contact.FirstName} {doctor.Contact.LastName}?", "Yes", "No");

        if (userConfirmation)
        {
            var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<DoctorApiRepository>();
            try
            {
                await apiRepository.DeleteDoctorAsync(doctor.Id);
                await LoadDoctors();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }

    private async Task LoadDoctors()
    {
        var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<DoctorApiRepository>();
        var doctors = await apiRepository.GetAllDoctorsAsync();
        doctorsList.ItemsSource = new ObservableCollection<Doctor>(doctors);
    }

    private async void AddDoctorButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddDoctorPage));
    }
}
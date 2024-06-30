using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    public partial class MedicalHistoriesPage : ContentPage
    {
        public MedicalHistoriesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //searchBar.Text = string.Empty;

            await LoadMedicalHistories();
        }

        private async void medicalHistoriesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedMedicalHistory = (MedicalHistory)e.SelectedItem;

                await Shell.Current.GoToAsync($"{nameof(MedicalHistoryDetailsPage)}?Id={selectedMedicalHistory.Id}");

                medicalHistoriesList.SelectedItem = null;
            }
        }

        private void medicalHistoriesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            medicalHistoriesList.SelectedItem = null;
        }

        private async void EditMedicalHistoryButton_Clicked(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.BindingContext is MedicalHistory selectedMedicalHistory)
            {
                await Shell.Current.GoToAsync($"{nameof(EditMedicalHistoryPage)}?Id={selectedMedicalHistory.Id}");
            }
        }

        private async void DeleteMedicalHistoryButton_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var medicalHistory = menuItem.BindingContext as MedicalHistory;

            bool userConfirmation = await DisplayAlert("Confirm Deletion", $"Are you sure you want to delete the medical history?", "Yes", "No");

            if (userConfirmation)
            {
                var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<MedicalHistoryApiRepository>();
                try
                {
                    await apiRepository.DeleteMedicalHistoryAsync(medicalHistory.Id);
                    await LoadMedicalHistories();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }

        private async Task LoadMedicalHistories()
        {
            var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<MedicalHistoryApiRepository>();
            var medicalHistories = await apiRepository.GetAllMedicalHistoriesAsync();
            medicalHistoriesList.ItemsSource = new ObservableCollection<MedicalHistory>(medicalHistories);
        }

        private async void AddMedicalHistoryButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddMedicalHistoryPage));
        }
    }
}

using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    public partial class PatientsPage : ContentPage
    {
        public PatientsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //searchBar.Text = string.Empty;

            await LoadPatients();
        }

        private async void patientsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedPatient = (Patient)e.SelectedItem;

                await Shell.Current.GoToAsync($"{nameof(PatientDetailsPage)}?Id={selectedPatient.Id}");

                patientsList.SelectedItem = null;
            }
        }

        private void patientsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            patientsList.SelectedItem = null;
        }

        private async void EditPatientButton_Clicked(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.BindingContext is Patient selectedPatient)
            {
                await Shell.Current.GoToAsync($"{nameof(EditPatientPage)}?Id={selectedPatient.Id}");
            }
        }

        private async void DeletePatientButton_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var patient = menuItem.BindingContext as Patient;

            bool userConfirmation = await DisplayAlert("Confirm Deletion", $"Are you sure you want to delete {patient.Contact.FirstName} {patient.Contact.LastName}?", "Yes", "No");

            if (userConfirmation)
            {
                var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<PatientApiRepository>();
                try
                {
                    await apiRepository.DeletePatientAsync(patient.Id);
                    await LoadPatients();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    throw;
                }
                
            }
        }

        //private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    string searchText = searchBar.Text;
        //    patientsList.ItemsSource = new ObservableCollection<Patient>(PatientRepository.SearchPatients(searchText));
        //}

        private async Task LoadPatients()
        {
            var apiRepository = Application.Current.Handler.MauiContext.Services.GetService<PatientApiRepository>();
            var patients = await apiRepository.GetAllPatientsAsync();
            patientsList.ItemsSource = new ObservableCollection<Patient>(patients);
        }

        private async void AddPatientButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddPatientPage));
        }
    }
}

using System;

namespace CareWatch.Mobile.Views.Controls
{
    public partial class MedicalHistoryControl : ContentView
    {
        public MedicalHistoryControl()
        {
            InitializeComponent();
        }

        public string Disease
        {
            get => entryDisease.Text;
            set => entryDisease.Text = value;
        }

        public string Treatment
        {
            get => entryTreatment.Text;
            set => entryTreatment.Text = value;
        }

        public event EventHandler<Guid> PatientSelected;

        public event EventHandler<Guid> DoctorSelected;

        public event EventHandler SaveButtonClicked;

        public event EventHandler CancelButtonClicked;

        public void SetPatientsList(string[] patients)
        {
            pickerPatients.ItemsSource = patients;
        }

        public void SetDoctorsList(string[] doctors)
        {
            pickerDoctors.ItemsSource = doctors;
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            // Get the selected patient and doctor IDs
            var selectedPatientId = GetSelectedId(pickerPatients);
            var selectedDoctorId = GetSelectedId(pickerDoctors);

            // Notify subscribers about the selected patient and doctor
            PatientSelected?.Invoke(this, selectedPatientId);
            DoctorSelected?.Invoke(this, selectedDoctorId);

            // Notify subscribers about the save button click
            SaveButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private Guid GetSelectedId(Picker picker)
        {
            var selectedItem = picker.SelectedItem?.ToString();
            // You need to implement a logic to map the selected item to its corresponding ID
            // For simplicity, I assume that the ID is included in the item text
            if (Guid.TryParse(selectedItem, out Guid selectedId))
            {
                return selectedId;
            }
            return Guid.Empty;
        }

        public event EventHandler<string> OnError;
        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            // Notify subscribers about the cancel button click
            CancelButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
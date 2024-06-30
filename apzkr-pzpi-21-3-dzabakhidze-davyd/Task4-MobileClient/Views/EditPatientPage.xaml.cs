using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Requests;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    [QueryProperty(nameof(PatientId), "Id")]
    public partial class EditPatientPage : ContentPage
    {
        private Patient _patient;
        private PatientApiRepository apiRepository;

        public EditPatientPage()
        {
            InitializeComponent();
            apiRepository = Application.Current.Handler.MauiContext.Services.GetService<PatientApiRepository>();
        }

        public string PatientId
        {
            set
            {
                InitializePatientAsync(value);
            }
        }

        private async void InitializePatientAsync(string value)
        {
            if (!string.IsNullOrEmpty(value) && Guid.TryParse(value, out Guid patientGuid))
            {

                _patient = await apiRepository.GetPatientByIdAsync(patientGuid);

                if (_patient != null)
                {
                    patientCtrl.FirstName = _patient.Contact.FirstName;
                    patientCtrl.LastName = _patient.Contact.LastName;
                    patientCtrl.MiddleName = _patient.Contact.MiddleName;
                    patientCtrl.Phone = _patient.Contact.Phone;
                    patientCtrl.Email = _patient.Contact.Email;
                    patientCtrl.Address = _patient.Contact.Address;
                    patientCtrl.DateOfBirth = _patient.Contact.DateOfBirth;
                    patientCtrl.Gender = _patient.Contact.Gender;
                }
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            _patient.Contact.FirstName = patientCtrl.FirstName;
            _patient.Contact.LastName = patientCtrl.LastName;
            _patient.Contact.Phone = patientCtrl.Phone;
            _patient.Contact.Email = patientCtrl.Email;
            _patient.Contact.Address = patientCtrl.Address;
            _patient.Contact.DateOfBirth = patientCtrl.DateOfBirth.ToUniversalTime();
            _patient.Contact.MiddleName = patientCtrl.MiddleName;
            patientCtrl.Gender = _patient.Contact.Gender;


            var newPatientRequest = new PatientRequest
            {
                Contact = new ContactRequest()
                {
                    FirstName = patientCtrl.FirstName,
                    LastName = patientCtrl.LastName,
                    Phone = patientCtrl.Phone,
                    Email = patientCtrl.Email,
                    Address = patientCtrl.Address,
                    DateOfBirth = patientCtrl.DateOfBirth.ToUniversalTime(),
                    MiddleName = patientCtrl.MiddleName,
                    Gender = 0
                }
            };
            await apiRepository.UpdatePatientAsync(_patient.Id, newPatientRequest);
            await Shell.Current.Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
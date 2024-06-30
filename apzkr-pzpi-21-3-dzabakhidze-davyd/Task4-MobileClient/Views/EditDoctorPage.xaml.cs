using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Requests;
using CareWatch.Mobile.Models.Services;

namespace CareWatch.Mobile.Views
{
    [QueryProperty(nameof(DoctorId), "Id")]
    public partial class EditDoctorPage : ContentPage
    {
        private Doctor _doctor;
        private DoctorApiRepository apiRepository;

        public EditDoctorPage()
        {
            InitializeComponent();
            apiRepository = Application.Current.Handler.MauiContext.Services.GetService<DoctorApiRepository>();
        }

        public string DoctorId
        {
            set
            {
                InitializeDoctorAsync(value);
            }
        }

        private async void InitializeDoctorAsync(string value)
        {
            if (!string.IsNullOrEmpty(value) && Guid.TryParse(value, out Guid doctorGuid))
            {
                _doctor = await apiRepository.GetDoctorByIdAsync(doctorGuid);

                if (_doctor != null)
                {
                    doctorCtrl.FirstName = _doctor.Contact.FirstName;
                    doctorCtrl.LastName = _doctor.Contact.LastName;
                    doctorCtrl.MiddleName = _doctor.Contact.MiddleName;
                    doctorCtrl.Phone = _doctor.Contact.Phone;
                    doctorCtrl.Email = _doctor.Contact.Email;
                    doctorCtrl.Address = _doctor.Contact.Address;
                    doctorCtrl.DateOfBirth = _doctor.Contact.DateOfBirth;
                    doctorCtrl.Gender = _doctor.Contact.Gender;
                }
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            _doctor.Contact.FirstName = doctorCtrl.FirstName;
            _doctor.Contact.LastName = doctorCtrl.LastName;
            _doctor.Contact.Phone = doctorCtrl.Phone;
            _doctor.Contact.Email = doctorCtrl.Email;
            _doctor.Contact.Address = doctorCtrl.Address;
            _doctor.Contact.DateOfBirth = doctorCtrl.DateOfBirth.ToUniversalTime();
            _doctor.Contact.MiddleName = doctorCtrl.MiddleName;
            _doctor.Contact.Gender = doctorCtrl.Gender;

            var newDoctorRequest = new DoctorRequest
            {
                Contact = new ContactRequest()
                {
                    FirstName = doctorCtrl.FirstName,
                    LastName = doctorCtrl.LastName,
                    Phone = doctorCtrl.Phone,
                    Email = doctorCtrl.Email,
                    Address = doctorCtrl.Address,
                    DateOfBirth = doctorCtrl.DateOfBirth.ToUniversalTime(),
                    MiddleName = doctorCtrl.MiddleName,
                    Gender = 0 //TODO: fix this
                }
            };
            await apiRepository.UpdateDoctorAsync(_doctor.Id, newDoctorRequest);
            await Shell.Current.Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}

using CareWatch.Mobile.Models.Services;
using CareWatch.Mobile.Views;

namespace CareWatch.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            Routing.RegisterRoute(nameof(PatientsPage), typeof(PatientsPage));
            Routing.RegisterRoute(nameof(PatientDetailsPage), typeof(PatientDetailsPage));
            Routing.RegisterRoute(nameof(EditPatientPage), typeof(EditPatientPage));
            Routing.RegisterRoute(nameof(AddPatientPage), typeof(AddPatientPage));

            Routing.RegisterRoute(nameof(DoctorsPage), typeof(DoctorsPage));
            Routing.RegisterRoute(nameof(DoctorDetailsPage), typeof(DoctorDetailsPage));
            Routing.RegisterRoute(nameof(EditDoctorPage), typeof(EditDoctorPage));
            Routing.RegisterRoute(nameof(AddDoctorPage), typeof(AddDoctorPage));

            Routing.RegisterRoute(nameof(MedicalHistoriesPage), typeof(MedicalHistoriesPage));
            Routing.RegisterRoute(nameof(MedicalHistoryDetailsPage), typeof(MedicalHistoryDetailsPage));
            Routing.RegisterRoute(nameof(EditMedicalHistoryPage), typeof(EditMedicalHistoryPage));
            Routing.RegisterRoute(nameof(AddMedicalHistoryPage), typeof(AddMedicalHistoryPage));

            Routing.RegisterRoute(nameof(EmergencyRequestsPage), typeof(EmergencyRequestsPage));
            Routing.RegisterRoute(nameof(EmergencyRequestDetailsPage), typeof(EmergencyRequestDetailsPage));
            Routing.RegisterRoute(nameof(AddEmergencyRequestPage), typeof(AddEmergencyRequestPage));
        }
    }
}

// namespace Kolosok.Infrastructure;
//
// public class Seeder : ISeeder
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private List<Doctor> _doctors = new List<Doctor>();
//
//         public Seeder(IUnitOfWork unitOfWork)
//         {
//             _unitOfWork = unitOfWork;
//         }
//
//         public async Task SeedAsync()
//         {
//             var patients = await _unitOfWork.PatientRepository.GetAllByFiltersAsync(new SearchFIlter());
//             if (patients.Any())
//             {
//                 return;
//             }
//
//             await SeedDoctorsAsync(25);
//             await SeedPatientsAsync(25);
//         }
//
//         private async Task SeedDoctorsAsync(int count)
//         {
//             var doctors = new Faker<Doctor>(locale: "uk")
//                 .RuleFor(x => x.Contact, f => new Contact()
//                 {
//                     FirstName = f.Person.FirstName,
//                     LastName = f.Person.LastName,
//                     Email = f.Person.Email,
//                     Phone = f.Person.Phone,
//                     DateOfBirth = f.Person.DateOfBirth.ToUniversalTime(),
//                     Address = f.Person.Address.Street,
//                     Gender = f.Random.Enum<Gender>(),
//                     Role = Role.Doctor,
//                     Password = f.Random.Hash(12)
//                 })
//                 .Generate(count);
//             foreach (var doctor in doctors)
//             {
//                 var newDoctor = await _unitOfWork.DoctorRepository.CreateAsync(doctor);
//                 _doctors.Add(newDoctor);
//             }
//
//             await _unitOfWork.SaveChangesAsync();
//         }
//
//         private async Task SeedPatientsAsync(int count)
//         {
//             var patients = new Faker<Patient>(locale: "uk")
//                 .RuleFor(x => x.Contact, f => new Contact()
//                 {
//                     FirstName = f.Person.FirstName,
//                     LastName = f.Person.LastName,
//                     Email = f.Person.Email,
//                     Phone = f.Person.Phone,
//                     DateOfBirth = f.Person.DateOfBirth.ToUniversalTime(),
//                     Address = f.Person.Address.Street,
//                     Gender = f.Random.Enum<Gender>(),
//                     Role = Role.Patient,
//                     Password = f.Random.Hash(12)
//                 })
//                 .RuleFor(p => p.MedicalRecordNumber, f => f.Random.Hash(12))
//                 .Generate(count);
//
//             foreach (var patient in patients)
//             {
//                 var symptom = new Faker<Diagnosis>(locale: "uk")
//                     .RuleFor(s => s.Name, f => f.Lorem.Word())
//                     .RuleFor(s => s.SeverityLevel, f => f.Random.Enum<SeverityLevel>())
//                     .RuleFor(s => s.DetectionDate, f => f.Date.Past(1).ToUniversalTime())
//                     .RuleFor(s => s.Duration, f => f.Date.Future(1).ToUniversalTime())
//                     .RuleFor(s => s.RelatedFactors, f => f.Lorem.Sentence())
//                     .RuleFor(s => s.DoctorId, f => f.PickRandom(_doctors).Id)
//                     .Generate();
//                 
//                 var medicalHistory = new Faker<MedicalHistory>(locale: "uk")
//                     .RuleFor(m => m.PatientId, f => f.Random.Guid())
//                     .RuleFor(m => m.Disease, f => f.Lorem.Word())
//                     .RuleFor(m => m.Treatment, f => f.Lorem.Sentence())
//                     .RuleFor(m => m.AssignedDoctorId, f => f.PickRandom(_doctors).Id)
//                     .Generate();
//                 
//                 var emergencyRequests = new Faker<EmergencyRequest>(locale: "uk")
//                     .RuleFor(er => er.PatientId, f => f.PickRandom(patient).Id)
//                     .RuleFor(er => er.AcceptedDoctorId, f => f.PickRandom(_doctors).Id)
//                     .RuleFor(er => er.Location, f => f.Address.FullAddress())
//                     .RuleFor(er => er.Status, f => f.Random.Enum<EmergencyRequestStatus>())
//                     .RuleFor(er => er.Type, f => f.Random.Enum<EmergencyRequestType>())
//                     .Generate(new Random().Next(0, 5));
//                     
//
//                 patient.Diagnoses.Add(symptom);
//                 patient.MedicalHistories.Add(medicalHistory);
//                 patient.EmergencyRequests = emergencyRequests;
//
//                 await _unitOfWork.PatientRepository.CreateAsync(patient);
//             }
//
//             await _unitOfWork.SaveChangesAsync();
//         }
//
//     }
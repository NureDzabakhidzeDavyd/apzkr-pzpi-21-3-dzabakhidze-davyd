using System;
using System.Collections.Generic;
using CareWatch.Mobile.Models.Entities;

namespace CareWatch.Mobile.Models.Services
{
    public static class PatientRepository
    {
        private static readonly List<Patient> patients;

        static PatientRepository()
        {
            patients = new List<Patient>
    {
        new Patient
        {
            Id = Guid.NewGuid(),
            Contact = new Entities.Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                Phone = "555-1234",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                Role = "Patient"
            }
        },

        new Patient
        {
            Id = Guid.NewGuid(),
            Contact = new Entities.Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Address = "456 Oak St",
                Phone = "555-5678",
                DateOfBirth = new DateTime(1985, 5, 15),
                Gender = "Female",
                Role = "Patient"
            }
        },

        new Patient
        {
            Id = Guid.NewGuid(),
            Contact = new Entities.Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Michael",
                LastName = "Johnson",
                Email = "michael.johnson@example.com",
                Address = "789 Pine St",
                Phone = "555-9876",
                DateOfBirth = new DateTime(1982, 8, 20),
                Gender = "Male",
                Role = "Patient"
            }
        },

        new Patient
        {
            Id = Guid.NewGuid(),
            Contact = new Entities.Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Williams",
                Email = "emily.williams@example.com",
                Address = "101 Cedar St",
                Phone = "555-5432",
                DateOfBirth = new DateTime(1995, 3, 10),
                Gender = "Female",
                Role = "Patient"
            }
        },

        new Patient
        {
            Id = Guid.NewGuid(),
            Contact = new Entities.Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "David",
                LastName = "Brown",
                Email = "david.brown@example.com",
                Address = "202 Maple St",
                Phone = "555-8765",
                DateOfBirth = new DateTime(1978, 11, 5),
                Gender = "Male",
                Role = "Patient"
            }
        }
    };
        }

        public static IEnumerable<Patient> GetAllPatients()
        {
            return patients;
        }

        public static void AddPatient(Patient newPatient)
        {
            newPatient.Id = Guid.NewGuid();
            patients.Add(newPatient);
        }

        //public static List<Patient> SearchPatients(string filterText)
        //{
        //    var searchPatients = patients.Where(x => x.Contact.FirstName.Contains(filterText, StringComparison.OrdinalIgnoreCase) || x.Contact.LastName.Contains(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
        //    return searchPatients;
        //}

        public static Patient GetPatientById(Guid patientId)
        {
            var patient = patients.Find(p => p.Id == patientId);
            if (patient != null)
            {
                return new Patient()
                {
                    Id = patient.Id,
                    Contact = new Entities.Contact()
                    {
                        Id = patient.Contact.Id,
                        FirstName = patient.Contact.FirstName,
                        LastName = patient.Contact.LastName,
                        Email = patient.Contact.Email,
                        Address = patient.Contact.Address,
                        Phone = patient.Contact.Phone,
                        DateOfBirth = patient.Contact.DateOfBirth,
                        Gender = patient.Contact.Gender,
                        Role = patient.Contact.Role
                    }
                };
            }
            return null;
        }

        public static void UpdatePatient(Patient updatedPatient)
        {
            var existingPatient = patients.Find(p => p.Id == updatedPatient.Id);
            if (existingPatient != null)
            {
                existingPatient.Contact.FirstName = updatedPatient.Contact.FirstName;
                existingPatient.Contact.LastName = updatedPatient.Contact.LastName;
                existingPatient.Contact.Email = updatedPatient.Contact.Email;
                existingPatient.Contact.Address = updatedPatient.Contact.Address;
                existingPatient.Contact.Phone = updatedPatient.Contact.Phone;
                existingPatient.Contact.DateOfBirth = updatedPatient.Contact.DateOfBirth;
                existingPatient.Contact.Gender = updatedPatient.Contact.Gender;
                existingPatient.Contact.Role = updatedPatient.Contact.Role;
            }
        }

        public static void DeletePatient(Guid patientId)
        {
            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient != null)
            {
                patients.Remove(patient);
            }
        }
    }
}
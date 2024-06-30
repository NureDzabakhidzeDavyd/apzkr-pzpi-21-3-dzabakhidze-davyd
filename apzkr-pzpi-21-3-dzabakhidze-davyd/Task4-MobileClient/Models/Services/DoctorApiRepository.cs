using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CareWatch.Mobile.Models.Services
{
    public class DoctorApiRepository
    {
        private static List<Doctor> doctors; // Assuming Doctor model exists
        private readonly string baseApiUrl = "http://10.0.2.2:5000";
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public DoctorApiRepository()
        {
            httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Similar methods for Doctor CRUD operations
        // You can implement methods like GetAllDoctorsAsync, GetDoctorByIdAsync, UpdateDoctorAsync, CreateDoctorAsync, and DeleteDoctorAsync

        // Example:
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            try
            {
                var uri = new Uri($"{baseApiUrl}/api/v1/doctors");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                doctors = JsonConvert.DeserializeObject<List<Doctor>>(content);
                return doctors;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Doctor> GetDoctorByIdAsync(Guid doctorId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{baseApiUrl}/api/v1/doctors/{doctorId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Doctor>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Doctor> UpdateDoctorAsync(Guid doctorId, DoctorRequest updateDoctorRequestCommand)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(updateDoctorRequestCommand, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var uri = new Uri($"{baseApiUrl}/api/v1/doctors/{doctorId}");
                var response = await httpClient.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var updatedDoctorContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Doctor>(updatedDoctorContent);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var validationErrors = JsonConvert.DeserializeObject<ValidationErrors>(errorContent);

                    Debug.WriteLine("Validation errors occurred:");
                    foreach (var error in validationErrors.Errors)
                    {
                        Debug.WriteLine($"{error.Key}: {string.Join(", ", error.Value)}");
                    }

                    // You may want to present these errors to the user in your UI
                }
                else
                {
                    // Handle other non-success status codes
                    Console.WriteLine($"Error: {response.StatusCode}");
                }

                return null;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Doctor> CreateDoctorAsync(DoctorRequest createDoctorCommand)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(createDoctorCommand, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{baseApiUrl}/api/v1/doctors", content);

                if (response.IsSuccessStatusCode)
                {
                    var createdDoctorContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Doctor>(createdDoctorContent);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var validationErrors = JsonConvert.DeserializeObject<ValidationErrors>(errorContent);

                    Debug.WriteLine("Validation errors occurred:");
                    foreach (var error in validationErrors.Errors)
                    {
                        Debug.WriteLine($"{error.Key}: {string.Join(", ", error.Value)}");
                    }

                    // You may want to present these errors to the user in your UI
                }
                else
                {
                    // Handle other non-success status codes
                    Console.WriteLine($"Error: {response.StatusCode}");
                }

                return null;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteDoctorAsync(Guid doctorId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseApiUrl}/api/v1/doctors/{doctorId}");

                if (response.IsSuccessStatusCode)
                {
                    // Return success message or perform other actions if needed
                    var deletedDoctorContent = await response.Content.ReadAsStringAsync();
                    return;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var validationErrors = JsonConvert.DeserializeObject<ValidationErrors>(errorContent);

                    Debug.WriteLine("Validation errors occurred:");
                    foreach (var error in validationErrors.Errors)
                    {
                        Debug.WriteLine($"{error.Key}: {string.Join(", ", error.Value)}");
                    }

                    // You may want to present these errors to the user in your UI
                }
                else
                {
                    // Handle other non-success status codes
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}

using CareWatch.Mobile.Models.Entities;
using CareWatch.Mobile.Models.Requests;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CareWatch.Mobile.Models.Services
{
    public class PatientApiRepository
    {
        private static List<Patient> patients;
        private readonly string baseApiUrl = "http://10.0.2.2:5000";
        private readonly HttpClient httpClient;
        JsonSerializerOptions _serializerOptions;

        public PatientApiRepository()
        {
            httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync(SearchFilter searchFilter = null)
        {
            try
            {
                string apiUrl = $"{baseApiUrl}/api/v1/patients/";

                if (searchFilter != null)
                {
                    // Serialize the search filter and append it to the URL
                    var queryString = string.Join("&", searchFilter.GetType()
                        .GetProperties()
                        .Where(prop => prop.GetValue(searchFilter) != null)
                        .Select(prop => $"{prop.Name}={Uri.EscapeDataString(prop.GetValue(searchFilter).ToString())}"));

                    apiUrl += $"?{queryString}";
                }

                var uri = new Uri($"{baseApiUrl}/api/v1/patients");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                patients = JsonConvert.DeserializeObject<List<Patient>>(content);
                return patients;
            }
            catch (Exception ex)
            {
                // Handle the error
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(Guid patientId)
        {
            //try
            //{
            //    var response = await httpClient.GetAsync($"{baseApiUrl}/api/v1/patients/{patientId}");
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<Patient>(content);
            //}
            //catch (Exception ex)
            //{
            //    // Обробити помилку
            //    Console.WriteLine($"Error: {ex.Message}");
            //    return null;
            //}
            return patients.FirstOrDefault(p => p.Id == patientId);
        }

        public async Task<Patient> UpdatePatientAsync(Guid patientId, PatientRequest updatePatientRequestCommand)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(updatePatientRequestCommand, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var uri = new Uri($"{baseApiUrl}/api/v1/patients/{patientId}");
                var response = await httpClient.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var updatedPatientContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Patient>(updatedPatientContent);
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

        public async Task<Patient> CreatePatientAsync(PatientRequest createPatientCommand)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(createPatientCommand, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{baseApiUrl}/api/v1/patients", content);

                if (response.IsSuccessStatusCode)
                {
                    var createdPatientContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Patient>(createdPatientContent);
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

        public async Task DeletePatientAsync(Guid patientId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseApiUrl}/api/v1/patients/{patientId}");

                if (response.IsSuccessStatusCode)
                {
                    // Return the deleted patient if the deletion is successful
                    var deletedPatientContent = await response.Content.ReadAsStringAsync();
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

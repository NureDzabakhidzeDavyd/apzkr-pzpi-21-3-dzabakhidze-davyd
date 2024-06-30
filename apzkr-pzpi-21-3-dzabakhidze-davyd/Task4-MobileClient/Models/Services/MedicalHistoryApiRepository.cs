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
using System.Threading.Tasks;

namespace CareWatch.Mobile.Models.Services
{
    public class MedicalHistoryApiRepository
    {
        private static List<MedicalHistory> medicalHistories; // Assuming MedicalHistory model exists
        private readonly string baseApiUrl = "http://10.0.2.2:5000";
        private readonly HttpClient httpClient;
        private readonly JsonSerializerSettings _serializerSettings;

        public MedicalHistoryApiRepository()
        {
            httpClient = new HttpClient();
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<MedicalHistory>> GetAllMedicalHistoriesAsync()
        {
            try
            {
                var uri = new Uri($"{baseApiUrl}/api/v1/medical-histories");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                medicalHistories = JsonConvert.DeserializeObject<List<MedicalHistory>>(content);
                return medicalHistories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<MedicalHistory> GetMedicalHistoryByIdAsync(Guid medicalHistoryId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{baseApiUrl}/api/v1/medical-histories/{medicalHistoryId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MedicalHistory>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<MedicalHistory> UpdateMedicalHistoryAsync(Guid medicalHistoryId, MedicalHistoryRequest updateMedicalHistoryRequest)
        {
            try
            {
                var json = JsonConvert.SerializeObject(updateMedicalHistoryRequest, _serializerSettings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var uri = new Uri($"{baseApiUrl}/api/v1/medical-histories/{medicalHistoryId}");
                var response = await httpClient.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var updatedMedicalHistoryContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MedicalHistory>(updatedMedicalHistoryContent);
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

        public async Task<MedicalHistory> CreateMedicalHistoryAsync(MedicalHistoryRequest createMedicalHistoryRequest)
        {
            try
            {
                var json = JsonConvert.SerializeObject(createMedicalHistoryRequest, _serializerSettings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{baseApiUrl}/api/v1/medical-histories", content);

                if (response.IsSuccessStatusCode)
                {
                    var createdMedicalHistoryContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MedicalHistory>(createdMedicalHistoryContent);
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

        public async Task DeleteMedicalHistoryAsync(Guid medicalHistoryId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseApiUrl}/api/v1/medical-histories/{medicalHistoryId}");

                if (response.IsSuccessStatusCode)
                {
                    // Return success message or perform other actions if needed
                    var deletedMedicalHistoryContent = await response.Content.ReadAsStringAsync();
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

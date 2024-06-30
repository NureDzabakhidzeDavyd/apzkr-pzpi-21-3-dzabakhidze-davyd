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
    public class EmergencyRequestApiRepository
    {
        private static List<EmergencyRequest> emergencyRequests;
        private readonly string baseApiUrl = "http://10.0.2.2:5000"; // Update with your actual API base URL
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public EmergencyRequestApiRepository()
        {
            httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<EmergencyRequest>> GetAllEmergencyRequestsAsync()
        {
            try
            {
                var uri = new Uri($"{baseApiUrl}/api/v1/emergency-requests");
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                emergencyRequests = JsonConvert.DeserializeObject<List<EmergencyRequest>>(content);
                return emergencyRequests;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<EmergencyRequest> GetEmergencyRequestByIdAsync(Guid emergencyRequestId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{baseApiUrl}/api/v1/emergency-requests/{emergencyRequestId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EmergencyRequest>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<EmergencyRequest> CreateEmergencyRequestAsync(EmergencyRequestRequest createEmergencyRequestCommand)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(createEmergencyRequestCommand, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{baseApiUrl}/api/v1/emergency-requests", content);

                if (response.IsSuccessStatusCode)
                {
                    var createdEmergencyRequestContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EmergencyRequest>(createdEmergencyRequestContent);
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
                    Console.WriteLine($"Error: {response.StatusCode}");
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateEmergencyRequestAsync(Guid emergencyRequestId, EmergencyRequest updateEmergencyRequestCommand)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(updateEmergencyRequestCommand, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var uri = new Uri($"{baseApiUrl}/api/v1/emergency-requests/{emergencyRequestId}");
                var response = await httpClient.PutAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    HandleError(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task DeleteEmergencyRequestAsync(Guid emergencyRequestId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseApiUrl}/api/v1/emergency-requests/{emergencyRequestId}");

                if (!response.IsSuccessStatusCode)
                {
                    HandleError(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async void HandleError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
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
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}

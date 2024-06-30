using CareWatch.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Contact = CareWatch.Mobile.Models.Entities.Contact;

namespace CareWatch.Mobile.Models.Services
{
    public class AuthApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;

        public AuthApiRepository()
        {
            _httpClient = new HttpClient();
            _baseApiUrl = "http://10.0.2.2:5000";
        }

        public async Task<AuthenticatedResponse> LoginAsync(string email, string password)
        {
            try
            {
                var loginUrl = $"{_baseApiUrl}/api/v1/auth/login";
                var loginModel = new { Email = email, Password = password };
                var json = JsonSerializer.Serialize(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(loginUrl, content);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AuthenticatedResponse>(responseBody);
            }
            catch (Exception ex)
            {
                // Обробка помилок або виведення відповідного повідомлення
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Contact> GetUserProfileAsync()
        {
            try
            {
                var profileUrl = $"{_baseApiUrl}/api/v1/auth/profile";
                var accessToken = await GetAccessTokenAsync();

                if (accessToken == null)
                {
                    // Отримання нового токену або виконання іншої обробки, якщо токен відсутній
                    return null;
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(profileUrl);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Contact>(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user profile: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> SaveAccessTokenAsync(string accessToken)
        {
            try
            {
                SecureStorage.Remove("AccessToken"); // Видалення попереднього токену (опціонально)
                SecureStorage.SetAsync("AccessToken", accessToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving access token: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync("AccessToken");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting access token: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var accessToken = await GetAccessTokenAsync();

                // Перевірка наявності токену
                if (string.IsNullOrEmpty(accessToken))
                    return false;

                // Перевірка дійсності токену, можливо, використовуючи декодування JWT і перевірку терміну дії
                var isTokenValid = ValidateAccessToken(accessToken);

                return isTokenValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking authentication status: {ex.Message}");
                return false;
            }
        }

        private bool ValidateAccessToken(string accessToken)
        {
            // Реалізуйте перевірку дійсності токену, можливо, декодування JWT та перевірка терміну дії
            // Повертається true, якщо токен дійсний, і false в іншому випадку
            // Приклад:
            // var handler = new JwtSecurityTokenHandler();
            // var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
            // return jsonToken.ValidTo > DateTime.UtcNow;

            // Для прикладу, завжди повертаємо true, тобто завжди вважаємо токен дійсним
            return true;
        }

    }
}

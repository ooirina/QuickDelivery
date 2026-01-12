using Newtonsoft.Json;
using QuickDelivery.Mobile.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Data
{
    public class RestService
    {
        private readonly HttpClient client;
        private readonly string Url = "http://10.0.2.2:5132/api/"; // Corect pentru emulator Android

        public RestService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            client = new HttpClient(handler);
        }
        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            using var client = new HttpClient();

            var token = Preferences.Get("AuthToken", string.Empty);
            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var json = await client.GetStringAsync("http://10.0.2.2:5132/api/restaurante");
                var lista = JsonConvert.DeserializeObject<List<Restaurant>>(json);
                return lista ?? new List<Restaurant>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Eroare RestService: " + ex.Message);
                return new List<Restaurant>();
            }
        }






        public async Task<List<Produs>> GetProduseByRestaurantAsync(int restaurantId)
        {
            try
            {
                var response = await client.GetAsync($"{Url}Produse/ByRestaurant/{restaurantId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Produs>>(json) ?? new List<Produs>();
                }
                Debug.WriteLine($"Error GET Produse: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception GET Produse: {ex.Message}");
            }
            return new List<Produs>();
        }

       

      

        public async Task<List<Recenzie>> GetRecenziiByRestaurantAsync(int restaurantId)
        {
            try
            {
                var response = await client.GetAsync($"{Url}Recenzies/Restaurant/{restaurantId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Recenzie>>(json) ?? new List<Recenzie>();
                }
                Debug.WriteLine($"Error GET Recenzii: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception GET Recenzii: {ex.Message}");
            }
            return new List<Recenzie>();
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var loginData = new
            {
                Email = email,
                Password = password
            };

            using var client = new HttpClient();
            var json = System.Text.Json.JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://10.0.2.2:5132/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                // primim token-ul din API
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent; // poate fi JWT
            }

            return null;
        }


    }
}

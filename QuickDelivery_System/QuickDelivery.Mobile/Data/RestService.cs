using QuickDelivery.Mobile.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Net.Http;
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
            try
            {
                var response = await client.GetAsync($"{Url}Restaurante");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Restaurant>>(json) ?? new List<Restaurant>();
                }
                Debug.WriteLine($"Error GET Restaurants: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception GET Restaurants: {ex.Message}");
            }
            return new List<Restaurant>();
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

        public async Task<List<Categorie>> GetCategoriesAsync()
        {
            try
            {
                var response = await client.GetAsync($"{Url}Categorii");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Categorie>>(json) ?? new List<Categorie>();
                }
                Debug.WriteLine($"Error GET Categories: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception GET Categories: {ex.Message}");
            }
            return new List<Categorie>();
        }

        public async Task<List<Categorie>> GetCategoriesByRestaurantAsync(int restaurantId)
        {
            try
            {
                var response = await client.GetAsync($"{Url}Categorii/ByRestaurant/{restaurantId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Categorie>>(json) ?? new List<Categorie>();
                }
                Debug.WriteLine($"Error GET CategoriesByRestaurant: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception GET CategoriesByRestaurant: {ex.Message}");
            }
            return new List<Categorie>();
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

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { Email = email, Password = password };
                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{Url}auth/login", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Login: {ex.Message}");
                return false;
            }
        }
    }
}

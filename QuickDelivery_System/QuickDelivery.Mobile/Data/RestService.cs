using QuickDelivery.Mobile.Models;
using System.Text.Json;

namespace QuickDelivery.Mobile.Data
{
    public class RestService
    {
        private readonly HttpClient client;
        private const string BaseAddress = "http://10.0.2.2:5132/api";

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
                var response = await client.GetAsync($"{BaseAddress}/Restaurante");
                if (!response.IsSuccessStatusCode) return new List<Restaurant>();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Restaurant>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return new List<Restaurant>();
            }
        }

        public async Task<List<Produs>> GetProduseByRestaurantAsync(int restaurantId)
        {
            try
            {
                var response = await client.GetAsync($"{BaseAddress}/Produse/ByRestaurant/{restaurantId}");
                if (!response.IsSuccessStatusCode) return new List<Produs>();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Produs>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return new List<Produs>();
            }
        }
    }
}

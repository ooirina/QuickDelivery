
using QuickDelivery.Mobile.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json;           // Pentru JsonConvert (Serializarea obiectelor în JSON)
using System.Text;               // Pentru Encoding.UTF8
using System.Net.Http;           // Pentru HttpClient, StringContent și HttpResponseMessage
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Data
{
    public class RestService
    {
        private readonly HttpClient client;
        string Url = "http://10.0.2.2:5132/api/";

        public RestService()
        {
            var handler = new HttpClientHandler();
            // Permite conexiunea HTTPS pe emulator
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
                    return JsonConvert.DeserializeObject<List<Restaurant>>(json);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
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
                    return JsonConvert.DeserializeObject<List<Produs>>(json);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new List<Produs>();
        }

        public async Task<List<Categorie>> GetCategoriesAsync()
        {
            try
            {
                var response = await client.GetAsync($"{Url}Categorii");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Categorie>>(content);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new List<Categorie>();
        }

        public async Task<List<Categorie>> GetCategoriesByRestaurantAsync(int restaurantId)
        {
            try
            {
                var response = await client.GetAsync(
                    $"{Url}Categorii/ByRestaurant/{restaurantId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Categorie>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Recenzie>>(content);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new List<Recenzie>();
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { Email = email, Password = password };
                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
\
                var response = await client.PostAsync("api/auth/login", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
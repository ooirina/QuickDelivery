
namespace QuickDelivery.Web.Models
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string Adresa { get; set; } = string.Empty;
        public string ImagineUrl { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}

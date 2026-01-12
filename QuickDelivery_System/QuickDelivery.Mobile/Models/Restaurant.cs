using SQLite;

namespace QuickDelivery.Mobile.Models
{
    public class Restaurant
    {
        // ====== Date din API / DB ======

        public int Id { get; set; }

        public string Nume { get; set; } = string.Empty;

        public string Adresa { get; set; } = string.Empty;

        public string ImagineUrl { get; set; } = string.Empty;

        public int CategorieId { get; set; }   // ⚠️ OBLIGATORIU public set

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        // ====== Date calculate local (nu vin din API / DB) ======

        [Ignore] // SQLite nu o salvează
        public double DistantaKm { get; set; }
    }
}

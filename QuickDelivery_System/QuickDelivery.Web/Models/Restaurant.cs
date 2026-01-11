using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuickDelivery.Web.Models
{
    public class Restaurant
    {
        
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele restaurantului este obligatoriu")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 100 de caractere.")]
        [Display(Name = "Nume Restaurant")]
        [JsonPropertyName("nume")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Adresa este necesară pentru livrare.")]
        [StringLength(200)]
        [JsonPropertyName("adresa")]
        public string Adresa { get; set; }

        [Display(Name = "Imagine Restaurant")]
        [JsonPropertyName("imagineUrl")]
        public string? ImagineUrl { get; set; }

        [NotMapped] // Această proprietate nu se salvează în baza de date
            public string DurataLivrare
            {
                get
                {
                    // Generăm o durată între 20 și 50 minute bazată pe ID
                    int start = 20 + (Id % 10);
                    int end = start + 10;
                    return $"{start}-{end} min";
                }
            }

            [NotMapped]
            public double NotaCalculata
            {
                get
                {
                    // Generăm o notă între 4.0 și 4.9
                    return 4.0 + (double)(Id % 10) / 10.0;
                }
            }
        
        // Această proprietate permite accesul la lista de produse din acest restaurant 1:N
        [JsonIgnore]
        public virtual ICollection<Produs>? Produse { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}

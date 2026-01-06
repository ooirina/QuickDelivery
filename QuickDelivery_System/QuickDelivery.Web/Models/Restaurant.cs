using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.Web.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele restaurantului este obligatoriu")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 100 de caractere.")]
        [Display(Name = "Nume Restaurant")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Adresa este necesară pentru livrare.")]
        [StringLength(200)]
        public string Adresa { get; set; }

        [Display(Name = "Imagine Restaurant")]
        public string? ImagineUrl { get; set; }
        // Această proprietate permite accesul la lista de produse din acest restaurant 1:N
        public virtual ICollection<Produs>? Produse { get; set; }
    }
}

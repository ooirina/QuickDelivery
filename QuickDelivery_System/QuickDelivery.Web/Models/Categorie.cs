using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.Web.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nume Categorie")]
        public string Nume { get; set; }

        [Display(Name = "Iconiță (Bootstrap Icon)")]
        public string? Iconita { get; set; } // Exemplu: bi-pizza, bi-cup-straw

        public virtual ICollection<Produs>? Produse { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickDelivery.Web.Models
{
    public class Comanda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data Comenzii")]
        [DataType(DataType.DateTime)]
        public DateTime DataComanda { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Status Comandă")]
        public string Status { get; set; } = "In preparare";// Ex:"In preparare", "In livrare", "Finalizata"

        [Required]
        public int Cantitate { get; set; } = 1;
        
        [Required, StringLength(100)]
        public string AdresaLivrare { get; set; }

        // Legătura cu Clientul
        public int? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }

        [ForeignKey("RestaurantId")]
        public int? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        [ForeignKey("ProdusId")]
        public int? ProdusId { get; set; }
        public Produs? Produs { get; set; }
    }
}
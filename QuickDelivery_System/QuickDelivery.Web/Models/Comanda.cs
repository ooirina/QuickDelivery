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
        public string Status { get; set; } // Ex: "In preparare", "In livrare", "Finalizata"

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPlata { get; set; }

        // Legătura cu Clientul
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public string Statut { get; set; }

        // Relație: O comandă are mai multe produse (prin DetaliiComanda)
        public virtual ICollection<DetaliiComanda>? DetaliiComenzi { get; set; }
        public Restaurant? Restaurant { get; set; }
        public Produs? Produs { get; set; }
    }
}
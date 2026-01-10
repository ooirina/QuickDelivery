using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickDelivery.Web.Models
{
    public class Recenzie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Nota trebuie să fie între 1 și 5.")]
        public int Nota { get; set; }

        [Required(ErrorMessage = "Comentariul este obligatoriu.")]
        [StringLength(500)]
        public string Comentariu { get; set; }

        // Legătura cu Restaurantul
        [ForeignKey("RestaurantId")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

        [ForeignKey("ClientId")]
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}

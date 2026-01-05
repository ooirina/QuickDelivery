using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.Web.Models
{
    public class Recenzie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Nota trebuie să fie între 1 și 5.")]
        public int Nota { get; set; }

        [StringLength(500)]
        public string Comentariu { get; set; }

        // Legătura cu Restaurantul
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}

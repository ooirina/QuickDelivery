
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickDelivery.Web.Models
    {
        public class Produs
        {
            [Key]
            public int Id { get; set; }

        [Required(ErrorMessage = "Produsul trebuie să aibă un nume.")]
        [RegularExpression(@"^[A-Z][a-z/s]*$", ErrorMessage = "Numele trebuie să înceapă cu literă mare.")]
        public string Nume { get; set; }

        [Required]
        [Range(1, 500, ErrorMessage = "Prețul trebuie să fie între 1 și 500 RON.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")] // Important pentru baza de date SQL
        public decimal Pret { get; set; }

        [StringLength(500, ErrorMessage = "Descrierea este prea lungă.")]
        public string Descriere { get; set; }

        // Foreign Key către Restaurant
        public int RestaurantId { get; set; }

            // Proprietatea de navigare - Aici dispare eroarea dacă clasa Restaurant există
            [ForeignKey("RestaurantId")]
            public virtual Restaurant Restaurant { get; set; }
        }
    }
   


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickDelivery.Web.Models
{
    public class DetaliiComanda
    {
        [Key]
        public int Id { get; set; }

        // Legătura cu Comanda
        public int ComandaId { get; set; }

        [ForeignKey("ComandaId")]
        public virtual Comanda Comanda { get; set; }

        // Legătura cu Produsul
        public int ProdusId { get; set; }

        [ForeignKey("ProdusId")]
        public virtual Produs Produs { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Poți comanda maxim 20 de bucăți din același produs.")]
        public int Cantitate { get; set; }
    }
}
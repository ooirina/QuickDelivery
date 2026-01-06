using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.Web.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele complet este obligatoriu.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Nume Client")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Adresa de email este obligatorie.")]
        [EmailAddress(ErrorMessage = "Te rugăm să introduci o adresă de email validă.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Numărul de telefon este obligatoriu.")]
        [Phone(ErrorMessage = "Formatul numărului de telefon este invalid.")]
        [RegularExpression(@"^07[0-9]{8}$", ErrorMessage = "Telefonul trebuie să înceapă cu 07 și să aibă 10 cifre.")]
        public string Telefon { get; set; }

        // Relație: Un client poate avea mai multe comenzi
        public virtual ICollection<Comanda>? Comenzi { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string? Iconita { get; set; }
    }
}

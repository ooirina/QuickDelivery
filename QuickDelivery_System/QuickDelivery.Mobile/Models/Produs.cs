using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Models
{
    public class Produs
    {
        public int Id { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string Descriere { get; set; } = string.Empty;
        public decimal Pret { get; set; }
        public string ImagineUrl { get; set; } = string.Empty;
        public int RestaurantId
        {
            get; set;
        }
    }
}

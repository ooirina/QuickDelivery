using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string Adresa { get; set; } = string.Empty;

        public string ImagineUrl { get; set; } = string.Empty;

    }
}

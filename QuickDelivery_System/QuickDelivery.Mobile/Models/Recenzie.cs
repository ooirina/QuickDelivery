using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Models
{
    public class Recenzie
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Comentariu { get; set; } = string.Empty;
        public int RestaurantId { get; set; }

        public string NumeClient { get; set; } = "Client Anonim";
    }
}

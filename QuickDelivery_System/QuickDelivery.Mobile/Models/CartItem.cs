using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickDelivery.Mobile.Models;

namespace QuickDelivery.Mobile.Models
{
    public class CartItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ProdusId { get; set; }
        public string Nume { get; set; } = string.Empty;
        public decimal Pret { get; set; }
        public int Cantitate { get; set; }
        public int RestaurantId { get; set; } 
        public string RestaurantName { get; set; }
    }
}

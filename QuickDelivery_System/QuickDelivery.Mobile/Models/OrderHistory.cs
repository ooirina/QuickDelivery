using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Models
{
    public class OrderHistory
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string OrderGroupId { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string RestaurantName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

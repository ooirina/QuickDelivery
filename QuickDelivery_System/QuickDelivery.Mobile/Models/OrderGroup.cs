using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDelivery.Mobile.Models
{
    public class OrderGroup: List<OrderHistory>
    {
    public string RestaurantName { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderGroup(string name, DateTime date, List<OrderHistory> items) : base(items)
        {
            RestaurantName = name;
            OrderDate = date;
        }
    }
}

using System;
namespace PointOfSales.Web.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
        }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public int Quantity { get; set; }
    }
}

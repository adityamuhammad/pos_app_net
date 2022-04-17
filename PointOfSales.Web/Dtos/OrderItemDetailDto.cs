using System;
namespace PointOfSales.Web.Dtos
{
    public class OrderItemDetailDto
    {
        public OrderItemDetailDto()
        {
        }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}

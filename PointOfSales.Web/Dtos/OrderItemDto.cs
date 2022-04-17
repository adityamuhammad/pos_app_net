using System;
namespace PointOfSales.Web.Dtos
{
    public class OrderItemDto
    {
        public OrderItemDto()
        {
        }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

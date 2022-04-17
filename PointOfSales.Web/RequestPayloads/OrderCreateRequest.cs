using PointOfSales.Web.Dtos;

namespace PointOfSales.Web.RequestPayloads
{
    public class OrderCreateRequest
    {
        public OrderCreateRequest()
        {
        }
        public OrderItemDto[] OrderItems { get; set; }
    }
}

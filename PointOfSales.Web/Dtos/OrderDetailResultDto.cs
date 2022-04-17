using System;
using System.Collections.Generic;

namespace PointOfSales.Web.Dtos
{
    public class OrderDetailResultDto
    {
        public OrderDetailResultDto()
        {
        }
        public long OrderId { get; set; }
        public string Status { get; set; }
        public IList<OrderItemDetailDto> OrderDetail { get; set; }
    }
}

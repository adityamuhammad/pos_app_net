using System;
using PointOfSales.Web.Models;

namespace PointOfSales.Web.Dtos
{
    public class OrderCreateDto
    {
        public OrderCreateDto()
        {
        }
        public OrderItemDto[] OrderItems { get; set; }
    }
}

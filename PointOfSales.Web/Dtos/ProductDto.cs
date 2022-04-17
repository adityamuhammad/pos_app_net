using System;
namespace PointOfSales.Web.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
namespace PointOfSales.Web.Dtos
{
    public class ProductCreateDto
    {
        public ProductCreateDto()
        {
        }
        public string Name { get; set; }
        public long Price { get; set; }
    }
}

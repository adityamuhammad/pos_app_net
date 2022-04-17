using System;
namespace PointOfSales.Web.Dtos
{
    public class ProductUpdateDto
    {
        public ProductUpdateDto()
        {
        }
        public string Name { get; set; }
        public long Price { get; set; }
        public long Id { get; set; }
    }
}

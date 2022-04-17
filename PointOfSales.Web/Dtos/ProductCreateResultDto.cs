using System;
namespace PointOfSales.Web.Dtos
{
    public class ProductCreateResultDto
    {
        public ProductCreateResultDto()
        {
        }
        public string Name { get; set; }
        public long Price { get; set; }
    }
}

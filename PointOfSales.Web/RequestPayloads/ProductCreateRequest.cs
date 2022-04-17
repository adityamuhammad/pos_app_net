using System;
using System.ComponentModel.DataAnnotations;

namespace PointOfSales.Web.RequestPayloads
{
    public class ProductCreateRequest
    {
        public ProductCreateRequest()
        {
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public long Price { get; set; }
    }
}

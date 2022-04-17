using System;
using System.ComponentModel.DataAnnotations;

namespace PointOfSales.Web.RequestPayloads
{
    public class ProductUpdateRequest
    {
        public ProductUpdateRequest()
        {
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public long Id { get; set; }
    }
}

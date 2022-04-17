using System;
using System.ComponentModel.DataAnnotations;

namespace PointOfSales.Web.RequestPayloads
{
    public class ProductDeleteRequest
    {
        public ProductDeleteRequest()
        {
        }
        [Required]
        public long Id { get; set; }
    }
}

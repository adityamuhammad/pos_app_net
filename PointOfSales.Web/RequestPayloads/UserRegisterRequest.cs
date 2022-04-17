using System;
using System.ComponentModel.DataAnnotations;
namespace PointOfSales.Web.RequestPayloads
{
    public class UserRegisterRequest
    {
        public UserRegisterRequest()
        {
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}

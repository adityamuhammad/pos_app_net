using System;

namespace PointOfSales.Web.Dtos
{
    public class UserRegisterDto
    {
        public UserRegisterDto()
        {
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

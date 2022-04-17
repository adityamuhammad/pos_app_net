using System;
namespace PointOfSales.Web.Dtos
{
    public class UserDto
    {
        public UserDto()
        {
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

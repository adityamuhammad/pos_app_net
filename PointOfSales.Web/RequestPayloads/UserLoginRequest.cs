using System;
namespace PointOfSales.Web.RequestPayloads
{
    public class UserLoginRequest
    {
        public UserLoginRequest()
        {
        }
		public string Name { get; set; }
		public string Password { get; set; }
    }
}

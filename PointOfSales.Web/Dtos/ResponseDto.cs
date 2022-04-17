using System;
namespace PointOfSales.Web.Dtos
{
    public class ResponseDto<T>
    {
        public ResponseDto()
        {
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

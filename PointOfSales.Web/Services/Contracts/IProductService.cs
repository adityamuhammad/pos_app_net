using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PointOfSales.Web.Dtos;

namespace PointOfSales.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<ResponseDto<ProductCreateResultDto>> Create(ProductCreateDto productCreateDto);
        Task<ResponseDto<ProductUpdateResultDto>> Update(ProductUpdateDto productUpdateDtod);
        Task Delete(long id);
        Task<ResponseDto<IEnumerable<ProductDto>>> GetAll();
        Task<ResponseDto<ProductDto>> GetById(long id);
    }
}

using System;
using System.Threading.Tasks;
using PointOfSales.Web.Dtos;

namespace PointOfSales.Web.Services.Contracts
{
    public interface IUserService
    {
        Task<ResponseDto<UserRegisterResultDto>> Register(UserRegisterDto userRegisterDto);
        Task<ResponseDto<UserLoginResultDto>> Login(UserLoginDto userLoginDto);
    }
}

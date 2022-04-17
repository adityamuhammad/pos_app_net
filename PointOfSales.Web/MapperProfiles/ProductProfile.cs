using System;
using AutoMapper;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.RequestPayloads;

namespace PointOfSales.Web.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateRequest, ProductCreateDto>();
            CreateMap<ProductUpdateRequest, ProductUpdateDto>();
        }
    }
}

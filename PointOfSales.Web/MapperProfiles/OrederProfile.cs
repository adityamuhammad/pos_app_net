using System;
using AutoMapper;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.RequestPayloads;

namespace PointOfSales.Web.MapperProfiles
{
    public class OrederProfile : Profile
    {
        public OrederProfile()
        {
            CreateMap<OrderCreateRequest, OrderCreateDto>();
        }
    }
}

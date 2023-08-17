using AffiliatesApi.Data.Entities;
using AffiliatesApi.Model;
using AutoMapper;

namespace AffiliatesApi.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<AffiliateDTO, AffiliateEntity>();
            CreateMap<AffiliateEntity, AffiliateDTO>();
        }
    }
}


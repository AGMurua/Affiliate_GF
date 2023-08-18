using AffiliatesApi.Data.Entities;
using AffiliatesApi.Model;
using AutoMapper;

namespace AffiliatesApi.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AffiliateDTO, AffiliateEntity>();
            CreateMap<AffiliateEntity, AffiliateDTO>();
            CreateMap<AffiliateWithRelationsDTO, AffiliateEntity>();
            CreateMap<AffiliateEntity, AffiliateWithRelationsDTO>()
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.Customers));

            CreateMap<CustomerEntity, CustomerDTO>();
            CreateMap<CustomerDTO, CustomerEntity>()
                .ForMember(x => x.Affiliate, opt => opt.Ignore());

            CreateMap<CustomerCreateDTO, CustomerEntity>()
                .ForMember(x => x.Affiliate, opt => opt.Ignore());

            CreateMap<CustomerEntity, CustomerCreateDTO>();
            CreateMap<IEnumerable<AffiliateEntity>, IEnumerable<AffiliateWithRelationsDTO>>();

        }
    }
}


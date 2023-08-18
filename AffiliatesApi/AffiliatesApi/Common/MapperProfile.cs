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
            CreateMap<AffiliateEntity, AffiliateDTO>()
                     .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.Customers));
            CreateMap<CustomerEntity, CustomerDTO>();
            CreateMap<CustomerDTO, CustomerEntity>()
                .ForMember(x => x.Affiliate, opt => opt.Ignore());
            CreateMap<CustomerCreateDTO, CustomerEntity>()
                                .ForMember(x => x.Affiliate, opt => opt.Ignore());

            CreateMap<CustomerEntity, CustomerCreateDTO>();
            CreateMap<IEnumerable<AffiliateEntity>, IEnumerable<AffiliateDTO>>();

        }
    }
}


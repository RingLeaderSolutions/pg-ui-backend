using AutoMapper;
using RLS.PortfolioGeneration.Persistence.Model.Clients;
using RLS.PortfolioGeneration.Persistence.Model.Pricing;

namespace RLS.PortfolioGeneration.FrontendBackend.Dtos
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();

            CreateMap<TenancyPeriod, TenancyPeriodDto>();
            CreateMap<TenancyPeriodDto, TenancyPeriod>();

            CreateMap<Site, SiteWithMetersDto>();
            CreateMap<Site, SiteDto>();
            CreateMap<SiteDto, Site>();

            CreateMap<Mpan, MpanDto>();
            CreateMap<MpanDto, Mpan>();

            CreateMap<Mprn, MprnDto>();
            CreateMap<MprnDto, Mprn>();

            CreateMap<Portfolio, PortfolioDto>();
            CreateMap<PortfolioMeter, PortfolioMeterDto>();
        }
    }
}
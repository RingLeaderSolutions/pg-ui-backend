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
            CreateMap<TenancyPeriod, TenancyPeriodDto>();
            CreateMap<Site, SiteDto>();
            CreateMap<Mpan, MpanDto>();
            CreateMap<Mprn, MprnDto>();

            CreateMap<Portfolio, PortfolioDto>();
            CreateMap<PortfolioMeter, PortfolioMeterDto>();
        }
    }
}
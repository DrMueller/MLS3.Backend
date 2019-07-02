using AutoMapper;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;

namespace Mmu.Mls3.WebApi.Areas.Web.Dtos.Profiles
{
    public class LearningSessionOverviewEntryDtoProfile : Profile
    {
        public LearningSessionOverviewEntryDtoProfile()
        {
            CreateMap<LearningSession, LearningSessionOverviewEntryDto>()
                .ForMember(d => d.SessionName, c => c.MapFrom(f => f.SessionName))
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id));
        }
    }
}
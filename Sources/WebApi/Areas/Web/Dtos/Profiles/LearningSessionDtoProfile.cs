using System.Linq;
using AutoMapper;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;

namespace Mmu.Mls3.WebApi.Areas.Web.Dtos.Profiles
{
    public class LearningSessionDtoProfile : Profile
    {
        public LearningSessionDtoProfile()
        {
            CreateMap<LearningSession, LearningSessionDto>()
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.SessionName, c => c.MapFrom(f => f.SessionName))
                .ForMember(d => d.FactIds, c => c.MapFrom(f => f.LearningSessionFacts.Select(fact => fact.FactId).ToList()));

            CreateMap<LearningSessionDto, LearningSession>()
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.SessionName, c => c.MapFrom(f => f.SessionName));
        }
    }
}
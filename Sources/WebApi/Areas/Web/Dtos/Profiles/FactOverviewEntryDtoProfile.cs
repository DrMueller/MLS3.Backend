using System.Linq;
using AutoMapper;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;

namespace Mmu.Mls3.WebApi.Areas.Web.Dtos.Profiles
{
    public class FactOverviewEntryDtoProfile : Profile
    {
        public FactOverviewEntryDtoProfile()
        {
            CreateMap<Fact, FactOverviewEntryDto>()
                .ForMember(d => d.CreationDateDescription, c => c.MapFrom(f => f.CreationDate.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.ExistsInRun, c => c.MapFrom(f => f.LearningSessionFacts.Any()))
                .ForMember(d => d.QuestionText, c => c.MapFrom(f => f.QuestionText));
        }
    }
}
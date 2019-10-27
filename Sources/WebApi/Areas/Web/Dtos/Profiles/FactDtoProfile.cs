using System.Linq;
using AutoMapper;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;

namespace Mmu.Mls3.WebApi.Areas.Web.Dtos.Profiles
{
    public class FactDtoProfile : Profile
    {
        public FactDtoProfile()
        {
            CreateMap<FactDto, Fact>()
                .ForMember(d => d.AnswerText, c => c.MapFrom(f => f.AnswerText))
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.QuestionText, c => c.MapFrom(f => f.QuestionText));

            CreateMap<Fact, FactDto>()
                .ForMember(d => d.AnswerText, c => c.MapFrom(f => f.AnswerText))
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.QuestionText, c => c.MapFrom(f => f.QuestionText))
                .ForMember(d => d.CreationDate, c => c.MapFrom(f => f.CreationDate))
                .ForMember(d => d.LearningSessionIds, c => c.MapFrom(f => f.LearningSessionFacts.Select(ls => ls.LearningSessionId).ToList()));
        }
    }
}
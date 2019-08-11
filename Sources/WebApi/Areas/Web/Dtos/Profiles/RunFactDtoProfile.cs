using AutoMapper;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;

namespace Mmu.Mls3.WebApi.Areas.Web.Dtos.Profiles
{
    public class RunFactDtoProfile : Profile
    {
        public RunFactDtoProfile()
        {
            CreateMap<Fact, RunFactDto>()
                .ForMember(d => d.AnswerText, c => c.MapFrom(f => f.AnswerText))
                .ForMember(d => d.QuestionText, c => c.MapFrom(f => f.QuestionText));
        }
    }
}
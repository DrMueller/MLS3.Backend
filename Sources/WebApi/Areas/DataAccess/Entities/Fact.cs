using System;
using System.Collections.Generic;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Entities.Base;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Entities
{
    public class Fact : EntityBase
    {
        public string AnswerText { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<LearningSessionFact> LearningSessionFacts { get; set; }
        public string QuestionText { get; set; }
    }
}
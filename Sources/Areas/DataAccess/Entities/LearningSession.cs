using System.Collections.Generic;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Entities.Base;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Entities
{
    public class LearningSession : EntityBase
    {
        public ICollection<LearningSessionFact> LearningSessionFacts { get; set; }
        public string SessionCategory { get; set; }
        public string SessionName { get; set; }
    }
}
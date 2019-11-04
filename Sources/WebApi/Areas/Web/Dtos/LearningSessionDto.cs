using System.Collections.Generic;

namespace Mmu.Mls3.WebApi.Areas.Web.Dtos
{
    public class LearningSessionDto
    {
        public List<long> FactIds { get; set; }
        public long? Id { get; set; }
        public string SessionName { get; set; }
    }
}
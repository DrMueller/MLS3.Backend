namespace Mmu.Mls3.WebApi.Areas.DataAccess.Entities
{
    public class LearningSessionFact
    {
        public Fact Fact { get; set; }
        public long FactId { get; set; }
        public LearningSession LearningSession { get; set; }
        public long LearningSessionId { get; set; }
    }
}
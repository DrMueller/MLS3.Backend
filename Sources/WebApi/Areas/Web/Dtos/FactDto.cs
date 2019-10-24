using System;

namespace Mmu.Mls3.WebApi.Areas.Web.Dtos
{
    public class FactDto
    {
        public string AnswerText { get; set; }
        public DateTime CreationDate { get; set; }
        public long? Id { get; set; }
        public string QuestionText { get; set; }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Areas.DataAccess.Repositories;
using Mmu.Mls3.WebApi.Areas.Web.Dtos;

namespace Mmu.Mls3.WebApi.Areas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ////[Authorize]
    [AllowAnonymous]
    public class LearningSessionsController : ControllerBase
    {
        private readonly ILearningSessionRepository _learningSessionRepo;
        private readonly IMapper _mapper;

        public LearningSessionsController(ILearningSessionRepository learningSessionRepo, IMapper mapper)
        {
            _learningSessionRepo = learningSessionRepo;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLearningSessionAsync(long sessionId)
        {
            await _learningSessionRepo.DeleteAsync(sessionId);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<LearningSessionOverviewEntryDto>>> GetOverviewAsync()
        {
            var allSessions = await _learningSessionRepo.LoadAllAsync();
            var result = _mapper.Map<List<LearningSessionOverviewEntryDto>>(allSessions);
            return result;
        }

        [HttpGet("edit/{id}")]
        public async Task<ActionResult> LoadEditSessionAsync(long sessionId)
        {
            var entity = await _learningSessionRepo.LoadByIdAsync(sessionId);
            var result = _mapper.Map<LearningSessionEditEntryDto>(entity);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> SaveSessionAsync([FromBody] LearningSessionEditEntryDto dto)
        {
            var entity = _mapper.Map<LearningSession>(dto);
            await _learningSessionRepo.SaveAsync(entity);
            return Ok();
        }
    }
}
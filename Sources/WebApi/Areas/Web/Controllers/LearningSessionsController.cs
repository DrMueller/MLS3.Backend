using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Areas.DataAccess.Repositories;
using Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Implementation;
using Mmu.Mls3.WebApi.Areas.Web.Dtos;

namespace Mmu.Mls3.WebApi.Areas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LearningSessionsController : ControllerBase
    {
        private readonly IFactRepository _factRepo;
        private readonly ILearningSessionRepository _learningSessionRepo;
        private readonly IMapper _mapper;

        public LearningSessionsController(
            ILearningSessionRepository learningSessionRepo,
            IFactRepository factRepo,
            IMapper mapper)
        {
            _learningSessionRepo = learningSessionRepo;
            _factRepo = factRepo;
            _mapper = mapper;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAllLearningSessionsAsync()
        {
            await _learningSessionRepo.DeleteAllAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> DeleteLearningSessionAsync(long id)
        {
            await _learningSessionRepo.DeleteAsync(id);
            return Ok(id);
        }

        [HttpGet("{id}/next")]
        [AllowAnonymous]
        public async Task<ActionResult<long>> LoadNextRunFactsAsync(long id)
        {
            var nextSessionId = await _learningSessionRepo.LoadNextIdAsync(id);
            return Ok(nextSessionId);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyCollection<LearningSessionOverviewEntryDto>>> GetOverviewAsync()
        {
            var allSessions = await _learningSessionRepo.LoadAllAsync();
            var result = _mapper.Map<List<LearningSessionOverviewEntryDto>>(allSessions);
            return result;
        }

        [HttpGet("overview/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<LearningSessionOverviewEntryDto>> GetOverviewEntryByIdAsync(long id)
        {
            var session = await _learningSessionRepo.LoadByIdAsync(id);
            var result = _mapper.Map<LearningSessionOverviewEntryDto>(session);
            return Ok(result);
        }

        [HttpGet("edit/{id}")]
        public async Task<ActionResult> LoadEditSessionAsync(long id)
        {
            var entity = await _learningSessionRepo.LoadByIdAsync(id);
            var result = _mapper.Map<LearningSessionEditEntryDto>(entity);
            return Ok(result);
        }

        [HttpGet("{id}/runfacts")]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyCollection<RunFactDto>>> LoadRunFactsAsync(long id)
        {
            var session = await _learningSessionRepo.LoadByIdAsync(id);
            var factIds = session.LearningSessionFacts.Select(f => f.FactId).ToList();
            var facts = await _factRepo.LoadByIdsAsync(factIds);

            var result = _mapper.Map<List<RunFactDto>>(facts);
            return result;
        }

        [HttpPut("edit")]
        public async Task<ActionResult> SaveSessionAsync([FromBody] LearningSessionEditEntryDto dto)
        {
            var entity = _mapper.Map<LearningSession>(dto);
            entity.LearningSessionFacts = dto.FactIds.Select(id => new LearningSessionFact { FactId = id }).ToList();

            if (entity.LearningSessionFacts == null)
            {
                entity.LearningSessionFacts = new List<LearningSessionFact>();
            }

            var returnedEntry = await _learningSessionRepo.SaveAsync(entity);
            var result = _mapper.Map<LearningSessionEditEntryDto>(returnedEntry);
            return Ok(result);
        }
    }
}
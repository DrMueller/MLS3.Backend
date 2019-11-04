using System.Collections.Generic;
using System.Linq;
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
    [Authorize]
    public class LearningSessionsController : ControllerBase
    {
        private readonly ILearningSessionRepository _learningSessionRepo;
        private readonly IMapper _mapper;

        public LearningSessionsController(
            ILearningSessionRepository learningSessionRepo,
            IMapper mapper)
        {
            _learningSessionRepo = learningSessionRepo;
            _mapper = mapper;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAllAsync()
        {
            await _learningSessionRepo.DeleteAllAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> DeleteAsync(long id)
        {
            await _learningSessionRepo.DeleteAsync(id);
            return Ok(id);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<LearningSessionDto>> GetByIdAsync(long id)
        {
            var session = await _learningSessionRepo.LoadByIdAsync(id);
            var result = _mapper.Map<LearningSessionDto>(session);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyCollection<LearningSessionDto>>> GetOverviewAsync()
        {
            var allSessions = await _learningSessionRepo.LoadAllAsync();
            var result = _mapper.Map<List<LearningSessionDto>>(allSessions);
            return result;
        }

        [HttpGet("{id}/next")]
        [AllowAnonymous]
        public async Task<ActionResult<long>> LoadNextIdAsync(long id)
        {
            var nextSessionId = await _learningSessionRepo.LoadNextIdAsync(id);
            return Ok(nextSessionId);
        }

        [HttpPut]
        public async Task<ActionResult<LearningSessionDto>> SaveAsync([FromBody] LearningSessionDto dto)
        {
            var entity = _mapper.Map<LearningSession>(dto);
            entity.LearningSessionFacts = dto.FactIds.Select(id => new LearningSessionFact { FactId = id }).ToList();

            var returnedEntry = await _learningSessionRepo.SaveAsync(entity);
            var result = _mapper.Map<LearningSessionDto>(returnedEntry);
            return Ok(result);
        }
    }
}
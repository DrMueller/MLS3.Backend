using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Implementation;
using Mmu.Mls3.WebApi.Areas.Web.Dtos;

namespace Mmu.Mls3.WebApi.Areas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FactsController : ControllerBase
    {
        private readonly IFactRepository _factRepo;
        private readonly IMapper _mapper;

        public FactsController(IFactRepository factRepository, IMapper mapper)
        {
            _factRepo = factRepository;
            _mapper = mapper;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAllFactsAsync()
        {
            await _factRepo.DeleteAllAsync();
            return NoContent();>
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> DeleteFactAsync(long id)
        {
            await _factRepo.DeleteAsync(id);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<FactOverviewEntryDto>>> GetOverviewAsync()
        {
            var allFacts = await _factRepo.LoadAllAsync();
            var result = _mapper.Map<List<FactOverviewEntryDto>>(allFacts);
            return result;
        }

        [HttpGet("overview/{id}")]
        public async Task<ActionResult<LearningSessionOverviewEntryDto>> GetOverviewEntryByIdAsync(long id)
        {
            var fact = await _factRepo.LoadByIdAsync(id);
            var result = _mapper.Map<FactOverviewEntryDto>(fact);
            return Ok(result);
        }

        [HttpGet("edit/{id}")]
        public async Task<ActionResult> LoadEditFactAsync(long id)
        {
            var entity = await _factRepo.LoadByIdAsync(id);
            var result = _mapper.Map<FactEditEntryDto>(entity);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<FactEditEntryDto>> SaveFactAsync([FromBody] FactEditEntryDto dto)
        {
            Fact entity;

            if (dto.Id.HasValue)
            {
                entity = await _factRepo.LoadByIdAsync(dto.Id.Value);
                entity.AnswerText = dto.AnswerText;
                entity.QuestionText = dto.QuestionText;
            }
            else
            {
                entity = _mapper.Map<Fact>(dto);
                entity.CreationDate = DateTime.Now;
            }

            var savedEntity = await _factRepo.SaveAsync(entity);
            var result = _mapper.Map<FactEditEntryDto>(savedEntity);
            return Ok(result);
        }
    }
}
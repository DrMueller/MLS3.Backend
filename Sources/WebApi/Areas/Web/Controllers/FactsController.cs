using System;
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
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> DeleteFactAsync(long id)
        {
            await _factRepo.DeleteAsync(id);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<FactDto>>> GetAllAsync()
        {
            var allFacts = await _factRepo.LoadAllAsync();
            var result = _mapper.Map<List<FactDto>>(allFacts);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FactDto>> GetByIdAsync(long id)
        {
            var fact = await _factRepo.LoadByIdAsync(id);
            var result = _mapper.Map<FactDto>(fact);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<FactDto>> SaveAsync([FromBody] FactDto dto)
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
            var result = _mapper.Map<FactDto>(savedEntity);
            return Ok(result);
        }
    }
}
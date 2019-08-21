using System;
using System.Collections.Generic;
using System.Linq;
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
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFactAsync(long id)
        {
            await _factRepo.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<FactOverviewEntryDto>>> GetOverviewAsync()
        {
            var allFacts = await _factRepo.LoadAllAsync();
            var result = _mapper.Map<List<FactOverviewEntryDto>>(allFacts);
            return result;
        }

        [HttpGet("edit/{id}")]
        public async Task<ActionResult> LoadEditFactAsync(long id)
        {
            var entity = await _factRepo.LoadByIdAsync(id);
            var result = _mapper.Map<FactEditEntryDto>(entity);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> SaveFactAsync([FromBody] FactEditEntryDto dto)
        {
            var entity = _mapper.Map<Fact>(dto);
            if (!entity.Id.HasValue)
            {
                entity.CreationDate = DateTime.Now;
            }

            await _factRepo.SaveAsync(entity);
            return Ok();
        }
    }
}
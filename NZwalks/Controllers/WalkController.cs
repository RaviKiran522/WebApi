using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using NZwalks.Repositories;

namespace NZwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        public WalkController(IWalkRepository _walkRepository, IMapper _mapper)
        {
            this._walkRepository = _walkRepository;
            this._mapper = _mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {
            var walkData = this._mapper.Map<Walk>(createWalkDto);
            await this._walkRepository.CreateWalkAsync(walkData);
            return Ok(this._mapper.Map<WalkDto>(walkData));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await this._walkRepository.GetAllWalkAsync();

            return Ok(this._mapper.Map<List<WalkDto>>(data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await this._walkRepository.GetWalkAsync(id);

            return Ok(this._mapper.Map<WalkDto>(data));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CreateWalkDto createWalkDto, [FromRoute] Guid id)
        {

            var data = await this._walkRepository.UpdateWalkAsync(this._mapper.Map<Walk>(createWalkDto), id);

            return Ok(this._mapper.Map<WalkDto>(data));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await this._walkRepository.DeleteWalkAsync(id);

            return Ok(this._mapper.Map<WalkDto>(data));
        }

    }
}

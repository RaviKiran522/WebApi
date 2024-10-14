using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.CustomActionFilter;
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
        [ValidateModelAttributes] // Custom validator, which validates ModelState Class
        [Authorize(Roles = "Writer,Reader")]

        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {
            var walkData = this._mapper.Map<Walk>(createWalkDto);
            await this._walkRepository.CreateWalkAsync(walkData);
            return Ok(this._mapper.Map<WalkDto>(walkData));
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? FilterOn, [FromQuery] string? FilterQuery, [FromQuery] bool? isAssending, [FromQuery] int? PageNumber, [FromQuery] int? PageSize)
        {
            var data = await this._walkRepository.GetAllWalkAsync(FilterOn, FilterQuery, isAssending, PageNumber, PageSize);

            return Ok(this._mapper.Map<List<WalkDto>>(data));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await this._walkRepository.GetWalkAsync(id);

            return Ok(this._mapper.Map<WalkDto>(data));
        }

        [HttpPut("{id}")]
        [ValidateModelAttributes]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromBody] CreateWalkDto createWalkDto, [FromRoute] Guid id)
        {

            var data = await this._walkRepository.UpdateWalkAsync(this._mapper.Map<Walk>(createWalkDto), id);

            return Ok(this._mapper.Map<WalkDto>(data));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await this._walkRepository.DeleteWalkAsync(id);

            return Ok(this._mapper.Map<WalkDto>(data));
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Data;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using NZwalks.Repositories;

namespace NZwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        public RegionsController(IRegionRepository _regionRepository)
        {
           this._regionRepository = _regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var regions = await _regionRepository.GetAllAsync();
            var regionDtos = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDtos.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            return Ok(regionDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _regionRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            var regionModel = new Region
            {
                Name = createRegionDto.Name,
                Code = createRegionDto.Code,
                RegionImageUrl = createRegionDto.RegionImageUrl
            };

            var savedRegion = await _regionRepository.CreateAsync(regionModel);

            var resultDto = new RegionDto
            {
                Id = savedRegion.Id,
                Name = savedRegion.Name,
                Code = savedRegion.Code,
                RegionImageUrl = savedRegion.RegionImageUrl
            };

            return Ok(resultDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] CreateRegionDto createRegionDto, [FromRoute] Guid id)
        {
            var updatedData = await _regionRepository.UpdateAsync(createRegionDto,id);

            if(updatedData == null)
            {
                return NotFound();
            }

            return Ok(new RegionDto
            {
                RegionImageUrl = updatedData.RegionImageUrl,
                Name = updatedData.Name,
                Code = updatedData.Code,
                Id = updatedData.Id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _regionRepository.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

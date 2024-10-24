using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Data;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using NZwalks.Repositories;
using System.Text.Json;

namespace NZwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> iLogger;

        public RegionsController(IRegionRepository _regionRepository, IMapper _mapper, ILogger<RegionsController> iLogger)
        {
           this._regionRepository = _regionRepository;
            this._mapper = _mapper;
            this.iLogger = iLogger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            iLogger.LogInformation("Get all region action method was invocked");
            var regions = await _regionRepository.GetAllAsync();

            //Convert Regions to RegionsDto model with mapper
            var regionDtos = this._mapper.Map<List<RegionDto>>(regions);

            //Convert Regions to RegionsDto model without mapper
            //var regionDtos = new List<RegionDto>();
            //foreach (var region in regions)
            //{
            //    regionDtos.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            iLogger.LogInformation($"Finished get all region action with result {JsonSerializer.Serialize(regionDtos)}");

            return Ok(regionDtos);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _regionRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            //Convert Region model to RegionDto 
            var resultDto = this._mapper.Map<RegionDto>(result);
            return Ok(resultDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Writer,Reader")]

        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            if (ModelState.IsValid) { 
            
            //Convert CreateRegionDto to Region domain model
            var regionModel = this._mapper.Map<Region>(createRegionDto);
            //var regionModel = new Region
            //{
            //    Name = createRegionDto.Name,
            //    Code = createRegionDto.Code,
            //    RegionImageUrl = createRegionDto.RegionImageUrl
            //};
            regionModel.Id = Guid.NewGuid();//this is added due to stored proc implementation, Id is not generating so...
            var savedRegion = await _regionRepository.CreateAsync(regionModel);

            //Convert Region domain model to RegionDto
            var resultDto = this._mapper.Map<RegionDto>(savedRegion);
            //var resultDto = new RegionDto
            //{
            //    Id = savedRegion.Id,
            //    Name = savedRegion.Name,
            //    Code = savedRegion.Code,
            //    RegionImageUrl = savedRegion.RegionImageUrl
            //};

            return Ok(resultDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromBody] CreateRegionDto createRegionDto, [FromRoute] Guid id)
        {
            if (ModelState.IsValid)
            {
                var updatedData = await _regionRepository.UpdateAsync(createRegionDto, id);

                if (updatedData == null)
                {
                    return NotFound();
                }
                //convert Region model to RegionDto
                var resultDto = this._mapper.Map<RegionDto>(updatedData);

                return Ok(resultDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _regionRepository.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var resultDto = this._mapper.Map<RegionDto>(result);
            return Ok(resultDto);
        }
    }
}

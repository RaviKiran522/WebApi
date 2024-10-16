﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        public RegionsController(IRegionRepository _regionRepository, IMapper _mapper)
        {
           this._regionRepository = _regionRepository;
            this._mapper = _mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {

            var regions = await _regionRepository.GetAllAsync();
            //Convert Regions to RegionsDto model
            var regionDtos = this._mapper.Map<List<RegionDto>>(regions);
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
        [Authorize(Roles = "Writer,Reader")]

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

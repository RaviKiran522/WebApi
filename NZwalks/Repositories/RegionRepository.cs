using Microsoft.EntityFrameworkCore;
using NZwalks.Data;
using NZwalks.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Models.DTOs;

namespace NZwalks.Repositories
{
    public class RegionRepository : IRegionRepository
    {

        private readonly NZWalksContext _nZWalksContext;
        public RegionRepository(NZWalksContext _nZWalksContext)
        {
            this._nZWalksContext = _nZWalksContext;
        }


        public async Task<Region> CreateAsync(Region region)
        {
            await this._nZWalksContext.Regions.AddAsync(region);
            await this._nZWalksContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid regionId)
        {
            var result = await _nZWalksContext.Regions.FirstOrDefaultAsync(x => x.Id == regionId);
            if (result == null)
            {
                return null;
            }
            _nZWalksContext.Regions.Remove(result);
            await _nZWalksContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            var regionList = await this._nZWalksContext.Regions.ToListAsync();
            if (regionList.Count == 0)
            {
                return new List<Region>();
            }
            return regionList;
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var result = await _nZWalksContext.Regions.FindAsync(id);
            return result;
        }

        public async Task<Region?> UpdateAsync(CreateRegionDto region, Guid id)
        {
            var findItem = await _nZWalksContext.Regions.FindAsync(id);
            if (findItem == null)
            {
                return null;
            }
            findItem.Name = region.Name;
            findItem.Code = region.Code;
            findItem.RegionImageUrl = region.RegionImageUrl;
            await _nZWalksContext.SaveChangesAsync();
            return findItem;
        }
    }
}

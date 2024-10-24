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
            //save data with dbset methods
            //await this._nZWalksContext.Regions.AddAsync(region);
            //await this._nZWalksContext.SaveChangesAsync();

            //insert data with stored procedure
            this._nZWalksContext.Database.ExecuteSqlRaw("EXEC InsertRegion @Id = {0} , @Code = {1}, @Name = {2}, @RegionImageUrl = {3}", region.Id, region.Name, region.Code, region.RegionImageUrl != null ? region.RegionImageUrl : "");
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
            //calling data with dbset methods
            //var regionList = await this._nZWalksContext.Regions.ToListAsync();

            //calling data with stored procedure
            var regionList = this._nZWalksContext.Regions.FromSqlRaw("EXEC GetAllRegions").ToList();
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

using Microsoft.EntityFrameworkCore;
using NZwalks.Data;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using System.Linq;

namespace NZwalks.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private NZWalksContext _dbContext;
        public WalkRepository(NZWalksContext dbContext)
        {
           this._dbContext = dbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk createWalk)
        {
            await _dbContext.Walks.AddAsync(createWalk);
            await _dbContext.SaveChangesAsync();
            return createWalk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
           var findItem = await this._dbContext.Walks.FindAsync(id);
            if (findItem != null)
            {
                _dbContext.Walks.Remove(findItem);
                await _dbContext.SaveChangesAsync();
            }
            return findItem;
        }

        public async Task<List<Walk>> GetAllWalkAsync(string? FilterOn = null, string? FilterQuery = null, bool? isAssending = true, int? PageNumber = 1, int? PageSize = 1000)
        {
            var WalkData = _dbContext.Walks.AsQueryable();

            //Filtering
            if (!(string.IsNullOrEmpty(FilterOn) && string.IsNullOrEmpty(FilterQuery)))
            {
                if(FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    WalkData = WalkData.Where(x => x.Name.Contains(FilterQuery));
                }
            }

            //sorting
            WalkData = isAssending == null ||isAssending == true ? WalkData.OrderBy(x => x.Name) : WalkData.OrderByDescending(x => x.Name);

            //Pagination

            var PageData = (PageNumber - 1) * PageSize ?? 1;
            return await WalkData.Skip(PageData).Take(PageSize ?? 1000).ToListAsync();

        }

        public async Task<Walk?> GetWalkAsync(Guid id)
        {
            var result = await _dbContext.Walks.FindAsync(id);
            return result;
        }

        public async Task<Walk?> UpdateWalkAsync(Walk walk, Guid id)
        {
            var findItem = await _dbContext.Walks.FindAsync(id);
            if (findItem == null)
            {
                return null;
            }
            findItem.Name = walk.Name;
            findItem.Description = walk.Description;
            findItem.LengthInKm = walk.LengthInKm;
            findItem.WalkImageUrl = walk.WalkImageUrl;
            findItem.DifficultyId = walk.DifficultyId;
            findItem.RegionId = walk.RegionId;
            findItem.WalkDifficulty = walk.WalkDifficulty;
            findItem.WalkRegion = walk.WalkRegion;
            await _dbContext.SaveChangesAsync();
            return findItem;
        }
    }
}

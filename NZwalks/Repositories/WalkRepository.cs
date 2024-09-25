using Microsoft.EntityFrameworkCore;
using NZwalks.Data;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

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

        public async Task<List<Walk>> GetAllWalkAsync()
        {
            var WalkData = await _dbContext.Walks.ToListAsync();
            if (WalkData.Count == 0)
            {
                return new List<Walk>();
            }
            return WalkData;
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

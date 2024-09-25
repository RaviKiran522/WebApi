using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

namespace NZwalks.Repositories
{
    public interface IWalkRepository
    {
        public Task<Walk> CreateWalkAsync(Walk createWalk);
        public Task<Walk?> UpdateWalkAsync(Walk walk, Guid id);
        public Task<Walk?> DeleteWalkAsync(Guid id);
        public Task<List<Walk>> GetAllWalkAsync();
        public Task<Walk?> GetWalkAsync(Guid id);
    }
}

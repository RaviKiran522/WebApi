using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

namespace NZwalks.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllAsync();

        public Task<Region?> GetByIdAsync(Guid id);

        public Task<Region> CreateAsync(Region region);

        public Task<Region?> UpdateAsync(CreateRegionDto region, Guid id);

        public Task<Region?> DeleteAsync(Guid regionId);
    }
}

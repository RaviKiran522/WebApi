using NZwalks.Models.Domain;

namespace NZwalks.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);

    }
}

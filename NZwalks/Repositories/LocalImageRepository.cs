using NZwalks.Data;
using NZwalks.Models.Domain;

namespace NZwalks.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksContext nZWalksContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksContext nZWalksContext)
        {
            WebHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZWalksContext = nZWalksContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(WebHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");
            
            //upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/Images/image.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;

            //Add Image to Images table
            await this.nZWalksContext.Images.AddAsync(image);
            await this.nZWalksContext.SaveChangesAsync();

            return image;
        }
    }
}

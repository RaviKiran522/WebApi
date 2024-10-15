using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using NZwalks.Repositories;

namespace NZwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            validateFileUpload(imageUploadRequestDto);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtention = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileName = imageUploadRequestDto.FileName,
                    FileSizeByBytes = imageUploadRequestDto.File.Length,
                    FileDescription = imageUploadRequestDto.FileDescription,
                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void validateFileUpload(ImageUploadRequestDto imageUploadRequestDto) {
            var allowedExtention = new string[]
            {
                ".jpeg", ".jpg", ".png"
            };
            if(!allowedExtention.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file extention");
            }
            if(imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size more than 10MB, Please upload smaller size file");
            }
        }
    }
}

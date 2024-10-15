using System.ComponentModel.DataAnnotations.Schema;

namespace NZwalks.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        public string FileExtention { get; set; }

        public long FileSizeByBytes { get; set; }

        public string FilePath { get; set; }
    }
}

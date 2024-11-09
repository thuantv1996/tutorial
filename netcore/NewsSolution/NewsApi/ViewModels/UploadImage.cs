using System.ComponentModel.DataAnnotations;

namespace NewsApi.ViewModels
{
    public class UploadImage
    {
        [Required]
        public Guid NewsId { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}

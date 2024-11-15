using NewsApi.Extensions.CustomTypes;
using NewsApi.Extensions.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [DateRange(InvalidTypeError = "Is not a date range")]
        public string ImageLink { get; set; }

        [DateRange]
        [Required]
        public DateRange DateShow { get; set; }
    }
}

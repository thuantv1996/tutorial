using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public List<NewsDTO> News { get; set; } = new List<NewsDTO>();
    }
}

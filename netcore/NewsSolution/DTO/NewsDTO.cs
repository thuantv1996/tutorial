using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class NewsDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public Guid? CategoryId { get; set; }
    }
}

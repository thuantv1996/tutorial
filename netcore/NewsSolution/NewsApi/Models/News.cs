namespace NewsApi.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageLink { get; set; }
    }
}

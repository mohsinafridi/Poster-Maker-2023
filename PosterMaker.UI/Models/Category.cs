namespace PosterMaker.UI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AppId { get; set; }
        public string? ThumbnailPath { get; set; }
        public DateTime? CreatedAt { get; set; } = default(DateTime?);
    }
}

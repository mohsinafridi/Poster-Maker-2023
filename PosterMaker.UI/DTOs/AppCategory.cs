using PosterMaker.UI.Models;

namespace PosterMaker.UI.DTOs
{
    public class AppCategory
    {
        public int  Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public App App { get; set; }
        public string? ThumbnailPath { get; set; }
        
    }
}

namespace PosterMaker.UI.Models
{
    public class App
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public string? ThumbnailPath { get; set; }        
    }
}

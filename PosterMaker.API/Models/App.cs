namespace PosterMaker.API.Models
{
    public class App
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}

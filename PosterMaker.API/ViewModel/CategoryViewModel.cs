using System.ComponentModel.DataAnnotations.Schema;

namespace PosterMaker.API.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public int AppId { get; set; } 
        public IFormFile File { get; set; }
        public byte[] Content { get; set; } //byte array to store the image data.        
    }
}

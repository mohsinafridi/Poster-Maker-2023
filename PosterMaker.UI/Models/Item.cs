namespace PosterMaker.UI.Models
{
    public class Item
    {      
        public string? ItemName { get; set; }        
        public int CategoryId { get; set; }
        public string? ThumbnailPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Content { get; set; } //byte array to store the image data.
    }
}

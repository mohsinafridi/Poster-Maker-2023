using System.ComponentModel.DataAnnotations.Schema;

namespace PosterMaker.API.Models;

public class Item
{
    public int Id { get; set; }
    public string? ItemName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public int CategoryId { get; set; }
    public string? ThumbnailPath { get; set; }
    public string? JsonPath { get; set; }
    public Category Category { get; set; } = null!;

    

}

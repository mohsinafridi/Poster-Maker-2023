using System.ComponentModel.DataAnnotations;

namespace PosterMaker.UI.Models
{
    public class Item
    {
        [Required(ErrorMessage ="Name is Required")]
        public string? ItemName { get; set; }
        [Required(ErrorMessage = "Select Category")]
        public int CategoryId { get; set; }

        public string? ThumbnailPath { get; set; }
       public string? JsonPath { get; set; }    

        public DateTime CreatedAt { get; set; }
    }
}

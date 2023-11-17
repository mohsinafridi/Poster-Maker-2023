using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosterMaker.UI.DTOs;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.App
{
    public class AppCategoryListModel : PageModel
    {
        public List<AppCategory> CategoryList = new();
        public int id { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            id = id;
            // Get Selected App's Categories
            CategoryList = await AppService.GetCategoriesByAppId(id);
            
            return Page();
        }
    }
}

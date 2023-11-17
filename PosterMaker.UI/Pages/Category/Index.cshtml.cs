using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.Category
{
    public class IndexModel : PageModel
    {
        public List<Models.Category> Categories = new();
        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await CategoryService.GetCategories();
            return Page();
        }

        
    }
}

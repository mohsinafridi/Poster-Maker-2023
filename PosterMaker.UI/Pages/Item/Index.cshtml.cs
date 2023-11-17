using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.Item
{
    public class IndexModel : PageModel
    {
        public List<Models.Item> ItemList = new();
        public async Task<IActionResult> OnGetAsync()
        {
            ItemList = await ItemService.GetItems();
            return Page();
        }
    }
}

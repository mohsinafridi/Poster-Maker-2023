using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.App
{
    public class IndexModel : PageModel
    {
        public List<Models.App> AppList = new();
        public async Task<IActionResult> OnGetAsync()
        {
            AppList = await AppService.GetApps();
            return Page();
        }
    }
}

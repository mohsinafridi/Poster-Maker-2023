using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.App
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.App? appVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Call an API
                var isSaved = await AppService.CreateApp(appVM);
                if (isSaved)
                {                    
                    return RedirectToPage("/App/Index");
                }
            }

            return Page();
        }
    }
}

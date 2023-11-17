using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.Category
{
    public class CreateModel : PageModel
    {
        public SelectList AppList { get; set; }
        public async Task OnGetAsync()
        {
            var appList = await AppService.GetApps();
            AppList = new SelectList(appList, "Id", "Name");            
        }

        [BindProperty]
        public Models.Category? categoryVm{ get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Call an API

                var count = await CategoryService.CreateCategory(categoryVm);
                if (count)
                {
                   // Message = "New Product Added Successfully !";
                    return RedirectToPage("/Category/Index");
                }
            }

            return Page();
        }
    }
}

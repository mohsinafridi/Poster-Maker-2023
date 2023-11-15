using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.Category
{
    public class CreateModel : PageModel
    {      
        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
        public Models.Category? categoryVm{ get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Call an API
                var count = await HttpHelper.CreateCategory(categoryVm);
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

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
        public Models.App? AppVM { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (ImageFile == null || ImageFile.Length == 0)
                {
                    return BadRequest("No image file uploaded.");
                }

                //var imagePath = Path.Combine("wwwroot", "images", ImageFile.FileName);
                //using (var stream = new FileStream(imagePath, FileMode.Create))
                //{
                //    await ImageFile.CopyToAsync(stream);
                //}
                AppVM.ThumbnailPath = "";

                var isSaved = await AppService.CreateApp(AppVM);
                if (isSaved)
                {                    
                    return RedirectToPage("/App/Index");
                }
            }
            return Page();
        }
    }
}

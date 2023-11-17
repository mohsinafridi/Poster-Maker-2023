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

                string randomImageFileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) +
                       "_" + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(ImageFile.FileName);

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "images\\app", randomImageFileName);

                string imagePath = Path.GetFileName(fullPath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                
                AppVM.ThumbnailPath = "\\images\\app\\" + imagePath;
                AppVM.CreatedAt = DateTime.Now;

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

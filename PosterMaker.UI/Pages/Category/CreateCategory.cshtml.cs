using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages.Category
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public SelectList AppList { get; set; }
        public async Task OnGetAsync()
        {
            var appList = await AppService.GetApps();
            AppList = new SelectList(appList, "Id", "Name");            
        }

        [BindProperty]
        public Models.Category? CategoryVm { get; set; }

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
                
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "images\\category", randomImageFileName);

                string imagePath = Path.GetFileName(fullPath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                int appId = Convert.ToInt32(Request.Form["SelectedAppId"]);
                CategoryVm.AppId = appId;
                CategoryVm.ThumbnailPath = "\\images\\category\\" + imagePath;
                CategoryVm.CreatedAt = DateTime.Now;

                var isSaved = await CategoryService.CreateCategory(CategoryVm);
                if (isSaved)
                {                   
                    return RedirectToPage("/Category/Index");
                }
            }

            return Page();
        }
    }
}

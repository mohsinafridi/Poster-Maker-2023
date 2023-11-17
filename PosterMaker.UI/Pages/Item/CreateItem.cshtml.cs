using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PosterMaker.UI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace PosterMaker.UI.Pages.Item
{
    public class CreateModel : PageModel
    {         

        [BindProperty]
        public Models.Item? ItemVm { get; set; }

        [BindProperty]
        public IFormFile ItemImage { get; set; }

        [BindProperty]
        public IFormFile JsonFile { get; set; }
        public SelectList CategoryList { get; set; }
        public async Task OnGetAsync()
        {
            var data = await CategoryService.GetCategories();
            CategoryList = new SelectList(data, "Id", "Name");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (ItemImage == null || ItemImage.Length == 0)
                {
                    return BadRequest("No image file uploaded.");                    
                }

                if (JsonFile == null || JsonFile.Length == 0)
                {
                    return BadRequest("No JSON file uploaded.");
                }

                // $"{DateTime.Now.Ticks}"
                string randomImageFileName = Path.GetFileNameWithoutExtension(ItemImage.FileName) +
                        "_" + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(ItemImage.FileName);              

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "images\\items", randomImageFileName);
               
                string imagePath = Path.GetFileName(fullPath);
               
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ItemImage.CopyToAsync(stream);
                }

                string randomJSONFileName = Path.GetFileNameWithoutExtension(ItemImage.FileName) +
                       "_" + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(ItemImage.FileName);

                string jsonFullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "json-files", randomJSONFileName);

                string jsonPath = Path.GetFileName(jsonFullPath);
                
                using (var stream = new FileStream(jsonFullPath, FileMode.Create))
                {
                    await JsonFile.CopyToAsync(stream);
                }

                var categoryId = Convert.ToInt32(Request.Form["SelectedCategoryId"]);
                ItemVm.CategoryId = categoryId;
                ItemVm.ThumbnailPath = "\\images\\items\\" + imagePath;
                ItemVm.JsonPath = "\\json-files\\" + jsonPath;
                ItemVm.CreatedAt = DateTime.Now;
                var count = await ItemService.CreateItem(ItemVm);
                if (count)
                {
                    return RedirectToPage("/Item/Index");
                }
            }

            return Page();
        }
        public List<ListMode> GetDropDownList()
        {
            var List_Mode = new List<ListMode>()
            {
            new ListMode(){Id=1,Name="Class"},
            new ListMode(){Id=2,Name="Subject"},
            new ListMode(){Id=3,Name="Option"},
            new ListMode(){Id=4,Name="Status"}
            };
            return List_Mode;
        }

    }

    public class ListMode
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

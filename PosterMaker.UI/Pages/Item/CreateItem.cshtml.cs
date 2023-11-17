using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PosterMaker.UI.Helpers;
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


                var imagePath = Path.Combine("wwwroot", "item/images", ItemImage.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ItemImage.CopyToAsync(stream);
                }

                var categoryId = Convert.ToInt32(Request.Form["SelectedCategory"]);
                ItemVm.CategoryId = categoryId;
                ItemVm.ThumbnailPath = imagePath;
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PosterMaker.UI.Helpers;
using System.IO;

namespace PosterMaker.UI.Pages.Item
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Models.Item? itemVm { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }
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
                //byte[] bytes = File.ReadAllBytes(Upload);
              //  string file = Convert.ToBase64String(bytes);

                var categoryId = Convert.ToInt32(Request.Form["SelectedCategory"]);
                itemVm.CategoryId = categoryId;
                var count = await ItemService.CreateItem(itemVm);
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

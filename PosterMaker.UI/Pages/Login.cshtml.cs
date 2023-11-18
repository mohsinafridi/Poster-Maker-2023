using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosterMaker.UI.Helpers;

namespace PosterMaker.UI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public required Models.LoginViewModel LoginVm { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (!string.IsNullOrEmpty(LoginVm?.Username) && !string.IsNullOrEmpty(LoginVm?.Password))
                        {
                            var isUserFound = await HttpHelper.LoginUser(LoginVm);
                            if (isUserFound)
                                return this.RedirectToPage("/App/Index");

                            else
                            {
                                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                                return Page();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return this.Page();
        }
    }
}

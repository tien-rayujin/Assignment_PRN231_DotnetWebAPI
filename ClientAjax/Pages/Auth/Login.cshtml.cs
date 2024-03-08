using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BOs;
using Services;

namespace ClientAjax.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _s;

        public LoginModel(IAccountService s)
        {
            _s = s;
        }

        public IActionResult OnGet()
        {
            //ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _s.Create(Account);

            return RedirectToPage("./Index");
        }
    }
}

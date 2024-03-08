using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient_Ajax.Pages.manage.product
{
    public class UpdateModel : PageModel
    {
        public PageResult OnGet(string id)
        {
            return Page();
        }
    }
}

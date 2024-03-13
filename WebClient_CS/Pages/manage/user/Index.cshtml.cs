using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace WebClient_CS.Pages.manage.user;

public class UserModel : PageModel
{
    private readonly ApiService _apiService;

    public UserModel(ApiService apiService)
    {
        _apiService = apiService;
    }
    public List<BOs.Account> Users { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userSession = HttpContext.Session.GetString("user");
        if (userSession == null) return RedirectToPage("/auth/Login");
        var userObject = _apiService.Json2O<Account>(userSession);
        if(userObject != null && userObject.Role.Name.ToLower().Equals("user"))
        {
            ViewData["error"] = "Don't have enough priviledge to access page";
            return RedirectToPage("/auth/Login");
        }
        // get required token to perform request
        var token = HttpContext.Session.GetString("token");

        var requestUri = "http://localhost:5201/api/Account";
        var responseData = await _apiService.SendGetRequest<List<BOs.Account>>(requestUri, token);
        Users = responseData;

        return Page();
    }
}

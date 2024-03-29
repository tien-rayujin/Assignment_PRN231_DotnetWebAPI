using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient_CS.Pages.manage.user;

public class UpdateModel : PageModel
{
    private readonly ApiService _apiService;

    public List<Role> Roles { get; set; }

    [BindProperty]
    public UpdateUserRequestModel RequestModel { get; set; }

    public UpdateModel(ApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<IActionResult> OnGetAsync(string userId)
    {
        var userSession = HttpContext.Session.GetString("user");
        if (userSession == null) return RedirectToPage("/auth/Login");

        var userObject = _apiService.Json2O<Account>(userSession);
        if (userObject == null) return RedirectToPage("/auth/Login");

        if (!userObject.Role.Name.ToLower().Equals("admin"))
        {
            ViewData["error"] = "Don't have enough priviledge to access page";
            return RedirectToPage("/auth/Login");
        }

        // get required token to perform request
        var token = HttpContext.Session.GetString("token");

        // request api to get role data
        var requestUri = "http://localhost:5201/api/Role";
        var responseData = await _apiService.SendGetRequest<List<Role>>(requestUri, token);
        Roles = responseData;

        var requestUri2 = "http://localhost:5201/api/Account/" + userId;
        var responseData2 = await _apiService.SendGetRequest<UpdateUserRequestModel>(requestUri2, token);
        RequestModel = responseData2;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestUri = "http://localhost:5201/api/Account/" + RequestModel.AccountId;
        var requestData = RequestModel;
        await _apiService.SendPutRequest(requestUri, requestData, HttpContext.Session.GetString("token"));

        return RedirectToPage("/manage/user/Index");
    }
}

public class UpdateUserRequestModel
{
    public string AccountId { get; set; }   
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public string? RoleId { get; set; }
    public Boolean IsActive { get; set; }
}


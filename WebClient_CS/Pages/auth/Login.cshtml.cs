using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebClient_CS.Pages.auth;

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginRequestModel AccountLogin { get; set; }

    private readonly ApiService _apiService;

    public LoginModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var requestUri = "http://localhost:5201/api/account/Login";
        var requestData = new LoginRequestModel
        {
            Email = AccountLogin.Email,
            Password = AccountLogin.Password,
        };
        var responseData = await _apiService.SendAuthRequest<LoginRequestModel, LoginResponseModel>(requestUri, requestData);
        var session = HttpContext.Session;

        if (responseData == null) return Page();
        session.SetString("user", _apiService.O2Json(responseData.account));
        session.SetString("token", responseData.token);

        return RedirectToPage("/manage/user/Index");
    }
}

public class LoginResponseModel
{
    public Account account { get; set; }
    public string token { get; set; }
}

public class LoginRequestModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
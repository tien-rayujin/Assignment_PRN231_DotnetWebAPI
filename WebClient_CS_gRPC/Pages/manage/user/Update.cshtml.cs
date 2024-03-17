using AppApi_gRPC;
using BOs;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebClient_CS.Pages.manage.user;

public class UpdateModel : PageModel
{
    public Roles Roles { get; set; }

    private RoleCRUD.RoleCRUDClient clientRole = new RoleCRUD.RoleCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));
    private AccountCRUD.AccountCRUDClient clientAccount = new AccountCRUD.AccountCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));

    [BindProperty]
    public AppApi_gRPC.Account RequestModel { get; set; }

    public UpdateModel()
    {

    }
    public async Task<IActionResult> OnGetAsync(string accountId)
    {
        if (accountId.IsNullOrEmpty()) return NotFound();
        // only admin role can access this page
        //var userSession = HttpContext.Session.GetString("user");
        //if (userSession == null) return RedirectToPage("/auth/Login");

        //var userObject = _apiService.Json2O<Account>(userSession);
        //if (userObject == null) return RedirectToPage("/auth/Login");

        //if (!userObject.Role.Name.ToLower().Equals("admin"))
        //{
        //    ViewData["error"] = "Don't have enough priviledge to access page";
        //    return RedirectToPage("/auth/Login");
        //}

        // get required token to perform request
        //var token = HttpContext.Session.GetString("token");

        // request api to get role data
        Roles = clientRole.GetAll(new EmptyRole());

        RequestModel = clientAccount.Get(new AccountFilter() { AccountId = accountId });

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        AppApi_gRPC.Account requestData = new AppApi_gRPC.Account()
        {
            AccountId = RequestModel.AccountId,
            Name = RequestModel.Name,
            Email = RequestModel.Email,
            Phone = RequestModel.Phone,
            Password = RequestModel.Password,
            RoleId = RequestModel.RoleId,
            IsActive = true,
        };
        clientAccount.Update(requestData);

        return RedirectToPage("/manage/user/Index");
    }
}

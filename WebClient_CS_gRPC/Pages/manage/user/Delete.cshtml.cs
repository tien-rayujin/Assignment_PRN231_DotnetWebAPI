using AppApi_gRPC;
using BOs;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebClient_CS.Pages.manage.user
{
    public class DeleteModel : PageModel
    {
        private AccountCRUD.AccountCRUDClient client = new AccountCRUD.AccountCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));

        public DeleteModel()
        {

        }
        public async Task<IActionResult> OnGetAsync(string accountId)
        {
            if (accountId.IsNullOrEmpty()) return NotFound();
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
            client.Delete(new AccountFilter() { AccountId = accountId });

            return RedirectToPage("/manage/user/Index");
        }
    }
}

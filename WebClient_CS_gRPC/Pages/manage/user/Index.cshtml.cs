using AppApi_gRPC;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient_CS_gRPC;

namespace WebClient_CS.Pages.manage.user;

public class AccountModel : PageModel
{
    public AccountModel()
    {

    }

    public Accounts Accounts { get; set; }
    public PaginatedList<AppApi_gRPC.Account> PagiAccount { get; set; }

    private AccountCRUD.AccountCRUDClient client = new AccountCRUD.AccountCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));

    public async Task<IActionResult> OnGetAsync(int? pageIndex, string searchParams = "")
    {
        //var session = HttpContext.Session;
        //var userSession = session.GetString("user");
        //if (userSession == null) return RedirectToPage("/auth/Login");
        //var userObject = _apiService.Json2O<Account>(userSession);
        //if (userObject != null && userObject.Role.Name.ToLower().Equals("user"))
        //{
        //    ViewData["error"] = "Don't have enough priviledge to access page";
        //    return RedirectToPage("/auth/Login");
        //}
        // get required token to perform request
        //var token = session.GetString("token");


        var products = client.GetAll(new EmptyAccount());
        Accounts = products;
        //Console.WriteLine(products);

        var listAccount = new List<AppApi_gRPC.Account>(products.Items);

        var pageSize = 3;
        PagiAccount = PaginatedList<AppApi_gRPC.Account>.Create(listAccount, pageIndex ?? 1, pageSize);


        return Page();
    }
}

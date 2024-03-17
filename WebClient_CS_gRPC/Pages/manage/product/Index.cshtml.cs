using AppApi_gRPC;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient_CS_gRPC;

namespace WebClient_CS.Pages.manage.Product;

public class ProductModel : PageModel
{
    public ProductModel()
    {
        
    }

    public Products Products { get; set; }
    public PaginatedList<AppApi_gRPC.Product> PagiProduct {  get; set; }

    private ProductCRUD.ProductCRUDClient client = new ProductCRUD.ProductCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));

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


        var products = client.GetAll(new EmptyProduct());
        Products = products;
        //Console.WriteLine(products);

        var listProduct = new List<AppApi_gRPC.Product>(products.Items);

        var pageSize = 3;
        PagiProduct = PaginatedList<AppApi_gRPC.Product>.Create(listProduct, pageIndex ?? 1, pageSize);


        return Page();
    }
}

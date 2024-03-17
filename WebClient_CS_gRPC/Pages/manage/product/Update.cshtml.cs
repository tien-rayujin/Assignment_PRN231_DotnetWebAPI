using AppApi_gRPC;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebClient_CS.Pages.manage.product;

public class UpdateModel : PageModel
{
    private CategoryCRUD.CategoryCRUDClient clientCate = new CategoryCRUD.CategoryCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));
    private ProductCRUD.ProductCRUDClient clientProduct = new ProductCRUD.ProductCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));

    public AppApi_gRPC.Categorys Categories { get; set; }

    [BindProperty]
    public AppApi_gRPC.Product RequestModel { get; set; }

    public UpdateModel()
    {
        
    }
    public async Task<IActionResult> OnGetAsync(string productId)
    {
        if(productId.IsNullOrEmpty()) return NotFound();
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

        // request api to get category data
        Categories = clientCate.GetAll(new EmptyCategory());

        // request api to get category data
        RequestModel = clientProduct.Get(new ProductFilter() { ProductId = productId });

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        AppApi_gRPC.Product requestData = new AppApi_gRPC.Product()
        {
            ProductId = RequestModel.ProductId,
            Name = RequestModel.Name,
            Price = RequestModel.Price,
            Description = RequestModel.Description,
            CategoryId = RequestModel.CategoryId,
            IsActive = RequestModel.IsActive,
        };
        clientProduct.Update(requestData);

        return RedirectToPage("/manage/product/Index");
    }
}
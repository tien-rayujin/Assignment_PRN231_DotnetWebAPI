using AppApi_gRPC;
using BOs;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient_CS.Pages.manage.product;
public class CreateModel : PageModel
{
    public Categorys Categories { get; set; }

    [BindProperty]
    public CreateProductRequestModel RequestModel { get; set; }

    private CategoryCRUD.CategoryCRUDClient clientCate = new CategoryCRUD.CategoryCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));
    private ProductCRUD.ProductCRUDClient clientProduct = new ProductCRUD.ProductCRUDClient(GrpcChannel.ForAddress("http://localhost:5234"));

    public CreateModel()
    {
    }
    public async Task<IActionResult> OnGetAsync()
    {
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
        var products = clientCate.GetAll(new EmptyCategory());
        Categories = products;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        AppApi_gRPC.Product requestData = new AppApi_gRPC.Product()
        {
            Name = RequestModel.Name,
            Price = RequestModel.Price,
            Description = RequestModel.Description,
            CategoryId = RequestModel.CategoryId,
            IsActive = true,
        };
        clientProduct.Create(requestData);

        return RedirectToPage("/manage/product/Index");
    }
}

public class CreateProductRequestModel
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
}
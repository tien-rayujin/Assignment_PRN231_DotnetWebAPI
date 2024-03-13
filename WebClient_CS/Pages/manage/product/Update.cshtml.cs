using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebClient_CS.Pages.manage.product;

public class UpdateModel : PageModel
{
    private readonly ApiService _apiService;

    public List<Category> Categories { get; set; }

    [BindProperty]
    public UpdateProductRequestModel RequestModel { get; set; }

    public UpdateModel(ApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<IActionResult> OnGetAsync(string productId)
    {
        if(productId.IsNullOrEmpty()) return NotFound();
        // only admin role can access this page
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

        // request api to get category data
        var requestUri = "http://localhost:5201/api/Category";
        var responseData = await _apiService.SendGetRequest<List<Category>>(requestUri, token);
        Categories = responseData;

        // request api to get category data
        var requestUri2 = "http://localhost:5201/api/Product/" + productId;
        var responseData2 = await _apiService.SendGetRequest<UpdateProductRequestModel>(requestUri2, token);
        RequestModel = responseData2;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var requestUri = "http://localhost:5201/api/Product/" + RequestModel.ProductId;
        var requestData = new
        {
            ProductId = RequestModel.ProductId,
            Name = RequestModel.Name,
            Price = RequestModel.Price,
            Description = RequestModel.Description,
            CategoryId = RequestModel.CategoryId,
        };
        await _apiService.SendPutRequest(requestUri, requestData, HttpContext.Session.GetString("token"));

        return RedirectToPage("/manage/product/Index");
    }
}

public class UpdateProductRequestModel
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
}
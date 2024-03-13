using BOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient_CS.Pages.manage.user
{
    public class DeleteModel : PageModel
    {
        private readonly ApiService _apiService;

        public DeleteModel(ApiService apiService)
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

            var requestUri = "http://localhost:5201/api/Account/" + userId;
            await _apiService.SendDeleteRequest(requestUri, token);

            return RedirectToPage("/manage/user/Index");
        }
    }
}

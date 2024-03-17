using AppApi_gRPC;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient_CS_gRPC.Pages.manage
{
    public class TestModel : PageModel
    {
        public void OnGet()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5234");
            var client = new ProductCRUD.ProductCRUDClient(channel);

            var products = client.GetAll(new EmptyProduct());
            Console.WriteLine(products);
        }
    }
}

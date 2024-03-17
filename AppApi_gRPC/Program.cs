using BOs;
using DAOs;
using Repos;
using Services;

namespace AppApi_gRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddControllers();
            //builder.Services.AddEndpointsApiExplorer
            builder.Services.AddScoped<AppDbContext>();

            builder.Services.AddScoped<IAccountDAO, AccountDAO>();
            builder.Services.AddScoped<IRoleDAO, RoleDAO>();
            builder.Services.AddScoped<ICategoryDAO, CategoryDAO>();
            builder.Services.AddScoped<IProductDAO, ProductDAO>();

            builder.Services.AddScoped<IAccountRepo, AccountRepo>();
            builder.Services.AddScoped<IRoleRepo, RoleRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.MapGrpcService<GreeterService>();
            app.MapGrpcService<AppApi_gRPC.Services.ProductService>();
            app.MapGrpcService<AppApi_gRPC.Services.CategoryService>();
            app.MapGrpcService<AppApi_gRPC.Services.RoleService>();
            app.MapGrpcService<AppApi_gRPC.Services.AccountService>();

            app.MapGet("/", async (context) =>
            {
                await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            });

            //app.MapControllers();

            app.Run();
        }
    }
}
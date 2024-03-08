using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BOs;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<Category> Categorys { get; init; }
    public DbSet<Product> Products { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AssignmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    public string getConnectionString()
    {
        var config =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        return config.GetConnectionString("default");
    }
}

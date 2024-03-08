using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BOs;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? AccountId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public bool? IsActive { get; set; }
    public string? RoleId { get; set; }
    public Role? Role { get; set; }
}

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? RoleId { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? CategoryId { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? ProductId { get; set; }
    [StringLength(40, MinimumLength = 10)]
    [RegularExpression(@"^[A-Z][A-Za-z\s]*$")]
    public string? Name { get; set; }
    [Range(1, 999)]
    public int? Price { get; set; }
    [StringLength(40)]
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public string? CategoryId { get; set; }
    public Category? Category { get; set; }
}

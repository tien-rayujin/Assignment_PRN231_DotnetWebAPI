using BOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    IAccountService _context;
    IRoleService _roleService;

    public AccountController(IAccountService context, IRoleService roleService)
    {
        _context = context;
        _roleService = roleService;
    }


    [HttpGet]
    [EnableQuery]
    [Authorize(Roles = "Staff, Admin")]
    public ActionResult<List<Account>> GetAll()
    {
        return Ok(_context.GetAll("Role"));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Staff, Admin")]
    public ActionResult<Account> Get([FromRoute] string id)
    {
        return Ok(_context.Get(id));
    }

    [HttpPost]
    [Route("Create")]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([FromBody] Account account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var defaultRoleId = _roleService.GetAll().FirstOrDefault(x => x.Name == "User").RoleId;

            account.RoleId = account.RoleId == "" ? defaultRoleId: account.RoleId;
            account.IsActive = true;

            _context.Create(account);
        }
        catch (Exception)
        {
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public ActionResult Login([FromBody] LoginModel login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate the user credentials
        var account = _context.GetAll("Role").FirstOrDefault(a => a.Email == login.Email && a.Password == login.Password);

        if (account == null)
        {
            return Unauthorized();
        }

        // If successful, you can generate and return a JWT for authentication, or just return a success message
        return Ok(new
        {
            account,
            token = GenerateToken(account)
        });
    }

    private string GenerateToken(Account account)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var tokenSecret = config["Jwt:Key"];

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(tokenSecret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, account.Email!.Trim()),
            new(JwtRegisteredClaimNames.Email, account.Email!.Trim()),
            new("Name", account.Name),
            new(ClaimTypes.Role, account.Role!.Name),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Update([FromRoute] string id, [FromBody] Account account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var entity = _context.Get(id);
            if (entity == null) return NotFound();

            _context.Update(account);
        }
        catch (Exception)
        {
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete([FromRoute] string id)
    {
        try
        {
            var entity = _context.Get(id);
            if (entity == null) return NotFound();

            _context.Delete(entity);
        }
        catch (Exception)
        {
            throw;
        }

        return NoContent();
    }
}
public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
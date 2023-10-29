using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Utapoi.Application.Persistence;
using Utapoi.Infrastructure.Identity.Entities;
using Utapoi.Infrastructure.Persistence.Contexts;

namespace Utapoi.Infrastructure.Persistence.Initializers;

public class AuthDbContextInitializer : IInitializer
{
    private readonly AuthDbContext _context;
    private readonly ILogger<AuthDbContextInitializer> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthDbContextInitializer(ILogger<AuthDbContextInitializer> logger, AuthDbContext context,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        try
        {
            await TrySeedAsync(configuration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync(IConfiguration configuration)
    {
        // Default roles
        var admin = await TryCreateRole("Admin");
        await TryCreateRole("User");

        // Default users
        var administrator = new ApplicationUser
            { UserName = "admin@utapoi.com", Email = "admin@utapoi.com" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator,
                configuration["Secrets:AdministratorPassword"] ?? "Administrator1!");

            await _userManager.AddToRolesAsync(administrator, new List<string> { admin.Name! });
        }
    }

    private async Task<IdentityRole> TryCreateRole(string role)
    {
        var r = new IdentityRole(role);

        if (_roleManager.Roles.All(x => x.Name != r.Name))
        {
            await _roleManager.CreateAsync(r);
        }

        return r;
    }
}
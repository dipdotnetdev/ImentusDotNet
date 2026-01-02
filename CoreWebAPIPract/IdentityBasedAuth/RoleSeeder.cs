using Microsoft.AspNetCore.Identity;

namespace CoreWebAPIPract.IdentityBasedAuth
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}
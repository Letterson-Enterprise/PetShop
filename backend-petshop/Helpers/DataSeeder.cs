using backend_petshop.Data;
using backend_petshop.Entities;

namespace backend_petshop.Helpers
{
    public static class DataSeeder
    {
        public static async Task SeedAdminUser(AppDbContext context)
        {
            if (!context.Usuarios.Any(u => u.Login == "admin"))
            {
                var admin = new Usuario
                {
                    Login = "admin",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin")
                };

                context.Usuarios.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}

using backend_petshop.Data;
using backend_petshop.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend_petshop.Helpers
{
    public static class DataSeeder
    {
        public static async Task SeedAdminUser(AppDbContext context)
        {
            var admin = await context.Usuarios.FirstOrDefaultAsync(u => u.Login == "admin");

            if (admin == null)
            {
                admin = new Usuario { Login = "admin" };
                context.Usuarios.Add(admin);
            }

            admin.SenhaHash = BCrypt.Net.BCrypt.HashPassword("administracao");
            await context.SaveChangesAsync();
        }
    }
}

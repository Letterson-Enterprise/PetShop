using Microsoft.EntityFrameworkCore;
using backend_petshop.Data;
using backend_petshop.Entities;
using backend_petshop.Interfaces;

namespace backend_petshop.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByLogin(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> LoginExists(string login)
        {
            return await _context.Usuarios.AnyAsync(u => u.Login == login);
        }
    }
}

using backend_petshop.Entities;

namespace backend_petshop.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByLogin(string login);
        Task<Usuario> Create(Usuario usuario);
        Task<bool> LoginExists(string login);
    }
}

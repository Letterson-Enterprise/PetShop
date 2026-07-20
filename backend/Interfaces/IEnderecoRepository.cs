using backend_petshop.Entities;

namespace backend_petshop.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco> Create(Endereco endereco);
        Task<Endereco> Update(Endereco endereco);
        Task<Endereco?> GetById(int id);
        Task Delete(Endereco endereco);
    }
}

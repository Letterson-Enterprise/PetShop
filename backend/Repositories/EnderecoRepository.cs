using Microsoft.EntityFrameworkCore;
using backend_petshop.Data;
using backend_petshop.Entities;
using backend_petshop.Interfaces;

namespace backend_petshop.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Endereco> Create(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco> Update(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco?> GetById(int id)
        {
            return await _context.Enderecos.FindAsync(id);
        }

        public async Task Delete(Endereco endereco)
        {
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
        }
    }
}

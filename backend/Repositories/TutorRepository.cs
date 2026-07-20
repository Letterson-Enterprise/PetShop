using Microsoft.EntityFrameworkCore;
using backend_petshop.Data;
using backend_petshop.Entities;
using backend_petshop.Interfaces;

namespace backend_petshop.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly AppDbContext _context;

        public TutorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tutor>> GetAll()
        {
            return await _context.Tutores
                .Include(t => t.Endereco)
                .Include(t => t.Animais)
                .ToListAsync();
        }

        public async Task<Tutor?> GetById(int id)
        {
            return await _context.Tutores
                .Include(t => t.Endereco)
                .Include(t => t.Animais)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tutor> Create(Tutor tutor)
        {
            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();
            return tutor;
        }

        public async Task<Tutor> Update(Tutor tutor)
        {
            _context.Tutores.Update(tutor);
            await _context.SaveChangesAsync();
            return tutor;
        }

        public async Task Delete(Tutor tutor)
        {
            _context.Tutores.Remove(tutor);
            await _context.SaveChangesAsync();
        }
    }
}

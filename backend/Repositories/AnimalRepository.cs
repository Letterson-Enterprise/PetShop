using Microsoft.EntityFrameworkCore;
using backend_petshop.Data;
using backend_petshop.Entities;
using backend_petshop.Interfaces;

namespace backend_petshop.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AppDbContext _context;

        public AnimalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Animal>> GetAll()
        {
            return await _context.Animais.ToListAsync();
        }

        public async Task<Animal?> GetById(int id)
        {
            return await _context.Animais.FindAsync(id);
        }

        public async Task<Animal> Create(Animal animal)
        {
            _context.Animais.Add(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> Update(Animal animal)
        {
            _context.Animais.Update(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task Delete(Animal animal)
        {
            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();
        }
    }
}

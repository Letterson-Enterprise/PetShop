using backend_petshop.Entities;

namespace backend_petshop.Interfaces
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAll();
        Task<Animal?> GetById(int id);
        Task<Animal> Create(Animal animal);
        Task<Animal> Update(Animal animal);
        Task Delete(Animal animal);
    }
}

using backend_petshop.Entities;

namespace backend_petshop.Interfaces
{
    public interface ITutorRepository
    {
        Task<List<Tutor>> GetAll();
        Task<Tutor?> GetById(int id);
        Task<Tutor> Create(Tutor tutor);
        Task<Tutor> Update(Tutor tutor);
        Task Delete(Tutor tutor);
    }
}

using backend_petshop.DTOs;

namespace backend_petshop.Interfaces
{
    public interface ITutorService
    {
        Task<List<TutorDto>> GetAll();
        Task<TutorDto?> GetById(int id);
        Task<TutorDto> Create(CreateTutorDto dto);
        Task<TutorDto> Update(int id, UpdateTutorDto dto);
        Task Delete(int id);
    }
}

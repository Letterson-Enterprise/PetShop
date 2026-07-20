using backend_petshop.DTOs;

namespace backend_petshop.Interfaces
{
    public interface IAnimalService
    {
        Task<List<AnimalDto>> GetAll();
        Task<AnimalDto?> GetById(int id);
        Task<AnimalDto> Create(CreateAnimalDto dto);
        Task<AnimalDto> Update(int id, UpdateAnimalDto dto);
        Task Delete(int id);
    }
}

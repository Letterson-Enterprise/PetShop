using AutoMapper;
using backend_petshop.DTOs;
using backend_petshop.Entities;
using backend_petshop.Interfaces;

namespace backend_petshop.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly IMapper _mapper;

        public AnimalService(IAnimalRepository animalRepository, ITutorRepository tutorRepository, IMapper mapper)
        {
            _animalRepository = animalRepository;
            _tutorRepository = tutorRepository;
            _mapper = mapper;
        }

        public async Task<List<AnimalDto>> GetAll()
        {
            var animais = await _animalRepository.GetAll();
            return _mapper.Map<List<AnimalDto>>(animais);
        }

        public async Task<AnimalDto?> GetById(int id)
        {
            var animal = await _animalRepository.GetById(id);
            if (animal == null) return null;
            return _mapper.Map<AnimalDto>(animal);
        }

        public async Task<AnimalDto> Create(CreateAnimalDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ApplicationException("Nome é obrigatório.");

            if (dto.Peso <= 0)
                throw new ApplicationException("Peso deve ser maior que zero.");

            if (dto.DataNascimento > DateTime.UtcNow)
                throw new ApplicationException("Data de nascimento não pode ser futura.");

            var tutor = await _tutorRepository.GetById(dto.TutorId);
            if (tutor == null)
                throw new ApplicationException("Tutor não encontrado.");

            var animal = _mapper.Map<Animal>(dto);
            animal = await _animalRepository.Create(animal);
            return _mapper.Map<AnimalDto>(animal);
        }

        public async Task<AnimalDto> Update(int id, UpdateAnimalDto dto)
        {
            var animal = await _animalRepository.GetById(id);
            if (animal == null)
                throw new KeyNotFoundException("Animal não encontrado.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ApplicationException("Nome é obrigatório.");

            if (dto.Peso <= 0)
                throw new ApplicationException("Peso deve ser maior que zero.");

            if (dto.DataNascimento > DateTime.UtcNow)
                throw new ApplicationException("Data de nascimento não pode ser futura.");

            var tutor = await _tutorRepository.GetById(dto.TutorId);
            if (tutor == null)
                throw new ApplicationException("Tutor não encontrado.");

            _mapper.Map(dto, animal);
            animal = await _animalRepository.Update(animal);
            return _mapper.Map<AnimalDto>(animal);
        }

        public async Task Delete(int id)
        {
            var animal = await _animalRepository.GetById(id);
            if (animal == null)
                throw new KeyNotFoundException("Animal não encontrado.");

            await _animalRepository.Delete(animal);
        }
    }
}

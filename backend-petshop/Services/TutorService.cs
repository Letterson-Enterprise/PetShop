using AutoMapper;
using backend_petshop.DTOs;
using backend_petshop.Entities;
using backend_petshop.Interfaces;

namespace backend_petshop.Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _tutorRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IViaCepService _viaCepService;
        private readonly IMapper _mapper;

        public TutorService(
            ITutorRepository tutorRepository,
            IEnderecoRepository enderecoRepository,
            IViaCepService viaCepService,
            IMapper mapper)
        {
            _tutorRepository = tutorRepository;
            _enderecoRepository = enderecoRepository;
            _viaCepService = viaCepService;
            _mapper = mapper;
        }

        public async Task<List<TutorDto>> GetAll()
        {
            var tutores = await _tutorRepository.GetAll();
            return _mapper.Map<List<TutorDto>>(tutores);
        }

        public async Task<TutorDto?> GetById(int id)
        {
            var tutor = await _tutorRepository.GetById(id);
            if (tutor == null) return null;
            return _mapper.Map<TutorDto>(tutor);
        }

        public async Task<TutorDto> Create(CreateTutorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ApplicationException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.CEP))
                throw new ApplicationException("CEP é obrigatório.");

            var viaCep = await _viaCepService.ConsultarCep(dto.CEP);

            var endereco = _mapper.Map<Endereco>(viaCep);
            endereco.NumeroCasa = dto.NumeroCasa;
            endereco = await _enderecoRepository.Create(endereco);

            var tutor = _mapper.Map<Tutor>(dto);
            tutor.EnderecoId = endereco.Id;
            tutor = await _tutorRepository.Create(tutor);

            var tutorDto = _mapper.Map<TutorDto>(tutor);
            tutorDto.Endereco = _mapper.Map<EnderecoDto>(endereco);
            return tutorDto;
        }

        public async Task<TutorDto> Update(int id, UpdateTutorDto dto)
        {
            var tutor = await _tutorRepository.GetById(id);
            if (tutor == null)
                throw new KeyNotFoundException("Tutor não encontrado.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ApplicationException("Nome é obrigatório.");

            tutor.Nome = dto.Nome;
            tutor.DataNascimento = dto.DataNascimento;

            if (!string.IsNullOrWhiteSpace(dto.CEP))
            {
                var viaCep = await _viaCepService.ConsultarCep(dto.CEP);

                var endereco = await _enderecoRepository.GetById(tutor.EnderecoId);
                if (endereco != null)
                {
                    _mapper.Map(viaCep, endereco);
                    endereco.NumeroCasa = dto.NumeroCasa;
                    await _enderecoRepository.Update(endereco);
                }
            }

            tutor = await _tutorRepository.Update(tutor);

            var tutorDto = _mapper.Map<TutorDto>(tutor);
            if (tutor.Endereco != null)
                tutorDto.Endereco = _mapper.Map<EnderecoDto>(tutor.Endereco);
            return tutorDto;
        }

        public async Task Delete(int id)
        {
            var tutor = await _tutorRepository.GetById(id);
            if (tutor == null)
                throw new KeyNotFoundException("Tutor não encontrado.");

            if (tutor.Animais != null && tutor.Animais.Count > 0)
                throw new ApplicationException("Não é possível excluir um tutor que possui animais vinculados.");

            await _tutorRepository.Delete(tutor);

            if (tutor.Endereco != null)
                await _enderecoRepository.Delete(tutor.Endereco);
        }
    }
}

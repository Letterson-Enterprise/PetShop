using AutoMapper;
using backend_petshop.DTOs;
using backend_petshop.Entities;

namespace backend_petshop.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animal, AnimalDto>();
            CreateMap<CreateAnimalDto, Animal>();
            CreateMap<UpdateAnimalDto, Animal>();

            CreateMap<Endereco, EnderecoDto>();
            CreateMap<ViaCepResponse, Endereco>()
                .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Localidade))
                .ForMember(dest => dest.UF, opt => opt.MapFrom(src => src.Uf));

            CreateMap<Tutor, TutorDto>();
            CreateMap<CreateTutorDto, Tutor>();
            CreateMap<UpdateTutorDto, Tutor>();
        }
    }
}

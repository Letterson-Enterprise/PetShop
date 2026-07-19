using backend_petshop.DTOs;

namespace backend_petshop.Interfaces
{
    public interface IViaCepService
    {
        Task<ViaCepResponse> ConsultarCep(string cep);
    }
}

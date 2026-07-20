using System.Text.Json;
using backend_petshop.DTOs;
using backend_petshop.Interfaces;

namespace backend_petshop.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepResponse> ConsultarCep(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Erro ao consultar o CEP na ViaCEP.");

            var content = await response.Content.ReadAsStringAsync();
            var viaCepResponse = JsonSerializer.Deserialize<ViaCepResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (viaCepResponse == null || viaCepResponse.Erro)
                throw new ApplicationException("CEP inválido ou não encontrado.");

            return viaCepResponse;
        }
    }
}

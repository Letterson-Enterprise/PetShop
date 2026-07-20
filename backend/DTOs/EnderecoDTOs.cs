namespace backend_petshop.DTOs
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string CEP { get; set; } = string.Empty;
        public int NumeroCasa { get; set; }
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
    }

    public class ViaCepResponse
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public bool Erro { get; set; }
    }

    public class EnderecoInputDto
    {
        public string CEP { get; set; } = string.Empty;
        public int NumeroCasa { get; set; }
    }
}

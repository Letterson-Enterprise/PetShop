namespace backend_petshop.DTOs
{
    public class TutorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public EnderecoDto Endereco { get; set; } = null!;
        public List<AnimalDto> Animais { get; set; } = new();
    }

    public class CreateTutorDto
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string CEP { get; set; } = string.Empty;
        public int NumeroCasa { get; set; }
    }

    public class UpdateTutorDto
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string CEP { get; set; } = string.Empty;
        public int NumeroCasa { get; set; }
    }
}

namespace backend_petshop.DTOs
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Peso { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Especie { get; set; } = string.Empty;
        public int TutorId { get; set; }
    }

    public class CreateAnimalDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Peso { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Especie { get; set; } = string.Empty;
        public int TutorId { get; set; }
    }

    public class UpdateAnimalDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Peso { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Especie { get; set; } = string.Empty;
        public int TutorId { get; set; }
    }
}

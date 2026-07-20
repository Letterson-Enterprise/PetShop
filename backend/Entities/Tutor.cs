using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_petshop.Entities
{
    [Table("Tutores")]
    public class Tutor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [Required]
        public int EnderecoId { get; set; }

        [ForeignKey(nameof(EnderecoId))]
        public Endereco Endereco { get; set; } = null!;

        public ICollection<Animal> Animais { get; set; } = new List<Animal>();
    }
}

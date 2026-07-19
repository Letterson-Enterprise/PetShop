using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_petshop.Entities
{
    [Table("Animais")]
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Peso { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [MaxLength(100)]
        public string Especie { get; set; } = string.Empty;

        [Required]
        public int TutorId { get; set; }

        [ForeignKey(nameof(TutorId))]
        public Tutor Tutor { get; set; } = null!;
    }
}

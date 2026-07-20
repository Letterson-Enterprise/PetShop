using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_petshop.Entities
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(8)]
        public string CEP { get; set; } = string.Empty;

        [Required]
        public int NumeroCasa { get; set; }

        [Required]
        [MaxLength(100)]
        public string Bairro { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Cidade { get; set; } = string.Empty;

        [Required]
        [MaxLength(2)]
        public string UF { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Logradouro { get; set; } = string.Empty;
    }
}

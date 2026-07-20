using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_petshop.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(35)]
        public string Login { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string SenhaHash { get; set; } = string.Empty;
    }
}

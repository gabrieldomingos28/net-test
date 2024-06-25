using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teste.AM53.Domain.Entities
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(15)]
        public string Sku { get; set; }

        [Required]
        public decimal Preco {  get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}

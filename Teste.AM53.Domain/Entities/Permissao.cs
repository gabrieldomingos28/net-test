using System.ComponentModel.DataAnnotations.Schema;

namespace Teste.AM53.Domain.Entities
{
    [Table("Permissao")]
    public class Permissao
    {
        public int Id { get; set; }
        [Column("Permissao")]
        public string Descricao {  get; set; }

    }
}

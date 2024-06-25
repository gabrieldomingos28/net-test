

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teste.AM53.Domain.Entities
{
    [Table("UsuarioPermissao")]
    public class UsuarioPermissao
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PermissaoId { get; set; }
        public virtual Permissao Permissao { get; set; }
    }
}

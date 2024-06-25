using Microsoft.EntityFrameworkCore;
using Teste.AM53.Domain.Entities;

namespace Teste.AM53.Infrastructure.Context
{
    public class Am53DbContext : DbContext
    {
        public Am53DbContext(DbContextOptions<Am53DbContext> options) : base(options)
        { }
        
       public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<UsuarioPermissao> UsuariosPermissoes { get; set; }
    }
}

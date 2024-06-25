
using Microsoft.EntityFrameworkCore;
using Teste.AM53.Domain.Entities;
using Teste.AM53.Domain.Interfaces.Repository;
using Teste.AM53.Infrastructure.Context;

namespace Teste.AM53.Infrastructure
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Am53DbContext _db;
        public UsuarioRepository(Am53DbContext am53DbContext)
        {
            _db = am53DbContext;
        }

        public async Task<IEnumerable<UsuarioPermissao>> GetPermissoesAsync(int idUsuario)
        {
            return await _db
                        .UsuariosPermissoes
                        .Include(_ => _.Permissao)
                        .Where(_ => _.UsuarioId == idUsuario)
                        .ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioAsync(string email, string senha)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(_ => _.Email == email && _.Senha == senha);
        }
    }
}

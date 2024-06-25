

using Teste.AM53.Domain.Entities;

namespace Teste.AM53.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetUsuarioAsync(string email, string senha);
        Task<IEnumerable<UsuarioPermissao>> GetPermissoesAsync(int idUsuario);
    }
}

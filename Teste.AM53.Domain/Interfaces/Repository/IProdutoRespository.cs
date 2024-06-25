
using Teste.AM53.Domain.Entities;

namespace Teste.AM53.Domain.Interfaces.Repository
{
    public interface IProdutoRespository
    {
        Task CreateAsync(Produto produto);
        Task DeleteAsync(int id);
        Task UpdateAsync(Produto produto);
        Task<Produto?> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetAllAsync();
    }
}

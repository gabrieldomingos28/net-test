using Microsoft.EntityFrameworkCore;
using Teste.AM53.Domain.Entities;
using Teste.AM53.Domain.Interfaces.Repository;
using Teste.AM53.Infrastructure.Context;

namespace Teste.AM53.Infrastructure
{
    public class ProdutoRepository : IProdutoRespository
    {
        private readonly Am53DbContext _db;
        public ProdutoRepository(Am53DbContext am53DbContext)
        {
                _db = am53DbContext;
        }
        public async Task CreateAsync(Produto produto)
        {
            _db.Produtos.Add(produto);
             await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var produto = await _db.Produtos.FirstOrDefaultAsync(_ => _.Id == id);
            if (produto != null) { 
                produto.DeletedAt = DateTime.Now;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _db.Produtos
                            .Where(_ => _.DeletedAt == null)
                            .ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
           return await  _db.Produtos.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task UpdateAsync(Produto produto)
        {
            _db.Produtos.Update(produto);
            await _db.SaveChangesAsync();
        }
    }
}

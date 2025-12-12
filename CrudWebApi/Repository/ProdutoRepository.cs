using CrudWebApi.Data;
using CrudWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly BancoContext _context;
        public ProdutoRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProdutoModel>> BuscarTodosProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<ProdutoModel> BuscarProdutoPorId(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<ProdutoModel> AdicionarProduto(ProdutoModel produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<ProdutoModel> AtualizarProduto(ProdutoModel produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> DeletarProduto(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }

            return false;
        }
    }
}

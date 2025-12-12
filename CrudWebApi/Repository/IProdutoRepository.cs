using CrudWebApi.Models;

namespace CrudWebApi.Repository
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoModel>> BuscarTodosProdutos();
        Task<ProdutoModel> BuscarProdutoPorId(int id);
        Task<ProdutoModel> AdicionarProduto(ProdutoModel produto); 
        Task AtualizarProduto(ProdutoModel produto);
        Task<bool> DeletarProduto(int id); 
    }
}
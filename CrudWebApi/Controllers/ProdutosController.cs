using Microsoft.AspNetCore.Mvc;
using CrudWebApi.Data;
using CrudWebApi.Models;
using Microsoft.EntityFrameworkCore;
using CrudWebApi.Repository;

namespace CrudWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> GetProdutos()
        {
            var produtos = await _produtoRepository.BuscarTodosProdutos();
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> PostProduto(ProdutoModel produto)
        {
           var produtos = await _produtoRepository.AdicionarProduto(produto);
            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProdutoModel>> GetProduto(int Id)
        {
        var produtos = await _produtoRepository.BuscarProdutoPorId(Id);
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
          var produtos = await _produtoRepository.DeletarProduto(id);
            if (!produtos)
            {
                return NotFound();
            }
            return NoContent(); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoModel produto)
        {
        var produtos = await _produtoRepository.BuscarProdutoPorId(id);
            if (produtos == null)
            {
                return NotFound();
            }
            produtos.Nome = produto.Nome;
            produtos.Preco = produto.Preco;
            produtos.Estoque = produto.Estoque;
            await _produtoRepository.AtualizarProduto(produtos);
            return NoContent(); 

        }
    }
}

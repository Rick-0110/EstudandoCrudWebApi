using Microsoft.AspNetCore.Mvc;
using CrudWebApi.Data;
using CrudWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly BancoContext _context;
        public ProdutosController(BancoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> PostProduto(ProdutoModel produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProdutoModel>> GetProduto(int Id)
        {
            var produto = await _context.Produtos.FindAsync(Id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;

        }
    }
}

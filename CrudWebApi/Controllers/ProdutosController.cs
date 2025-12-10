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

        //metodo auxiliar para verificar se o produto existe
        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoModel produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

     
    }
}

using Microsoft.EntityFrameworkCore;
using CrudWebApi.Models;


namespace CrudWebApi.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
    }
}

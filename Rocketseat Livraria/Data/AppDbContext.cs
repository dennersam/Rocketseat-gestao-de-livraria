using Microsoft.EntityFrameworkCore;
using Rocketseat_Livraria.Models;

namespace Rocketseat_Livraria.Data
{
    public class AppDbContext : DbContext
    {
        public string DbPath {  get; }
        public DbSet<Livro> Livros { get; set; }

        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.ApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "Livros.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
}

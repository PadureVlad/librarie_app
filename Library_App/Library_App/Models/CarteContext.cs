using Microsoft.EntityFrameworkCore;

namespace Library_App.Models
{
    public class CarteContext : DbContext
    {
        public DbSet<Carte> Carti { get; set; }
        public CarteContext(DbContextOptions<CarteContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public IQueryable<Carte> GetCartiPret()
        {
            return Carti.Where(carte => carte.Pret >= 150 && carte.Pret <= 500);
        }
        public IQueryable<Carte> GetAutorConsoana()
        {
            return Carti.Where(carte => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(carte.Autor.Substring(0, 1)));
        }
    }
}

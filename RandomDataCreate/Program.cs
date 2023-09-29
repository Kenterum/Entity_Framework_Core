using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;

ECommerceDbContext context = new();












Console.WriteLine("Hello");





public class ECommerceDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }

    public DbSet<Parca> Parcalar { get; set; }

    public DbSet<UrunParca> UrunParca { get; set; }

    public DbSet<UrunDetay> UrunDetay { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=EticaretDB; Trusted_Connection=True;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {

            }

        }
        return base.SaveChangesAsync(cancellationToken);

    }




}


public class Urun
{
    public int Id { get; set; }

    public string UrunAdi { get; set; }

    public float Fiyat { get; set; }

    public ICollection<Parca> Parcalar { get; set; }
}

public class Parca
{
    public int Id { get; set; }

    public string ParcaAdi { get; set; }


}

public class UrunParca
{
    public int UrunId { get; set; }

    public int ParcaId { get; set; }

    public Urun Urun { get; set; }

    public Parca Parca { get; set; }
}

public class UrunDetay
{
    public int Id { get; set; }

    public float Fiyat { get; set; }

}

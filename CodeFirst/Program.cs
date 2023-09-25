using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class ECommerceDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=EticaretDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>()
            .HasKey(up => new { up.UrunId, up.ParcaId });

        // Configure the foreign keys
        modelBuilder.Entity<UrunParca>()
            .HasOne(up => up.Urun)
            .WithMany(u => u.UrunParca)
            .HasForeignKey(up => up.UrunId);

        modelBuilder.Entity<UrunParca>()
            .HasOne(up => up.Parca)
            .WithMany(p => p.UrunParca)
            .HasForeignKey(up => up.ParcaId);
    }
}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public ICollection<UrunParca> UrunParca { get; set; } // Navigation property
}

public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }

    public ICollection<UrunParca> UrunParca { get; set; } // Navigation property
}

public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }

    public Urun Urun { get; set; }
    public Parca Parca { get; set; }
}

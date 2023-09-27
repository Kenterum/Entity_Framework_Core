using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

public class ECommerceDbContext : DbContext
{
    // DbContext configuration

    // DbSet properties...

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=EticaretDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }
}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public static Urun GenerateRandomUrun()
    {
        Random random = new Random();
        string randomUrunAdi = "Urun" + random.Next(1, 1000);
        float randomFiyat = (float)random.NextDouble() * 1000; // Random price between 0 and 1000
        return new Urun { UrunAdi = randomUrunAdi, Fiyat = randomFiyat };
    }
}

public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
    public string Parcalar { get; set; }

    public static Parca GenerateRandomParca()
    {
        Random random = new Random();
        string randomParcaAdi = "Parca" + random.Next(1, 1000);
        string randomParcalar = "Random Parcalar for " + randomParcaAdi;
        return new Parca { ParcaAdi = randomParcaAdi, Parcalar = randomParcalar };
    }
}

public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }
    public Urun Urun { get; set; }
    public Parca Parca { get; set; }

    public static UrunParca GenerateRandomUrunParca()
    {
        Random random = new Random();
        int randomUrunId = random.Next(1, 1000);
        int randomParcaId = random.Next(1, 1000);
        return new UrunParca { UrunId = randomUrunId, ParcaId = randomParcaId };
    }
}

public class UrunDetay
{
    public int Id { get; set; }
    public float Fiyat { get; set; }

    public static UrunDetay GenerateRandomUrunDetay()
    {
        Random random = new Random();
        float randomFiyat = (float)random.NextDouble() * 1000; // Random price between 0 and 1000
        return new UrunDetay { Fiyat = randomFiyat };
    }
}

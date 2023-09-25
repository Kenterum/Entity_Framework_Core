using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ETicaretContext : DbContext
{

    public DbSet<Urun> Urunler { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=EticaretDB;Trusted_Connection=True;TrustServerCertificate=True");

    }
}


public class Urun
{
    public int UrunId { get; set; }



}

//public class 
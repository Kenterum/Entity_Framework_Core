using Microsoft.EntityFrameworkCore;
Console.Read();
#region Default Convention 

 public class Kitap
{
    public int Id { get; set; } 

    public string KitapAdi { get; set; }

   public  ICollection<Yazar> Yazarlar { get; set; }   

}

public class Yazar
{ 
    public int Id { get; set; }
    public string YazarAdi { get; set; }    

    public ICollection<Kitap> Kitaplar { get; set; }    
    
}
#endregion







public class EKitapDbContext : DbContext
{
 
    public DbSet<Kitap> Kitaplar { get; set; } 

    public DbSet<Yazar> Yazarlar { get; set; }  
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=EKitapDb; Trusted_Connection=True;TrustServerCertificate=True");
    }

}


using Microsoft.EntityFrameworkCore;

ETicaretContext context = new();

#region Veri Nasil Silinir? 
//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5);

//context.Urunler.Remove(urun);


//await context.SaveChangesAsync();   



#endregion

#region Silme isleminde changetracker rolu 
//vardir  

#endregion
#region Takip edilemeyen nesneler nasil silinir? 
//ETicaretContext context = new();
//Urun u = new()
//{

//    Id = 2
//};
//context.Urunler.Remove(u);
//await context.SaveChangesAsync();



#endregion
#region EntityState ile silme
//ETicaretContext context = new();
//Urun u = new() { Id = 1 };
//context.Entry(u).State = EntityState.Deleted;
//await context.SaveChangesAsync();




#endregion
#region RemoveRange (AddRange tersi) 

//List<Urun> urunler = await context.Urunler.Where(u => u.Id >= 7 && u.Id <= 9).ToListAsync();
//context.Urunler.RemoveRange(urunler);
//await context.SaveChangesAsync();

#endregion


//Peki butun urunleri nasil silebilirim ?




public class ETicaretContext : DbContext
{

    public DbSet<Urun> Urunler { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EticaretDB;Integrated Security=True;TrustServerCertificate=True");

    }
}


public class Urun
{
    public int Id { get; set; }

    public string UrunAdi { get; set; }

    public float Fiyat { get; set; }



}

using Microsoft.EntityFrameworkCore;

#region Veri Nasil Guncellenir?


//ETicaretContext context = new();

//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//urun.UrunAdi = "H Urunu";
//urun.Fiyat = 999;


//await context.SaveChangesAsync();

#endregion

#region Change Tracker 

//Context uzerinden gelen verilerin takibinden sorumludur.
//Bu takip mekanizmasi sayesinde context uzerinden gelen verilerle ilgili islemler neticesinde update yahut  delete sorgularinin olusturulacagi anlasilir.





#endregion

#region Takip edilemeyen nesneleri guncelleme

//ETicaretContext context = new();
//Urun urun = new()
//{
//    Id = 3,
//    UrunAdi = "Yeni Urun",
//    Fiyat = 123

//};

//#region Update fonksiyonu 
////Kullanilabilmesi icin kesinlikle Id degeri verilmelidir.

//context.Urunler.Update(urun);
//await context.SaveChangesAsync();
#endregion

#region Entitystate nedir?
//Bir entity instancesinin durumunu ifade eden bir referanstir. 

//ETicaretContext context = new();
//Urun u = new();
//Console.WriteLine(context.Entry(u).State);

#endregion

#region Ef Core acisindan bir verinin guncellenmesi gerektigi nasil anlasiliyor ?
//ETicaretContext context = new();
//Urun u = new();

/*  
The error message suggests that you are trying to cast a `System.Single` (a floating-point number) to an `System.Int32` (an integer).
The problem can be found in the `Urun` class, where the `Fiyat` property is declared as an integer, 
but apparently, the database field associated with it is in fact a floating-point number.
To fix the issue, you should change the type of the `Fiyat` property to `float` or `double` in the `Urun` class, 
and also update the database schema to match the updated model. */
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State);

//urun.UrunAdi = "Hilmi";

//Console.WriteLine(context.Entry(urun).State);

//await context.SaveChangesAsync();

//Console.WriteLine(context.Entry(urun).State);




#endregion

#region Birden fazla veri guncellenirken nelere dikkat edilmeli
ETicaretContext context = new();


var urunler = await context.Urunler.ToListAsync();
foreach (var urun in urunler)
{
    urun.UrunAdi += "*";

}
await context.SaveChangesAsync();
#endregion










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

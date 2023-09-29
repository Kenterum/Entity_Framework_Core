using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;

ECommerceDbContext context = new();

#region DB ADDING VALUES 
//Kitap urun1 = new()
//{
//    KitapAdi = "Annen",
//    SayfaSayisi = 2000
//};
//Kitap urun2 = new()
//{
//    KitapAdi = "May",
//    SayfaSayisi = 2000
//};
//Kitap urun3 = new()
//{
//    KitapAdi = "Kanterheit",
//    SayfaSayisi = 2000
//};
//await context.Kitaplar.AddRangeAsync(urun1, urun2, urun3);


//Kullanici kul1 = new()
//{
//    Adi = "Salmanova Pendire",
//    Email = "2000@gmail.com"
//};
//Kullanici kul2 = new()
//{
//    Adi = "Qurbanov Qoyun",
//    Email = "2000@gmail.com"
//};
//Kullanici kul3 = new()
//{
//    Adi = "Iskenderov Isfendiyar",
//    Email = "2000@gmail.com"
//};
//await context.Kullanicilar.AddRangeAsync(kul1, kul2, kul3);

#endregion







await context.SaveChangesAsync();



#region AsNoTracking Metodu
//Context uzerinden gelen tum datalar Change Tracker makanizmasi tarafindan takip edilmektedir

//Change Tracker, takip ettigi nesnelerin sayisiyla dogru orantili olacak sekilde bir maliyete sahiptir. O yuzden islem yapilmayacak verilerin tekip edilmesi bizlere 
//luzumsuz yere bir maliyet ortaya cikaracaktir.

//AsNoTracking metodu, context uzerinden sorgu neticesinde gelecek olan verilerin Change Tracker tarafindan takip edilmesini engeller. 

//AsNoTracking metodu ile Change Tracker'in ihtiyac olmayan verilerdeki maliyetini torpulemis oluruz. 


//AsNoTracking fonksiyonu ile yapilan sorgulamalarda, verileri elde edebilir, bu verileri istenilen noktalarda kullanabilir lakin veriler uzerinde herhangi bir 
//degisiklik/update islemi yapamayiz. 

var kullanicilar = await context.Kullanicilar.ToListAsync();

foreach(var kullanici in kullanicilar)
{
    Console.Write(kullanici.Adi);
    kullanici.Adi = $"yeni-{kullanici.Adi}";
}

#endregion


await context.SaveChangesAsync();



#region AsNoTrackingWithIdentifyResolution

#endregion





Console.WriteLine();
public class ECommerceDbContext : DbContext
{
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Rol> Roller { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=KutuphaneDB; Trusted_Connection=True;TrustServerCertificate=True");
    }
}


public class Kullanici
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Rol> Roller { get; set; }
}

public class Rol
{
    public int Id { get; set; }
    public string RolAdi { get; set; }
    public ICollection<Kullanici> Kullanicilar { get; set; }
}
public class Kitap
{
    public Kitap() => Console.WriteLine("Kitap nesnesi oluşturuldu.");
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public int SayfaSayisi { get; set; }

    public ICollection<Yazar> Yazarlar { get; set; }
}
public class Yazar
{
    public Yazar() => Console.WriteLine("Yazar nesnesi oluşturuldu.");
    public int Id { get; set; }
    public string YazarAdi { get; set; }

    public ICollection<Kitap> Kitaplar { get; set; }
}
using Microsoft.EntityFrameworkCore;
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
//await context.SaveChangesAsync();


//Yazar yaz1 = new()
//{
//    YazarAdi = "Cem Yilmaz"
//};
//Yazar yaz2 = new()
//{
//    YazarAdi = "Can Yilmaz"
//};
//Yazar yaz3 = new()
//{
//    YazarAdi = "Ilber Ortayli"
//};
//await context.Yazarlar.AddRangeAsync(yaz1, yaz2, yaz3);
//await context.SaveChangesAsync();


#endregion










#region AsNoTracking Metodu
//Context uzerinden gelen tum datalar Change Tracker makanizmasi tarafindan takip edilmektedir

//Change Tracker, takip ettigi nesnelerin sayisiyla dogru orantili olacak sekilde bir maliyete sahiptir. O yuzden uzerinde islem yapilmayacak verilerin tekip edilmesi
//bizlere luzumsuz yere bir maliyet ortaya cikaracaktir.

//AsNoTracking metodu, context uzerinden sorgu neticesinde gelecek olan verilerin Change Tracker tarafindan takip edilmesini engeller. 

//AsNoTracking metodu ile Change Tracker'in ihtiyac olmayan verilerdeki maliyetini torpulemis oluruz. 


//AsNoTracking fonksiyonu ile yapilan sorgulamalarda, verileri elde edebilir, bu verileri istenilen noktalarda kullanabilir lakin veriler uzerinde herhangi bir 
//degisiklik/update islemi yapamayiz. 

//var kullanicilar = await context.Kullanicilar.AsNoTracking().ToListAsync();

//foreach(var kullanici in kullanicilar)
//{
//    Console.Write(kullanici.Adi);
//    kullanici.Adi = $"yeni-{kullanici.Adi}";
//    //context.Kullanicilar.Update(kullanici); -> Tracking olmadan database sistemini update etmek 
//}
// await context.SaveChangesAsync(); -> Takipli bir sekilde database sistemini update etmek

#endregion


#region AsNoTrackingWithIdentifyResolution
//CT mekanizmasi yinelenen verileri tekil instance olarak getirir. Burdan ekstradan bir performans kazanci soz konusudur

//Bizler yaptigimiz sorgularda takip mekanizmasinin AsNoTracking metodu ile maliyeti kirmak isterken bazen maliyete sebebiyet verebiliriz.(Ozellikle iliskisel tablolari
//Sorgularken bu durumlara dikkat etmemiz gerekiyor)

//AsNoTracking ile elde edilen veriler takip edilmeyeceginden dolayi yenilenen verilerin ayri instancelarda olmasina sebebiyet veriyoruz. Cunku CT mekanizmasi takip ettigi
//nesneden bellekte varsa eger ayni nesneden birdaha olusturma geregi duymaksizin o nesneye ayri noktalardaki ihtiyaci ayni instance uzerinden gidermektedir.

//Boyle bir durumda hem takip mekanizmasinin maliyetini ortadan  kaldirmak hemde yenilenen datalari tek bir instance uzerinde karsilamak icin AsNoTrackingWithIdentityResolution
//fonksiyonunu kullanabiliriz  


//var kitaplar =await context.Kitaplar.Include(k => k.Yazarlar).AsNoTrackingWithIdentityResolution().ToListAsync();


//AsNoTrackingWithIdentityResolution fonksiyionu AsNoTracking fonksiyonuna nazaran yavastir/maliyetlidir lakin ct'a nazaran daha performansli ve daha az maaliyetlidir




#endregion


#region AsTracking

//var kitaplar = await context.Kitaplar.AsTracking().ToListAsync();

//Context uzerinden gelen datalarin ct tarafindan takip edilmesini iradeli bir sekilde ifade etmemizi saglayan fonksiyondur.


//UseQueryTrackingBehavior metodunun davranisi geregi uygulama seviyesinde Ct'nin default olarak devrede olup olmamasini ayarliyor olacagiz. 
//eger ki default olarak pasif hale getirilirse boyle durumda takip mekanizmasinin ihtiyac oldugu sorgularda AsTracking fonskiyonunu kullanabilir ve boylece
//takip mekanizmasini iradeli bir sekilde devreye sokmus oluruz. 


#endregion



#region UseQueryTrackingBehavior
//Ef core seviyesinde/uygulama seviyesinde ilgili contextten gelen verilerin uzerinde CT mekanizmasinin davranisi temel seviyede belirlememizi saglayan fonksiyondur. 
//Yani konfigurasyon fonksiyonudur.

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

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;


//ECommerceDbContext context = new();


#region Add Async Fonksiyonu 

//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "B Urunu",
//    Fiyat = 2000
//};

//Console.WriteLine(context.Entry(urun).State)
//await context.AddAsync(urun);

//await context.SaveChangesAsync();


//await context.AddAsync(urun);


// Context dbset Add Async Fonksiyonu 

//Console.WriteLine(context.Entry(urun).State);

//await context.Urunler.AddAsync(urun);

//Console.WriteLine(context.Entry(urun).State);

//await context.SaveChangesAsync();

//Console.WriteLine(context.Entry(urun).State);



//SaveChanges; Inset, update ve delete sorgularini olusturup bir transaction esliginde veritabanina gonderip execute eden fonksiyondur. 
//Eger ki olusturulan sorgulardan her hangi birisi basarisiz olursa tum islemleri geri alir (rollback)

#endregion
#region Birden Fazla Veri Ekleme 
//ETicaretContext context = new();
//Urun urun1 = new()
//{
//    UrunAdi = "A Urunu",
//    Fiyat = 2000
//};

//Urun urun2 = new()
//{
//    UrunAdi = "B Urunu",
//    Fiyat = 2000
//};


//Urun urun3 = new()
//{
//    UrunAdi = "C Urunu",
//    Fiyat = 2000
//};

//await context.AddAsync(urun1);

//await context.AddAsync(urun2);

//await context.AddAsync(urun3);
//await context.SaveChangesAsync();
////savechangesi verimli kullan. Bir defa kullan 
////await context.Urunler.AddRangeAsync(urun1, urun2, urun3);
////context.Urunler.AddRangeAsync();









#endregion
#region AddRange 

//ETicaretContext context = new();
//Urun urun1 = new()
//{

//    UrunAdi = "C Urunu",
//     Fiyat = 2000
//};

//Urun urun2 = new()
//{

//   UrunAdi = "D Urunu",
//    Fiyat = 2000
//};

//Urun urun3 = new()
//{

//    UrunAdi = "E Urunu",
//    Fiyat = 2000
//};



//await context.Urunler.AddRangeAsync(urun1, urun2, urun3);
//await context.SaveChangesAsync();


#endregion
#region Eklenen verinin Generate Edilen ID'Sini Elde Etme  
//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "O Urunu",
//    Fiyat = 2000
//};

//await context.AddAsync(urun);
//await context.SaveChangesAsync();
//Console.WriteLine(urun);

#endregion
#region Veri Nasıl Eklenir?
//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "A Ürünü",
//    Fiyat = 1000
//};

#region context.AddAsync Fonksiyonu
//await context.AddAsync(urun);
#endregion
#region context.DbSet.AddAsync Fonksiyonu
//await context.Urunler.AddAsync(urun);
#endregion

//await context.SaveChangesAsync(); 

#endregion
#region SaveChanges Nedir?
//SaveChanges; insert, update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip execute eden fonksiyodur. Eğer ki oluşturulan sorgulardan herhangi biri başarısız olursa tüm işlemleri geri alır(rollback)
#endregion
#region EF Core Açısından Bir Verinin Eklenmesi Gerektiği Nasıl Anlaşılıyor?
//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "B ürünü",
//    Fiyat = 2000
//};

//Console.WriteLine(context.Entry(urun).State );

//await context.AddAsync(urun);

//Console.WriteLine(context.Entry(urun).State);

//await context.SaveChangesAsync();

//Console.WriteLine(context.Entry(urun).State);
#endregion

#region SaveChanges'ı Verimli Kullanalım!
//SaveChanges fonksiyonu her tetiklendiğinde bir transaction oluşituracağından dolayı EF Core ile yapılan her bir işleme özel kullanmaktan kaçınmalıyız! Çünkü her işleme özel transaction veritaanı açısından ekstradan maliyet demektir. O yüzden mümkün mertebe tüm işlemlerimizi tek bir transaction eşliğinde veritabanına gönderebilmek için savechanges'ı aşağıdaki gibi tek seferde kullanmak hem maliyet hem de yönetilebilirlik açısından katkıda bulunmuş olacaktır.

//ETicaretContext context = new();
//Urun urun1 = new()
//{
//    UrunAdi = "C ürünü",
//    Fiyat = 2000
//};
//Urun urun2 = new()
//{
//    UrunAdi = "D ürünü",
//    Fiyat = 2000
//};
//Urun urun3 = new()
//{
//    UrunAdi = "E ürünü",
//    Fiyat = 2000
//};

//await context.AddAsync(urun1);

//await context.AddAsync(urun2);

//await context.AddAsync(urun3);
//await context.SaveChangesAsync();
#endregion


#region AddRange
ECommerceDbContext context = new();
Urun urun1 = new()
{
    UrunAdi = "C ürünü",
    Fiyat = 2000
};
Urun urun2 = new()
{
    UrunAdi = "D ürünü",
    Fiyat = 2000
};
Urun urun3 = new()
{
    UrunAdi = "E ürünü",
    Fiyat = 2000
};
await context.Urunler.AddRangeAsync(urun1, urun2, urun3);
await context.SaveChangesAsync();
#endregion

#region Eklenen Verinin Generate Edilen Id'sini Elde Etme
//ETicaretContext context = new();
//Urun urun = new()
//{
//UrunAdi = "O ürünü",
//Fiyat = 2000
//};
//await context.AddAsync(urun);
//await context.SaveChangesAsync();
//Console.WriteLine(urun.Id);
#endregion



public class ECommerceDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }

    public DbSet<Parca> Parcalar { get; set; }

    public DbSet<UrunParca> UrunParca { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=EticaretDB; Trusted_Connection=True;TrustServerCertificate=True");
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


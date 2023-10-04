using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new();


#region Default Convention 
//Her iki entity'de Navigation property ile birbirlerini tekil olarak referans edereik fiziksel
//bir iliskinin olacagi ifade edilir. 

//One to One iliski turunde, dependent entity'nin hangisi oldugunu default olarak belirleyebilmek pek kolay degildir. Bu durumda fiziksel olarak bir foreign key'e karsilik
//property/kolon tanimlayarak cozum getirebiliyoruz
//Boylece foreign key'e karsilik property tanimlayarak luzumsuz bir kolon olusturmus oluyoruz. 
//public class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }
//}
//public class CalisanAdresi
//{
//    public int Id { get; set; }

//    public int CalisanId { get; set; }  
//    public string Adres { get; set; }

//    public Calisan Calisan { get; set; }

//}

#endregion

#region Data Annotations  

//Navigation Property'ler tanimlanmalidir 
//Foreign kolonunun ismi default conventionun dusunda bir kolon olacaksa eger ForignKey attribute ile bunu bildirebiliriz
//Foreign Key kolonu olusturulmak zorunda degildir

//1 e 1 iliskide ekstradan foreign key kolonuna ihtiyac olmayacagindan dolayi dependent entity'deki id kolonunun hem forign key hem de primary key olarak kullanmayi 
//tercih ediyoruz ve bu duruma ozen gosterimelidir diyoruz. 



//public class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }



//}
//public class CalisanAdresi
//{
//    [Key, ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }

//    public string Adres { get; set; }

//    public Calisan Calisan { get; set; }

//}

#endregion

#region Fluent API 
//Navigation propertyler tanimlanmali
//Fluent API yonteminde entity'ler arasindaki iliski context sinigi icerisinde OnModelCreating fonksiyonun override edilerek metotlar araciligiyla tasarlanmasi gerekmektedir.
//Yani tum sorumluluk bu fonksiyon icerisindeki calismalardadir  
public class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }

    public CalisanAdresi CalisanAdresi { get; set; }



}
public class CalisanAdresi
{
    [Key, ForeignKey(nameof(Calisan))]
    public int Id { get; set; }

    public string Adres { get; set; }

    public Calisan Calisan { get; set; }

}

#endregion






public class ESirketDbContext : DbContext
{
    DbSet<Calisan> Calisanlar { get; set; }
    DbSet<CalisanAdresi> CalisanAdresleri { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=ESirketDb; Trusted_Connection=True;TrustServerCertificate=True");
    }
    //Model'larin (entity) Veritabaninda generate edilecek yapilan konfigurasyonlari bu fonskiyon icerisinde konfigure edilir. 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c => c.Id);
    }
}



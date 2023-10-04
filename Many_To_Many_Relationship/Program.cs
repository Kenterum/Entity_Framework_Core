using Microsoft.EntityFrameworkCore;
Console.Read();
#region Default Convention 
//Iki entity arasindaki iliskiyi navigation propertyler uzerinden cogul olarak kurmaliyiz (Icollection, List)
//Default Convention'da cross table'i manuel olusturmak zorunda degiliz. Ef Core Tasarima uygun bir sekilde cross table'i kendisi otomatik basacak ve generate edecektir.
//Ve olusturulan cross table'in icerisinde composite primary key'i de otomatik olusturmus olacaktir. 


// public class Kitap
//{
//    public int Id { get; set; } 

//    public string KitapAdi { get; set; }

//   public  ICollection<Yazar> Yazarlar { get; set; }   

//}

//public class Yazar
//{ 
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }    

//    public ICollection<Kitap> Kitaplar { get; set; }    

//}
#endregion

#region Data Annotations 
//Cross table manuel olarak olusturulmak zorundadir 
//Entity'lerde olusturdugumuz cross table entity si ile bire cok bir iliski kurmulmali 
//Cross table da composite primary key'i data annotation(attributes)lar ile manuel kurmaiyoruz. Bununa gore Fluent API kullanmamiz gerekmekte. 
//Cross table'a karsilik bir entity modeli olusturuyorsak eger, bunu context sinifi icerisinde DbSet propertysi olarak bildirmek mecburiyetinde degiliz 

//public class Kitap
//{
//    public int Id { get; set; }

//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }

//}
////Cross Table  
//public class KitapYazar
//{

//    public int KitapId { get; set; }
//    public int YazarId { get; set; }
//    public Kitap Kitap { get; set; }    
//    public Yazar Yazar { get; set; }    

//}

//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }

//    public ICollection<KitapYazar> Kitaplar { get; set; }


//}

#endregion

#region Fluent API
//Cross table manuel olusturulmali, DbSet olarak eklenmesine luzum yok.
//Composite PK HasKey metodu ile kurulmali. 
public class Kitap
{
    public int Id { get; set; }

    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }
}
//Cross Table  
public class KitapYazar
{

    public int KitapId { get; set; }
    public int YazarId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }

}

public class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }

    public ICollection<KitapYazar> Kitaplar { get; set; }


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


    #region Data Annotations  
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //   modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky => new {ky.KitapId, ky.YazarId});


    //}
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId });
        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Kitap)
            .WithMany(k => k.Yazarlar)
            .HasForeignKey(ky => ky.KitapId);
        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Yazar)
            .WithMany(y => y.Kitaplar)
            .HasForeignKey(ky => ky.YazarId);
    }
}


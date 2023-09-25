using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


ECommerceDbContext context = new();
await context.Database.MigrateAsync();




#region Temel Query Sorgulama  

#region Method Syntax 
//var urunler  = await context.Urunler.ToListAsync();


#endregion

#region Query Syntax 
//var urunler2 = await (from urun in context.Urunler select urun).ToListAsync();
#endregion


#region Iquaryable ve IEnumerable 

//Iquaryable ef core uzerindne yapilmis olan sorgunun execute edilmemis halini ifade eder. 
//Ienumarable sorgunun calistirilip/execute edilip verilen in memory yuklenmis halini ifade eder . 

//var urunler = (from urun in context.Urunler
// select urun).ToListAsync();

#endregion

#endregion

#region Olusturulan Sorguyu Execute etmek 

//int urunId = 5;
//string urunAdi = "2";


//var urunler = from urun in context.Urunler
//              where urun.Id > urunId
//              select urun;

////urunId = 200;
////string urunAdi = "4"; --> direkt program burayi kayde alacak cunku son aldigi deger tetiklenir.

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);

//}

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

#endregion


#region  Cogul veri getiren sorgulama fonksiyonlari 


#region Method Syntax 
//var urunler = context.Urunler.ToListAsync();
#endregion

#region Query Syntax 
//var urunler = (from urun in context.Urunler 
//              select urun).ToListAsync();

// YA DA YA DA 

//var urunler = from urun in context.Urunler
//              select urun;

//var datas = await urunler.ToListAsync();




#endregion 

#region Where 

//----------------------------- Method Syntax Icin 
//var urunler = await context.Urunler.Where(u => u.Id > 500).ToListAsync();
//var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("a")).ToListAsync();

//Console.WriteLine();

//-----------------------------Query Syntax Icin
//var urunler = from urun in context.Urunler
//              where urun.Id > 500 && urun.UrunAdi.EndsWith("7")
//              select urun;

//var data = await  urunler.ToListAsync();
//Console.WriteLine();


#endregion


#region OrderBy 
////Sorgu uzerinde siralama yapmamizi saglayan bir fonksiyondur (Ascending Order)

////Method syntax 
//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi);

////Query Syntax 
//var urunler2 = from urun in context.Urunler
//               where urun.Id > 500 || urun.UrunAdi.StartsWith("2")
//               orderby urun.UrunAdi /*ascending de yazilabilir order siralamasi icin */
//               select urun;

//await urunler.ToListAsync();
//await urunler2.ToListAsync();
#endregion 


#region ThenBy 
//OrderBy uzerinde yapilan siralama islemini farkli kolonlarda uygulamamizi saglayan bir fonksiyondur. (Ascending) 

//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi).ThenBy(u => u.Fiyat).ThenBy(u => u.Id);

//await urunler.ToListAsync();



#endregion

#region OrderByDescending
//descending olarak siralama yapmamizi saglayan bir fonksiyondur.

// Method and Query Syntax  

//Method Syntax 

//var urunler = await context.Urunler.OrderByDescending(u => u.Fiyat).ToListAsync();

//Query Syntax  
//var urunler = await (from urun in context.Urunler
//                     orderby urun.UrunAdi descending 
//                     select urun).ToListAsync();


#endregion

#region ThenByDescending 

//var urunler  = context.Urunler.OrderByDescending(u => u.Id).ThenByDescending(u => u.Fiyat).ThenBy(u => u.UrunAdi).ToListAsync();





#endregion 

#endregion


#region Tekil veri getiren sorgulama fonskiyonlari 

// Yapilan sorguda sade ve sadece tek bir verinin gelmesi amaclaniyorsa. Single ve ya SingleOrDefault fonksiyonlari kullanilabilir.

#region SingleAsync 

//Eger ki sorgu neticesinde birden fazla veri geliyorsa ya da hic gelmiyorsa her iki durumda da exception firlatir  

#region Tek Kayit Geldiginde 
//var urun = await context.Urunler.SingleAsync(u => u.Id == 18);
//Console.WriteLine();
#endregion
#region Hic Kayit gelmediginde
//var urun = await context.Urunler.SingleAsync(u => u.Id == 5555);

#endregion
#region Cok kayit geldiginde
//var urun = await context.Urunler.SingleAsync(u => u.Id > 1);
#endregion



#endregion

#region SingleOrDefaultAsync
//Eger ki sorgu neticesinde birden fazla veri geliyorsa exception firlatir. ANCAK hic veri gelmiyorsa null doner  
#region Tek Kayit Geldiginde 
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 18);

#endregion
#region Hic Kayit gelmediginde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 111118);

#endregion
#region Cok kayit geldiginde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id > 1);

#endregion

#endregion

//Yapilan sorguda tek bir verinin gelmesi amaclaniyorsa Fist ya da FirstOrDefault fonksiyonlari kullanilabilir.
#region FirstAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir.Eger ki hic ver gelmiyorsa hata firlatir  
#region Tek Kayit Geldiginde 
//var urun = await context.Urunler.FirstAsync(u => u.Id == 18);
#endregion

#region Hic kayit gelmediginde
//var urun = await context.Urunler.FirstAsync(u => u.Id == 123118);

#endregion

#region Cok kayit geldiginde 
//var urun = await context.Urunler.FirstAsync(u => u.Id > 1); 

#endregion


#endregion

#region FirstOrDefaultAsync  
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eger ki hic veri gelmiyorsa null degerini dondurur.
#region Tek Kayit Geldiginde 
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 18);
#endregion

#region Hic kayit gelmediginde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 123118);

#endregion

#region Cok kayit geldiginde 
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id > 1);



#endregion



#endregion

//Temel farklari kisaca 
//Firstde eger birden fazla veri donuluyor ise istenilen degerden bir sonraki degeri dondurur. Ancak  Single de ise exception firlatir 

//Eslesen satir bulunmadiginda her defaultta null doner. (SingleOrDefaultAsync) (FirstOrDefaultAsync)  


#region FindAsync

context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);



#endregion








/*
#region Tek Kayit Geldiginde 
#endregion

#region Hic kayit gelmediginde
#endregion

#region Cok kayit geldiginde 
#endregion
*/







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



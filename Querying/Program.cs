using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


ECommerceDbContext context = new();
//await context.Database.MigrateAsync();



//13
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

//13
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

//14
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

//15
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

//context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);



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


//16
#region Diger Sorgulama Fonksiyonlari 

#region CountAsync 
//Olsuturulan sorgunun execute edilmesi neticesinde kac adet satirin elde edilecegini integer olarak bizlere bildiren fonksiyonlardir 

//var urunler = (await context.Urunler.ToListAsync()).Count();

//var urunler = await context.Urunler.CountAsync();



#endregion


#region LongCountAsync
// Olusturulan sorgunun execute edilmesi neticesinde kac adet satirin elde edilecegini long olarak bizlere bildirir. Esasen big data da kullanilir  

//var urunler = await context.Urunler.LongCountAsync();


//var urunler = await context.Urunler.LongCountAsync(u => u.Fiyat > 5000);
//var urunler = context.Urunler.LongCount();

#endregion


#region AnyAsync 
//Sorgu neticesinde verinin gelip gelmedigini bool olarak veren fonksiyondur
// SQL de EXIST Fonksiyonu 

//var urunler = await context.Urunler.AnyAsync();

//var urunlercheck = await context.Urunler.AnyAsync(u => u.Id > 1);




#endregion

#region MaxAsync 

//var fiyat = await context.Urunler.MaxAsync(u => u.Fiyat);  --> En yuksek fiyat ne ise onu verir   (SELECT MAX)

#endregion

#region MinAsync 
//var fiyat = await context.Urunler.MinAsync(u => u.Fiyat);   //Buda en dusuk fiyat ne ise onu veriyor (SELECT MIN)

#endregion


#region Distinct
//Sorguda mukerrer kayit varsa bunu tekillestiren fonksiyondur.

//var urunler = await context.Urunler.Distinct().ToListAsync(); 

#endregion


#region AllAsync 
//Bir sorgu neticesinde gelen verilerin, verilan sarta uyup uymadigini kontrol etmektedir Eger ki tum veriler sarta uyuyorsa true uymuyorsa false doner 

//var m = await context.Urunler.AllAsync(u => u.Fiyat > 50000);
#endregion


#region SumAsync
//Toplam fonksiyonudur. Vermis oldgumuz sayisal propertynin toplamini alir  

//var fiyatToplam = await context.Urunler.SumAsync(u => u.Fiyat);

#endregion


#region AverageAsync 

//vermis oldugumuz sayisal propertynin aritmetik ortalamasini alir  

//var ortalama = await context.Urunler.AverageAsync(u => u.Fiyat); 



#endregion


#region ContainsAsync  
//Like '%....%' sorgusu olusturmamizi saglar  

//var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("7")).ToListAsync();

#endregion

#region StartsWith 
//Like '...%' sorgusunu olusturmamizi saglar 

//var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("7")).ToListAsync();    

#endregion

#region EndsWith 
//Like '%...' sorgusunu olusturmamizi saglar 
//var urunler = await context.Urunler.Where(u => u.UrunAdi.EndsWith("7")).ToListAsync();    



#endregion
#endregion


//17 
#region Sorgu Sonucu Donusum Fonksiyonlari 
// Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri istegimiz dogrultusunda farkli turlerler projeksiyon edebiliyoruz.


#region ToDictionaryAsync
//Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/tutmak/karsilamak istiyorsak eger kullanilir 
//var urunler = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat);

//ToList ile ayni amaca hizmet etmektedir. Yani olusturulan sorguyu execute edip neticesini alirlar.
//ToList: gelen sorgu neticesini entity turunde bir koleksiyona (List<TEntity>) donusturmekteyken 
//ToDictionary ise: Gelen Sorgu neticesini Dictionary turunden bir koleksiyona donusturecektir.
#endregion

#region ToArrayAsync 
//Olusturulan sorguyu dizi olarak elde eder. 
//ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin gelen sonucu entity dizisi olarak elde eder. 

//var urunler = await context.Urunler.ToArrayAsync();


#endregion


#region Select  
//Select fonksiyonunun islevsel olarak birden fazla davranisi vardir  
//Select fonksiyonu, generate edilecek sorgunun cekilecek kolonlarini ayarlamamizi saglamaktadir
//var urunler = await context.Urunler.Select(u => new Urun
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync(); 



// Select fonksiyonu glen verileri farkli turlerde karsilamamizi saglar. T, anonim

//var urunler = await context.Urunler.Select(u => new 
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync();


//var urunler = await context.Urunler.Select(u => new UrunDetay
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync();



//var urunler = await context.Urunler.Include(u => ).Select(u => new UrunDetay
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync();

#endregion

#region SelectMany 
//Select ile ayni amaca hizmet eder. Lakin iliskiler tablolar neticesinde gelen koleksiyonel verielri de tekillestirip projeksiyon etmemii saglar


//var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u => u.Parcalar,
//    (u,p) => new
//    {
//        u.Id,
//        u.Fiyat,
//        p.ParcaAdi
//    } ).ToListAsync();

#endregion











#endregion


//18 
#region GroupBy Fonksiyonu 

#region Method Syntax

//var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
//{
//    Count = group.Count(),
//    Fiyat = group.Key

//}).ToListAsync();

#endregion

#region Query Syntax 
//var datas = await (from urun in context.Urunler
//                   group urun by urun.Fiyat
//            into @group
//                   select new
//                   {
//                       Fiyat = @group.Key,
//                       Count = @group.Count()
//                   }).ToListAsync();

#endregion
#endregion



#region Foreach Fonksiyonu
//Sorgulama fonksiyonu degildir
//Sorgulama neticesinde elde edilen koleksiyonel veriler uzerinde iterasyonel olarak donemmizi ve teker teker vileri elde edip islemelr yapabilmemmizi saglayan bir fonksiyondur
//forach dongusunun metot halidir!
//foreach (var item in datas)
//{

//}

//datas.Foreach(x =>
//{

//});
#endregion

Console.WriteLine();

public class ECommerceDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }

    public DbSet<Parca> Parcalar { get; set; }

    public DbSet<UrunParca> UrunParca { get; set; }

    public DbSet<UrunDetay> UrunDetay { get; set; }


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

    public ICollection<Parca> Parcalar { get; set; }
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

public class UrunDetay
{
    public int Id { get; set; }

    public float Fiyat { get; set; }

}

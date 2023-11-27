using Microsoft.EntityFrameworkCore;

ECommerceDbContext context = new();


#region Change Tracker Nedir? 

// Context nesnesi uzerinden gleen tum nesneler/veriler otomatik olarak bir takip mekanizmasi tarafindan izlenirler. Iste bu takip mekanizmasinin ismi ChangeTrackerdir.
//changetracker ile  nesneler uzerindeki degisiklikler/islemler takp edilerek netice itibariyle bu fitratina uygun sql sorgularini generate edilir. 
//iste bu isleme de change tracking denir. 





#endregion


#region ChangeTracker Propertysi
//Takip edilen nesnelere erisebilmemizi saglayan ve gerektigi taktirde islemler gercekletirmemizi saglayan bir propertydir.

//Context sinifinin base class'i olan DbContext Sinifinin bir memberi'dir 

//var urunler = await context.Urunler.ToListAsync();

//urunler[6].Fiyat = 123; //Update
//context.Urunler.Remove(urunler[7]); //Delete
//urunler[8].UrunAdi = "asdasd"; //Update

//var datas = context.ChangeTracker.Entries();
//await context.SaveChangesAsync(); 


#region DetectChanges Metodu
//Ef core, context nesnesi tarafindan izlenen tum nesnelerdeki degisiklikleri change tracker sayesinde takip edebilmekte ve nesnelerde olan verisel 
//degisiklikleri Change tracker sayesinde takip edebilmekte ve nesnelere olan verisel degisiklikler yakalanarak bunlarin anlik goruntuleriini olusturabilir  
//Yapilan degisikliklerin veritabanina gonderilmeden once algilandigindan emin olmak gerekir. Savechanges fonksiyonu cagirildigi anda nesneler ef core tarafindan 
//otomatik olarka kontrol edilirler
//Ancak yapilan operasyonlarda guncel tracking verilerinden emin olabilmek icin degisikliklerin algilanmasini opsiyonel oalrak gerceklestirmek isteyebiliriz. 
//ista bunun icin DetectChanges fonksiyonu kullanilabilir ve her ne kadar Ef Core degisiklikleri otomatik algiliyor olsa da siz yine de iradenizler kontrole zorlayabilirsiniz\

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//urun.Fiyat = 123;

//context.ChangeTracker.DetectChanges();
//await context.SaveChangesAsync();

#endregion

#region AutoDetectChangesEnabled Property'si
//Ilgili metotlar(SaveChanges, Entries) tarafindan DetectChanges metodunun otomatik olarak tetiklenmesinin konfigurayon yapmamizi salayan propertydir. 
//Savechanges finksoiyonu tetiklendiginde DetectChanges metodunun icerisinde defult olarak cagirmaktadir. bu durum DetectChanges fonksiyonununun kullanimini irademizle
//yonetmek ve maliyet/performans optimizasyonu yapmak istedigimiz durumlarda AutoDetectchangesEnabled  ozelligini kapatabiliriz. 


#endregion

#region Entries Metodu 
//Context'te ki Entry metodunun koleksiyonel versiyonudur.
//Change Tracker mekanizmasi tarafindan izlenen her entitiy nesnesinin bilgisini EntityEntry turunden elde etmemizi saglar ve belirli islemler yapabilmemize olanak tanir
//Entries metodu, DetectChanges metodunu tetikler. Bu durum da tipki savechanges da oldugu gibi bir maaliyettir. 
//Buradaki maaliyetten kacinmak icin AutoDetectChangesEnables ozelligine false degeri verilebilir

//var urunler = await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(u => u.Id == 7).Fiyat = 123; //Update
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 8)); //Delete
//urunler.FirstOrDefault(u => u.Id == 9).UrunAdi = "Hmm"; //Update

//context.ChangeTracker.Entries().ToList().ForEach(e =>
//{
//    if (e.State == EntityState.Unchanged)
//    {
//        //..
//    }
//    else if (e.State == EntityState.Deleted)
//    {
//        //..
//    }


//});
#endregion 


#region AcceptAllChanges Metodu
//SaveChanges() veya SaveChanges(true) tetiklendiginde Ef Core her seyin yolunda oldugunu varsayarak tercih ettigi verilerin takibini keser ve yeni degisikliklerin takip 
//edilmesini bekler. Boyle bir durumda beklenmeyen bir durum olasi birhata soz konusu olursa eger Ef core takip ettigi nesneleri birakacagi icin bir duzeltme mevzu bahis olmayacaktir

//Haliye bu durumda devreye SaveChanges(false) ve AcceptAllChanges metotlari girecektir

//SaveChanges(False), Ef Core'a gerekli veritabani komutlarini yurutmesini soyler ancak gerektiginde yeniden oynatilabilmesi icin degisikleri beklemeye/nesneleri takip
//etmeye devam eder. Taa ki AcceptAllChanges metodnun irademizle cagirana kadar

//SaveChanges(false) ile islemin basarili oldugunden emin olursaniz AcceptAllchanges metodu ile nesnelerden takibi kesebilirsiniz.

//var urunler = await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(u => u.Id == 7).Fiyat = 123; //Update
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 8)); //Delete
//urunler.FirstOrDefault(u => u.Id == 9).UrunAdi = "Hmm"; //Update


//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();

#endregion



#region HasChanges Metodu
//Takip edilen nesneler arasindan degisiklik yapilanlarin olup olmadiginin bilgisini verir.
//Arkaplanda DetectChanges metodunu tetikler  

//var result = context.ChangeTracker.HasChanges();
#endregion 




#endregion


#region Entity States
//Entity nesnelerinin durumlerini ifade eder.  

#region Detached 
//Nesnenin change tracker mekanizmasi tarafindan takip edilmedigini ifade eder. 
//Urun urun = new();
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "sdadsd";
//await context.SaveChangesAsync();   

#endregion

#region Added
//Veritabanina eklenecek nesneyi ifade eder. Added henuz veritabanina islenmeyen veriyi ifade eder. SaveChanges fonksiyonu cagirildiginda insert sorgusu 
//olusturulacagi anlamina gelir

//Urun urun = new() { Fiyat = 123, UrunAdi = "Urun 1001" };
//Console.WriteLine(context.Entry(urun).State);
//await context.Urunler.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State);
//await context.Urunler.AddAsync(urun);
//urun.Fiyat = 321;
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();


#endregion

#region Unchanged
//Veritabanindan sorgulandigindan beri nesne uzerinde herhangi bir degisiklik yapilmadigini ifade eder. Sorgu neticesinde elde edilen tum nesneler baslangicta 
//bu state degerindedir. 

//var urunler = await context.Urunler.ToListAsync();

//var data = context.ChangeTracker.Entries();

#endregion

#region Modified
//Nesne uzerinde degisiklik yapildigini ifade eder. Savechanges fonksiyonu cagirildiginda update sorgusu olusturulacagi anlamina gelir. 

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "assdskjssd";
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync(false);
//Console.WriteLine(context.Entry(urun).State);


#endregion 

#region Deleted
//Nesnenin silindigini ifade eder. SaveChanged fonksiyonu cagirildiginda delete sorgusu olusturulacagi anlamina gelir.  

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//context.Urunler.Remove(urun);
//Console.WriteLine(context.Entry(urun).State);
//context.SaveChangesAsync();

#endregion

#region Context Nesnesi Uzerinden Change Tracker 

var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
urun.Fiyat = 123;
urun.UrunAdi = "Silgi"; //Modified | Update


//context.SaveChangesAsync();
#endregion

#region Entry Metodu
#region OriginalValues Property'si
//var fiyat = context.Entry(urun).OriginalValues.GetValue<float>(nameof(Urun.Fiyat));   
//var urunadi = context.Entry(urun).OriginalValues.GetValue<string>(nameof(Urun.UrunAdi));   
#endregion

#region CurrentValues Property'si
//var urunAdi = context.Entry(urun).CurrentValues.GetValue<string>(nameof(Urun.UrunAdi));

#endregion


#region GetDatabaseValues Metodu 
//var _urun = await context.Entry(urun).GetDatabaseValuesAsync();
#endregion



#endregion 


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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {

            }

        }
        return base.SaveChangesAsync(cancellationToken);

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

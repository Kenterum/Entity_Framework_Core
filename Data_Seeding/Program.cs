using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

ApplicationsDbContext context = new();


#region Data Seeding Nedir?
//EF Core ile insa edilen veritabani icerisinde veritabani nesneleri olabilecegi gibi verilerinde migrate surecinde uretilmesini isteyebiliriz
//Iste bu ihtiyaca istinaden Seed Data Ozelligi ile EF Core Uzerinden migrationlarda veriler olusturabilir ve migrate ederken bu verileri hedef tablolarimiza basabiliriz.
//Seed Data'lar migrate sureclerinde hazir verileri tablolara basabilmek icin bunlari yazilim kisminda tutmamizi gerektirmektedirler. Boylece bu veriler uzerinde veritabani seviyesinde isteniler manipulasyonlar gonul rahatligiyla gerceklestirilebilmektedir. 

//Data Seeding ozelligi su noktalarda kullanilabilir;
//Rest icin gecici verilere ihtiyac varsa
//Asp.NET Core'daki Identity yapilanmasindaki roller gibi static degerler de tutulabilir
//Yazilim icin temel konfigurasyonel degerler. 
#endregion
#region Seed Data Ekleme
//OnModelCreating metodu iceririnse Entity fonksiyonundan sonra cagirilan HasData fonskiyonu ilgili entitye karsilik Seed Data'lari eklememizi saglayan bir fonksiyondur.

//PK degerlerinin manuel olarak bildirilmesi/verilmesi gerekmektedir. Neden diye sorarsaniz eger, iliskisel verileri de Seed Datalara uretebilmek icin.


#endregion
#region Iliskisel Tablolar icin Seed Data Ekleme  
//Iliskisel senaryolarda dependent table'a veri eklerken foreign key kolonunun propertysi varsa eger ona iliskisel degerini vererek ekleme islemini yapiyoruz 
#endregion


class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}
class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public ICollection<Post> Posts { get; set; }
}


class ApplicationsDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
              .HasData(
                    new Blog() { Id = 1, Url = "www.google.com/blog"},
                    new Blog() { Id = 2, Url = "www.com.com/deneme" }
               );
        modelBuilder.Entity<Post>()
            .HasData(
                new Post() { Id = 1, BlogId = 11, Title = "A", Content = "...."},
                new Post() { Id = 2, BlogId = 1, Title = "B", Content = "...." }
            );
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=SeedDB; Trusted_Connection=True;TrustServerCertificate=True");
    }

}
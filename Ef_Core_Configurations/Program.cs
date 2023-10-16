using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Reflection.Metadata;

ApplicationsDbContext context = new();

#region Ef Core'da neden yapilandirmalara ihtiyacimiz olur? 
//Default davranislari yeri geldiginde gecersiz kilmak ve ozellestirmek isteyebiliriz. Bundan dolayi yapilandirmalara ihtiyacimiz olmaktadir.
#endregion

#region OnModelCreating Metodu
//Bu metot DbContext sinifi icerisinde virtual olarak ayarlanmis bir metottur. Bizler bu metodu kullanarak modallarimizla ilgili temel konfigrasyonel davranislari(FluentApi) sergileyebiliriz.
#endregion

#region GetEntityTypes
//Ef Core'da kullanilar entityleri elde etmek, programatik olarak ogrenmek istiyorsak eger, GetEntityTypes fonksiyonunu kullanabiliriz. 
#endregion 

#region Configurations | Data Annotations & Fluent API 


#region Table - ToTable 
//Generate edilecek tablonun ismini belirlememizi saglayan yapilanmadir. Ef Core normal sartlarda generate edecegi tablonun adini DbSet property'sinden almaktadir. Eger ki bunu ozellestirmek istiyor isek Table attributeunu ya da ToTable api'ini kullanabiliriz. 
#endregion

#region Coulumn - HasColumnName, HasColumnType, HasColumnOrder
//Ef Core'da tablolarin kolonlari entity siniflari icerisindeki property'lere karsilik gelmektedir. Default olarak propertylerin adi kolon adi iken, turleri ya da propertynin tipleri kolon turleridir.
//Eger ki generate edilecek kolon isimlerine ver turlerine mudahale etmek istiyorsak bu konfigurasyon kullanilir
#endregion

#region ForeignKey - HasForeignKey
//Iliskisel tablo tasarimlarinda, bagimli tabloda esas tabloya karsilik gelecek verilerin tutuldugu kolonu foreign key olarka temsil etmekteyiz
//Ef Core'da foreign key kolonu genellikle Entity Tanimlama kurallari geregi default yapilanmalarla olusturulur  
//ForeignKey Data Annotations Attributeunu direkt kullanabiliriz. Lakin Fluent api ile bu konfigurasyonu yapacaksaik iki entity arasindaki iliskiyi de modellememiz gerekmektedir. Aksi takdirde fluent api uzerinde HasForeignKey fonksiyonunu kullanamayiz 
#endregion

#region NotMapped - Ignore
//EF Core, entity siniflari icerisindeki tum propertyleri default olarak modellenen tabloya kolon seklinde migrate eder. 
//Bazen bizler entitiy siniflari icerisinde tabloda bir kolona karsilik gelmeyen propertyler tanimlamak mecburiyetinde kalabiliriz. 
//Bu propertylerin ef core tarafindan kolon olarak map edilmesini istemedigimizi bildirebilmek icin NotMapped ya da Ignore kullanabiliriz. 
//modelBuilder.Entity<Person>()
//            .Ignore(p => p.YazilimOgren);
#endregion

#region Key - HasKey
//Default convention olarak bir entity'nin icerisindeki Id, ID, EntityId, EntityID vs. seklinde tanimlanan tum propertyler varsayilan olarak primary key constraint uygulanir. 
//Key ya da HasKey yapilanmalariyla istedigimiz her hangi bir propertyde default convention disinda pk uygulayabiliriz. 
//Ef Core'da bir entity icerisinde kesinlikle Pk'i temesil edecek olan property bulunmalidir. Aksi takdirde Ef Core migration olustururken hata verecektir. Eger ki tablonun PK'i yoksa bunun bildirilmesi gerekir.

#endregion

#region Timestamp - IsRowVersion 
//Bir verinin versiyonunu olusturmamizi saglayan yapilanma bu konfigurasyonlardir.
#endregion

#region Required - IsRequired
//Bir kolonun nullable ya da not null olup olmamasini bu konfigurasyonla belirleyebiliriz. 
#endregion






#endregion

#region Saving 
//Person p = new();
//p.Department = new()
//{
//    Name = "Yazilim Departmani"
//};
//p.Name = "Nijat";
//p.Surname = "Orujzada";

//await context.People.AddAsync(p);
//await context.SaveChangesAsync();


#endregion
//var person = await context.People.FindAsync(1);

//Console.WriteLine();
//[Table("Kisiler")]
class Person
{
    //[Key]
    public int Id { get; set; }
    public int DId { get; set; }
    //[Column("Adi", TypeName = "metin", Order = 7)]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public decimal Salary { get; set; }
    //Yazilimsal amacla olusturdugum property  
    //NotMapped]
    //public string YazilimOgren {get; set; }

    [Timestamp]
    public byte[] RomVersion { get; set; }


    public DateTime CreatedDate { get; set; }
    public Department Department { get; set; }
  

}
class Department
{
    public int Id { get; set; } 
    public string Name { get; set; }    
   public ICollection<Person> People { get; set; }  

}
class ApplicationsDbContext : DbContext
{
    public DbSet<Person> People {  get; set; }
    public DbSet<Department> Departmens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region GetEntityTypes
        //var entities =  modelBuilder.Model.GetEntityTypes();
        //foreach (var entity in entities)
        //{
        //    Console.WriteLine(entity.Name); 
        //}
        #endregion
        #region ToTable
        //modelBuilder.Entity<Person>().ToTable("Mucahit"); //En son karar buradan verilir. 
        #endregion
        #region Column 
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasColumnName("Adi")
        //    .HasColumnType("int")
        //    .HasColumnOrder(7);
        #endregion
        #region ForeignKey
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(p => p.People)
        //    .HasForeignKey(p => p.DId);
        #endregion
        #region Ignore
        //modelBuilder.Entity<Person>()
        //    .Ignore(p => p.YazilimOgren);
        #endregion
        #region Primary Key
        //modelBuilder.Entity<Person>()
        //    .HasKey(p => p.Id);  
        #endregion
        #region IsRowVerison
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.RomVersion)
        //    .IsRowVersion();
        #endregion
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=ConfigurationsDb; Trusted_Connection=True;TrustServerCertificate=True");
    }
}

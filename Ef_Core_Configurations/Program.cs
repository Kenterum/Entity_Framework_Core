using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
//Ef Core da bir property default olarak not null seklinde tanimlanir. Eger ki property'i nullable yapmak istiyorsak turu uzerinde ? operatioru ile bildirimde bulunmamiz gerekmektedir.
#endregion

#region MaxLength | StringLength - HasMaxLength 
//Bir kolonun max karakter sayisini belirlememizi saglar  
#endregion

#region HasPrecision
//Sayisal degerlerde kesinligi ortaya koymamizi saglayan bir ozellik.(Kusuratli degerlerimiz vardir mesela.Noktanin hanesi gibi kesinlik bildiren bir calisma yapabiliriz) 
#endregion

#region Unicode - IsUnicode
//Kolon icerisinde unicode karakterler kullanilacaksa bu yapilanma kullanilir.    
#endregion

#region Comment - HasComment 
//Ef Core uzerinden olusturulmus olan veritabani nesneleri uzerinden bir aciklama/ yorum yapmak istiyorsaniz Comment yapilandirmasini kullanabiliriz. 
#endregion

#region ConcurrencyCheck - IsConcurrecnyToken 
//Verinin butunsel olarak tutarliligini saglayacak bir concurrency token yapilanmasidir. 

#endregion

#region InverseProperty
//Iki entity arasinda birden fazla iliski varsa eger bu iliskilerin hangi navigation property uzerinen olacagini ayarlamizi saglayan bir konfigurasyondur.
#endregion

#endregion

#region Configurations | Fluent API

#region Composite Key 
//Tablolarda birden fazla kolonu kumulatif olarak primary key yapmak istiyorsak buna composite key denir. 
#endregion

#region HasDefaultSchema 
//Ef Core uzerinden insa edilen herhangi bir veritabani nesnesi default olarak dbo semasina sahiptir. Bunu ozellestirebilmek icin kullanilan bir yapilanmadir. 
#endregion

#region Property
#region HasDefaultValue
//Tablodaki herhangi bir kolonun deger gonderilmedigi durumlarda default olarak hangi degeri alacagini  belirler
#endregion

#region HasDefaultValueSql
//Tablodaki herhangi bir kolonun deger gondeerilmedigi durumlarda default olarak hangi sql cumleciginden degeri alacagini belirler
#endregion

#endregion

#region HasComputedColumnSql
//Tablolarda birden fazla kolondaki verileri isleyerek degerini olusturan colunlara Computed Column denmektedir. Ef Core uzerinden bu tarz computed column olusturabilmek icin kullanilan yapilanmadir 
#endregion

#region HasConstraintName
//EFC Uzerinden olusturulan contraint'lere default isim yerine ozellestirilmis bir isim verebilmek icin kullanilan yapilandirmadir
#endregion

#region HasData
//Migrate surecinde veritabanini insa ederken bir yandan da yazilim uzerinden hazir veriler olusturmak istiyorsak bunun yontemini ve usulunu inceliyor olacagiz
//HasData konfigurasyonu bu opersayonun yapilandirma ayagidir
//HasData ile migrate surecinde olusturulacak olan verilerin pk olan id kolonlarina iradeli bir sekilde degerlerin girilmesi zorunludur.  
#endregion

#region HasDiscriminator
//TPT ve TPH isminde konulari ile ilgili yapilandirmalarimiz  HasDiscriminator ve HasValue fonksiyonlaridir. 
#region HasValue

#endregion

#endregion

#region HasField
//Backing Field ozelligini kullanmamizi saglayan bir yapilandirmadir 
#endregion

#region HasNoKey
//Efc de tum entitylerin bir pk kolonu olmak zorundadir. Eger ki entity'de pk kolonu olmayacaksa bunun bildirilmesi gerekmetedir Iste bunun icin kullanilan fonksiyondur.
#endregion

#region HasIndex
//Index yapilanmasina  dair konfiguras
#endregion

#region HasQueryFilter
#endregion

#region DatabaseGenerated - ValueGeneratedOnAddOrUpdate, ValueGeneratedOnAdd, ValueGeneratedNever 
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

//Person person = new()
//{
//    Name = "Ahmet",
//    Surname = "Suleymanov",
//    DId = 1,
//    CreatedDate = DateTime.Now,
//};
//await context.People.AddAsync(person);
//await context.SaveChangesAsync();

//A a = new()
//{
//    X = "A dan",
//    Y = 1
//};
//B b = new()
//{
//    X = "B den",
//    Z = 2
//};
//Entity entity = new()
//{
//    X = "Entityden"
//};
//await context.As.AddAsync(a);
//await context.Bs.AddAsync(b);
//await context.Entities.AddAsync(entity);

//await context.SaveChangesAsync();   
#endregion
//var person = await context.People.FindAsync(1);

//Console.WriteLine();
//[Table("Kisiler")]

class Person
{
    //[Key]
    public int Id { get; set; }
    //public int Id2 { get; set; }
    //public int DId { get; set; }
    //[Column("Adi", TypeName = "metin", Order = 7)]
    public int DepartmentId { get; set; }
    public string _name;
    public string Name { get => _name; set => _name = value; }
    //[Required]
    //[MaxLength(13)]
    //[StringLength(13)]
    //[Unicode]
    public string Surname { get; set; }
    //[Precision(5,3)]
    public decimal Salary { get; set; }
    //Yazilimsal amacla olusturdugum property  
    //NotMapped]
    //public string YazilimOgren {get; set; }

    [Timestamp]
    //[Comment("Comment")]
    public byte[] RowVersion { get; set; }

    //[ConcurrencyCheck]
    //public int ConcurrencyCheck { get; set; }   
    public DateTime CreatedDate { get; set; }
    public Department Department { get; set; }


}
class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Person> People { get; set; }

}
class Example
{
    //public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Computed { get; set; }
}
// class Entity
//{
//    public int Id {  set; get; }
//    public string X { get; set; }

//}
//class A : Entity    
//{
//    public int Y {  set; get; }
//}
//class B : Entity
//{
//    public int Z { set; get; }
//}
class ApplicationsDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Department> Departmens { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Example> Examples { get; set; }
    //public DbSet<Entity> Entities { get; set; } 
    //public DbSet<A> As {get; set;}
    //public DbSet<B> Bs { get; set;}

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
        #region Required
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname).IsRequired();
        #endregion
        #region MaxLength 
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname)
        //    .HasMaxLength(13);
        #endregion
        #region Precision
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Salary)
        //    .HasPrecision(5, 3);
        #endregion
        #region Unicode  
        modelBuilder.Entity<Person>()
            .Property(e => e.Surname)
            .IsUnicode();
        #endregion
        #region Comment 
        //modelBuilder.Entity<Person>()
        //    .HasComment("Bu bir Commentdir")
        //    .Property(p => p.Surname)
        //    .HasComment("Bu kolon suna yaramaktadir");
        #endregion
        #region ConcurrencyCheck
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.ConcurrencyCheck)
        //    .IsConcurrencyToken();
        #endregion

        #region Compositekey
        //modelBuilder.Entity<Person>()
        //    .HasKey(p => new { p.Id, p.Id2 });
        #endregion
        #region HasDefaultSchema
        //modelBuilder.HasDefaultSchema("ahmet");
        #endregion
        #region Property
        #region HasDefaultValue 
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Salary)
        //    .HasDefaultValue(100);
        #endregion

        #region HasDefaultValueSql
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.CreatedDate)
        //    .HasDefaultValueSql("GETDATE()"); 
        #endregion
        #endregion
        #region HasComputedColumnSql
        //modelBuilder.Entity<Example>()
        //       .Property(p => p.Computed)
        //       .HasComputedColumnSql("[X] + [Y]");
        #endregion
        #region HasConstraintName
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(d => d.People)
        //    .HasForeignKey(p => p.DepartmenId)
        //    .HasConstraintName("ahmet");
        #endregion
        #region HasData
        //modelBuilder.Entity<Department>().HasData(
        //    new Department()
        //    { 
        //        Name = "Departman1",
        //        Id = 1
        //    }
        //    );
        //modelBuilder.Entity<Person>()
        //    .HasData(
        //    new Person
        //    {
        //       Id = 1,
        //       DepartmentId = 1,
        //       Name = "Boris",
        //       Surname = "Manchov",
        //       Salary = 100,
        //       CreatedDate = DateTime.Now
        //    },
        //    new Person
        //    {
        //        Id = 2,
        //        DepartmentId = 1,
        //        Name = "Irina",
        //        Surname = "Manchov",
        //        Salary = 200,
        //        CreatedDate = DateTime.Now
        //    });
        #endregion
        #region HasDiscriminator
        //modelBuilder.Entity<Entity>()
        //    .HasDiscriminator<int>("Ayirici")
        //    .HasValue<A>(1)
        //    .HasValue<B>(2)
        //    .HasValue<Entity>(3);
        #endregion
        #region HasField
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasField(nameof(Person._name));
        #endregion
        #region HasNoKey
        //modelBuilder.Entity<Example>()
        //    .HasNoKey();
        #endregion
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=ConfigurationsDb; Trusted_Connection=True;TrustServerCertificate=True");
    }
}
public class Flight
{
    public int FlightID { get; set; }

    [ForeignKey(nameof(DepartureAirport))]
    public int? DepartureAirportId { get; set; } // Make it nullable

    [ForeignKey(nameof(ArrivalAirport))]
    public int? ArrivalAirportId { get; set; } // Make it nullable

    public string Name { get; set; }

    public virtual Airport DepartureAirport { get; set; }
    public virtual Airport ArrivalAirport { get; set; }

}
public class Airport
{
    public int AirportID { get; set; }
    public string Name { get; set; }
    [InverseProperty(nameof(Flight.DepartureAirport))]
    public virtual ICollection<Flight> DepartingFlights { get; set; }
    [InverseProperty(nameof(Flight.ArrivalAirport))]
    public virtual ICollection<Flight> ArrivingFlight { get; set; }
}
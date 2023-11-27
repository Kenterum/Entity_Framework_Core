using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

ApplicationsDbContext context = new();



#region OnModelCreating
//Genel anlamda veritabani ile ilgili konfigrasyonel operasyonlarin disinda entityler uzerinde konfigrasyonel calismalar yapmamizi saglayan bir fonksiyondur.
#endregion

#region IEntityTypeConfiguration<T> Arayuzu  
//Entity bazli yapilacak olan konfigurasyonlarin o entitye ozer harici bir dosya uzerinde yapmamizi saglayan bir arayuzdur 

//Harici bir dosyada konfigurasyonlairn yurutulmesi merkezi bir yapilandirma noktasi olusturmamizi saglamaktadir 

//Harici bir dosyada konfigurasyonlarin yurutulmesi entity sayisinin fazla oldugu senaryolarda yonetilebilirligi arttiracak ve yapilandirma ile ilgili gelistiricinin yukunu azaltacaktir  
#endregion

#region ApplyConfiguration Metodu 
//Bu metot harici konfigurasyonel siniflarini EF Core'a bildirebilmek icin kullandigimiz bir metotdur.
#endregion

#region ApplyConfigurationsFromAsemmbly Metodu 
//Uygulama bazinda olusturulan harici konfiguasyonel siniflarin herbiri onmodelCreating metodunda ApplyConfiguration ile tek tek bildirmek yerrine bu siniflarin bulundugu Assembly'i bildirerek IentityTypeConfiguration arayuzunden tureyen tum siniflari ilgili ilili entityle karsilik konfigurasyonel deger olarak  baz almasini tek kalemde gerceklestirmemmizi saglayan bir metottur. 
#endregion 



class Order
{
    public int OrderID { get; set; }
    public string Description { get; set; }
    public DateTime OrderDate { get; set; }
}

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.OrderID);
        builder.Property(p => p.Description)
            .HasMaxLength(13);
        builder.Property(p => p.OrderDate)
            .HasDefaultValueSql("GETDATE()");
    }
}

class ApplicationsDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=SavingDB; Trusted_Connection=True;TrustServerCertificate=True");
    }

}









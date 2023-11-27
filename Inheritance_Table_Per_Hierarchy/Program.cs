using Microsoft.EntityFrameworkCore;

ApplicationsDbContext context = new();

#region Table Per Hierarchy (TPH) Nedir? 
//Kalitimsal iliskiye sahip olan entitiylerin oldugu senaryolarda her bir hiyerarsiye karsilik bir tablo olusturulan davranistir. 
#endregion
#region Neden TPH de bir tabloya ihtiyacimiz olsun 
//Icerisinde benzer alanlara sahip olan entityleri migrate ettigimizde her entitye karsilik bir tablo olusturmaktansa bu entityleri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayitlari discriminator kolonu uzerinden birbirlerinden ayirabiliriz. Iste bu tarz bir tablonun olusturulmasi ve bu tarz bir tabloya gore sorgulama, veri ekleme, silme vs. gibi operasyonlarin sekillendirilmesi icin TPH davranisini kullanabiliriz. 
#endregion
#region TPH Nasil Uygulanir?
//EF Core'da entity arasinda temel bir kalitimsal iliski soz konusuysa eger default olarak kabul edilen davranistir.
//O yuzden her hangi bir konfig gerekmez 
//TPH icin tek yapilmasi gereken Entitylerin kendi aralarinda kalitimsal iliskiye sahip olmali ve bu entitylerin hepsi DbContext nesnesine DbSet olarak eklenmelidir. 
#endregion
#region Discriminator Kolonu Nedir?
//Table Per Hierarchy yaklasimi neticesinde kumulatif olarak insa edilmis tablonun hangi entitye karsilik veri tuttugunu ayirt edebilmemizi saglayan bir kolondur.
//Ef Core tarafindan otomatik olarak tabloya yerlestirilir
//Default olarak icerisinde entity isimlerini tutar.
//Discriminator kolonunu komple ozellestirebiliriz. 
#endregion
#region Discriminator Kolon adi Nasil Degistirilir? 
//Oncelikle hiyerarsinin basinda hangi sinif varsa onun Fluent API'da konfigurasyonuna gidilmeli 
//Ardindan HasDiscriminator fonksiyonu ile ozellstirilmeli. 
#endregion

//Employee employee = new Employee() { Name = "Gencay", Surname = "Yildiz" };
//await context.Employees.AddAsync(employee);
//await context.SaveChangesAsync();

#region Discriminator Degerleri nasil Degisitilir
//Yine hiyerarsinin basindaki entity konfigurasyonlarina gelip HasDiscriminator fonksiyonu ile ozellestirmede bulunarak ardindan HasValue fonksiyonu ile hangi entitye karsilik hangi dergerin girecegini belirtilen  turde ifade edebilirsiniz. 
#endregion
#region TPH'da Veri Ekleme
//Davranislarin hicbirinde veri eklerken, silerken, guncellerken vs. normal operasyonlarin disinda bir islem yapilmaz!
//Hangi davranisi kullaniyorsaniz EF Core ona gore arkaplanda modellemeyi gerceklestirecektir

//Employee e1 = new() { Name = "Suleyman", Surname = "Mammadov", Department = "Yazilim Dev" };
//Employee e2 = new() { Name = "Arda", Surname = "Turan", Department = "Yazilim Dev" };
//Customer c1 = new() { Name = "Ahmet", Surname = "Yalcin", CompanyName = "OT Dev" };
//Customer c2 = new() { Name = "Farid", Surname = "Aliyev", CompanyName = "OT Dev" };
//Technician t1 = new() { Name = "Servet", Surname = "Sayar",Branch = "Staff", Department = "Yazilim Dev"};


//await context.Employees.AddAsync(e1);
//await context.Employees.AddAsync(e2);
//await context.Customers.AddAsync(c1);
//await context.Customers.AddAsync(c2);
//await context.Technicians.AddAsync(t1);
//await context.SaveChangesAsync();

#endregion
#region TPH De veri Silme 
//var employee = await context.Employees.FindAsync(4);
//context.Employees.Remove(employee);
//await context.SaveChangesAsync();


var customers = await context.Customers.ToListAsync();

context.Customers.RemoveRange(customers);

await context.SaveChangesAsync();

#endregion 
class Person
{
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

class Employee : Person
{
   public string? Department { get; set; } 
}
class Customer : Person
{
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public string? Branch { get; set; }

}



class ApplicationsDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }  
    public DbSet<Technician> Technicians { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>()
        //    .HasDiscriminator<string>("ayirici")
        //    .HasValue<Person>("A")
        //    .HasValue<Employee>("B")
        //    .HasValue<Customer>("C")
        //    .HasValue<Technician>("D");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=TPHDB; Trusted_Connection=True;TrustServerCertificate=True");
    }

}
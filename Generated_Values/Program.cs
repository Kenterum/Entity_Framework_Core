using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

ApplicationsDbContext context = new();


#region Generated Value Nedir? 
//EF Core'da uretilen degerlerle ilgili cesitli modellerin ayrintilarini yapilandirmamizi saglayan bir konfigurasyondur.
#endregion


#region Default Values
//Ef Cre'da herhangi bir tablonun herhangi bir kolonuna yazilim tarafindan bir deger gonderilmedigi taktirde bu kolona hangi degerin(default value) uretilip yazdirilcagini belirleyen yapilanmalardir. 
#region HasDefaultValue
//Static veri veriyor
#endregion

#region HasDefaultValueSql
//SQL cumlecigi 
#endregion

#endregion

#region Computed Columns 

#region HasComputedColumnSql
//Tablo icerisindeki kolonlar uzerinde yapilan aritmatik islemler neticesinde uretilen kolondur.  
#endregion



#endregion

#region Value Generation

#region Primary Keys 
//Herhangi bir tablodaki satilari kimlik vari sekilde tanimlayan, tekil(unique) olan sutun veya sutunlardir. 
#endregion

#region Identity
//Identity yalnizca otomatik olarak artan bir sutundur. Bir sutun, PK olmaksizin identity olarak tanimlanabilir. Bir tablo icerisinde identity kolonu sadece tek bir tane olarak tanimlanabilir. 
#endregion

//Bu iki ozellik genellille birlikte kullanilmaktadirlar. O yuzden EF Core PK olan bir kolonu otomatik olarak Identity olacak sekilde yapilandirmaktadir. Ancak Boyle olmasi icin bir gereklilik yoktur  

#endregion


#region DatabaseGenerated

#region DatabaseGeneratedOption.None - ValueGeneratedNever 
//Bir kolonda deger uretilmeyecekse eger None ile isaretliyoruz.
//Ef Core'un default olarak PK kolonlarda getirdigi Identity ozelligini kaldirmak istiyorsak eger None'i kullanabiliriz
#endregion
#region DatabaseGeneratedOption.Identity - ValueGeneratedOnAdd
//Her hangi bir kolona Identity ozelligini vermemizi saglayan bir konfigurasyondur.
#region Sayisal Turler
//Eger ki Identity ozelligi bir tabloda sayisal olan bir kolonda kullanilacaksa o durumda ilgili tablodaki pk olan kolondan ozellikle iradeli bir sekilde identity ozelliginin kaldirilmasi gerekmektedir(none)

#endregion

#region Sayisal Olmayan Turlerde
//Eger ki Identity ozelligi bir tabloda sayisal olmayan bir kolonda kullanilacaksa o durumda ilgili tablodaki pk olan kolondan iradeli bir sekilde identity ozelliginin kaldirilmasina gerek yoktur  
#endregion




#endregion
#region DatabaseGeneratedOption.Computed - ValueGeneratedOnAddOrUpdate

#endregion

#endregion


Person p = new()
{
    Name = "Erdem",
    Surname = "Isler",

    Premium = 10,
    TotalGain = 110,
};

await context.Persons.AddAsync(p);
await context.SaveChangesAsync();


class Person
{
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PersonId { get; set; } 
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Premium { get; set; }
    public int Salary { get; set; }
    public int TotalGain { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  PersonCode { get; set; } 

}


class ApplicationsDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Salary)
            .HasDefaultValueSql("Floor(Rand()*1000)");
        //.HasDefaultValue(100);
        modelBuilder.Entity<Person>()
            .Property(p => p.TotalGain)
            .HasComputedColumnSql("([Salary] + [Premium]) *10");
        modelBuilder.Entity<Person>()
            .Property(p => p.PersonId)
            .ValueGeneratedNever();
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.PersonCode)
        //    .HasDefaultValueSql("NEWID()");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=GeneratedValuesDB; Trusted_Connection=True;TrustServerCertificate=True");
    }
}
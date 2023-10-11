using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

ApplicationsDbContext context = new();

#region Saving 100 People Data with Loop
//using (var context = new ApplicationsDbContext())
//{
//    // Örneğin, 5 kişi eklemek için bir döngü oluşturuyoruz.
//    int numberOfPersonsToAdd = 100;
//    for (int i = 1; i <= numberOfPersonsToAdd; i++)
//    {
//        string personName = "Person" + i;
//        string departmentName = "Department" + i;

//        // Yeni Person nesnesi oluşturup veritabanına ekliyoruz.
//        var newPerson = new Person { Name = personName, Department = departmentName };
//        context.People.Add(newPerson);
//    }

//// Değişiklikleri kaydediyoruz.
//await context.SaveChangesAsync();
//    }
#endregion

var person = await context.People.FindAsync(1);
//Person person2 = new()
//{ 
//    Name = "Person 101",
//    Department = "Departmen 101"
//};

//await context.People.AddAsync(person2);
//await context.SaveChangesAsync();   

Console.Read();
#region Backing Fields 


//public class Person
//{
//    public int Id { get; set; }

//    public string name;    
//    public string Name { get => name.Substring(0,3); set => name = value.Substring(0,3); }
//    public string Department { get; set; }
//}
#endregion

#region BackingField Attributes 
//public class Person
//{
//    public int Id { get; set; }

//    public string name;
//    [BackingField(nameof(name))]
//    public string Name { get; set; }
//    public string Department { get; set; }
//}
#endregion

#region HasField Fluent API 
//Fluent Api'da HasField metodu BackingField ozelligine karsilik gelmektedir  
//public class Person
//{
//    public int Id { get; set; }

//    public string name;
//    public string Name { get; set; }
//    public string Department { get; set; }
//}
#endregion


#region Testing Kardesim 
//public class Person
//{
//    public int Id { get; set; }

//    public string name { get; set; }
//    public string Name { get; set; }
//    public string Department { get; set; }
//}

#endregion

#region Field And Property Access
//EF Core sorgulama surecinde entity icerisindeki propertyleri ya da field'lari kullanip kullanmayacaginin 
//davranisini bizlere belirtmektedir 

//EF Core, hicbir ayarlama yoksa varsayilan olarak propertyler uzerinden verileri isler. Eger ki backing
//field bildiriliyorsa field uzerinden isler, yok eger backing field bildirildigi halde davranis belirtiliyorsa yani ne belirtilmisse ona gore islemeyi devam ettirir  

//UsePropertyAccessMode uzerinden davranis modellemesi gerceklestirilebilir
#endregion

#region Field-Only Properties 
//Entitylerde degerleri almak icin peroperty'ler yerine metotlarin kullanildigi veya belirli alanlarin hic gosterilmemesi gerektigi durumlarda(ornegin primary key kolonu) kullanilabilir. 
public class Person
{
    public int Id { get; set; }

    public string name;
    public string Department { get; set; }
    public string GetName()
        => name;
    public string SetName(string value)
        => this.name = value;

}
#endregion








class ApplicationsDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=BackingFieldsDB; Trusted_Connection=True;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //    modelBuilder.Entity<Person>()
        //         .Property(p => p.Name)
        //         .HasField(nameof(Person.name))
        //         .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        //Field : Veri erisim sureclerinde sadece fueldlarin kullanilmasini soyler. Eger Field'in kullanayamayacagi durum soz konusu olursa bir exception firlatir  
        //FieldDuringConstruction: Veri erisim sureclerinde ilgili entityden bir nesne olusturulma surecinde field'larin kullanilmasini soyler  
        //Property: Veri erisim surecinde sadece propertynin kullanilmasini soyler. Eger property'nin kullanilmayacagi durum soz konusuysa (read-only, write-only) bir exception firlatir. 
        //PreferField: 
        //PreferFieldDuringConstruction
        //PreferProperty

        modelBuilder.Entity<Person>()
            .Property(nameof(Person.name));

    }


}


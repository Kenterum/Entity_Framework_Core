using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationsDbContext context = new();



#region One to One Iliskisel Senaryolarda Veri Ekleme 

#region 1.Yontem -> Principal Entity Uzerinden Dependent Entity Verisi Ekleme  
//Person person = new();
//person.Name = "Suleyman";
//person.Adress = new() { PersonAdress = "Kurupelit/Samsun" };

//await context.AddAsync(person);
//await context.SaveChangesAsync();
#endregion

//Eger ki principal entity uzerinden ekleme gerceklestiriliyorsa dependent entity nesnesi veirlmek zorunda degildir. Eger dependent entity uzerinden ekleme islemi
//gerceklestiriliyorsa, burada principal entitynin nesnesine ihtiyacimiz vardir. 
#region 2.Yontem -> Dependent Entity Uzerinden Principal Entity Verisi Ekleme  
//System.InvalidOperationException: 'The value of 'Adress.Id' is unknown when attempting to save changes.
//This is because the property is also part of a foreign key for which the principal entity in the relationship is not known.'  Bu da kaniti 
//Adress adress = new() 
//{ 
//    PersonAdress = "Altindag/Izmir",
//    //Person = new() { Name = "Turgay"} 
//};

//await context.AddAsync(adress);
//await context.SaveChangesAsync(); 
#endregion


//public class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Adress Adress { get; set; }

//}

//public class Adress
//{

//    public int Id { get; set; }
//    public string PersonAdress { get; set; }
//    public Person Person { get; set; }
//}

//public class ApplicationsDbContext : DbContext
//{

//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Adress> Adresses { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(
//            "Server =.; Database=ApplicationDb; Trusted_Connection=True;TrustServerCertificate=True");
//    }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Adress>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Adress)
//            .HasForeignKey<Adress>(a => a.Id);
//    }
//}
#endregion


#region One to Many Iliskisel Senaryolarda Veri Ekleme 

#region 1. Yontem -> Principal Entity Uzerinden Dependent Entity Verisi Ekleme 
#region Nesne Referansi Uzerinden Ekleme 
//Blog blog = new() { Name = "Suleymammadov.com Blog" };
////blog.Posts = new HashSet<Post>();   
//blog.Posts.Add(new() { Title = "Post1" });
//blog.Posts.Add(new() { Title = "Post2" });
//blog.Posts.Add(new() { Title = "Post3" });

//await context.AddAsync(blog);
//await context.SaveChangesAsync();

#endregion

#region Object Initializer Uzerinden Ekleme 
//Blog blog2 = new()
//{
//Name = "A Blog",
//Posts = new HashSet<Post>() { new() { Title = "Post 4" }, new() { Title = "Post 5" } }

//};
//await context.AddAsync(blog2);
//await context.SaveChangesAsync();
#endregion




#endregion

#region 2.Yontem -> Dependent Entity Uzerinden Principal Entity Verisi Ekleme  

//Post post = new()
//{
//    Title = "Post 6"
//    Blog = new() {Name = "B Blog"}
//};

//await context.AddAsync(post);   
//await context.SaveChangesAsync();


#endregion

#region 3.Yontem -> DEVAM EDILECEK

#endregion


public class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();

    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }


}

public class Post
{

    public int Id { get; set; }

    //[ForeignKey(nameof(Blog))]
    public int BlogId { get; set; }
    public string Title { get; set; }
    public Blog Blog { get; set; }
}

public class ApplicationsDbContext : DbContext
{

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=ApplicationDb; Trusted_Connection=True;TrustServerCertificate=True");
    }
}
#endregion
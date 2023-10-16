using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Reflection.Metadata;

ApplicationsDbContext context = new();

#region Shadow Properties

//Entity siniflarinda fiziksel oalrak tanimlanmayan/modellenmeyen ancak ef core tarafindan ilgili entity icin var olan veya var oldugu kabul edilen propertylerdir.
//Tabloda gosterilmesini istemedigimi/luzumlu gormedigimiz/entity instance'i uzerinde iselem yapmayacagimiz kolonlar icin shadow propertyler kullanilabilir.
//Shadow propertly'lerin degerleri ve stateleri Change Tracker tarafindan kontrol edilir. 



#endregion

Blog blog1 = new()
{
    Name = "Kenterum's Blog"
};

Post post1 = new()
{
    Title = "Post 1",
    lastUpdated = true,
};

//var blog1 = await context.FindAsync<Blog>(1);
//var post1 = await context.FindAsync<Post>(7);

//post1.Blog.Id = blog1.Id;
//await context.AddAsync(blog1);
//await context.AddAsync(post1);
//await context.SaveChangesAsync();

#region Foreign Key - Shadow Properties  
//Iliskisel senaryolarda foreign key propertysini tanimlamadigimiz halde Ef Core tarafindan dependent entity e eklenmektedir. Iste bu shadow propertydir 

//var blogs = await context.Blogs.Include(b => b.Posts)
//    .ToListAsync();
//Console.WriteLine();
#endregion

#region Shadow Property Olusturma
//Bir entity uzerinde shadow property olusturmak istiyorsaniz eger Fluent API'i kullanmaniz gerekmektedir.
//modelBuilder.Entity<Blog>()
//           .Property<DateTime>("CreatedDate"); //Shadow Property 

#endregion
#region Shadow Property'e Erisim Saglama

#region ChangeTracker ile Erisim

//var blog = await context.Blogs.FirstAsync();

//var createDate = context.Entry(blog).Property("CreatedDate");
//Console.WriteLine(createDate.CurrentValue);
//Console.WriteLine(createDate.OriginalValue);

//createDate.CurrentValue = DateTime.Now;
//await context.SaveChangesAsync();
//Console.WriteLine(createDate.);
#endregion

#region Ef.Property ile erisim 
//Ozellikle LINQ sorgularinda Shadow Propertylerine erisim icin ef.property sorgulari kullanilir.

//var blogs = await context.Blogs.OrderBy(b => EF.Property<DateTime>(b, "CreatedDate")).ToListAsync();

//var blogs2 = await context.Blogs.Where(b => EF.Property<DateTime>(b, "CreatedDate").Year > 2020).ToListAsync();
//Console.WriteLine();
#endregion

#endregion



public class Blog
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}



public class Post
{
    public int Id { get; set; } 
    public string Title { get; set; }
    [ForeignKey(nameof(Blog))]
    public int BlogId { get; set; } 
    public bool lastUpdated { get; set; }   
    public Blog Blog { get; set; }
    
}






class ApplicationsDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=ApplicationsDb; Trusted_Connection=True;TrustServerCertificate=True");
    }
    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Post>()
    //            .HasOne(b => b.Blog)
    //            .WithMany(p => p.Posts);
    //    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Blog>()
        //    .Property<DateTime>("CreatedDate"); //Shadow Property 
    }
}

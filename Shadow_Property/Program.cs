using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

ApplicationsDbContext context = new();

#region Shadow Properties

//Entity siniflarinda fiziksel oalrak tanimlanmayan/modellenmeyen ancak ef core tarafindan ilgili entity icin var olan veya var oldugu kabul edilen propertylerdir.
//Tabloda gosterilmesini istemedigimi/luzumlu gormedigimiz/entity instance'i uzerinde iselem yapmayacagimiz kolonlar icin shadow propertyler kullanilabilir.
//Shadow propertly'lerin degerleri ve stateleri Change Tracker tarafindan kontrol edilir. 



#endregion

//Blog blog1 = new()
//{
//    Name = "Kenterum's Blog"
//};

//Post post1 = new()
//{
//    Title = "Post 1",
//    lastUpdated = true,
//};

var blog1 = await context.FindAsync<Blog>(1);
var post1 = await context.FindAsync<Post>(7);

post1.Blog.Id = blog1.Id;
await context.AddAsync(blog1);
await context.SaveChangesAsync();



class Blog
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}



class Post
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
    DbSet<Post> Posts { get; set; }
    DbSet<Blog> Blogs { get; set; }
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
}

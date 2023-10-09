using Microsoft.EntityFrameworkCore;

ApplicationsDbContext context = new();

#region One to One Iliskisel Senaryolarda Veri Silme
//Person? person = await context.People
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 3);
//if (person !=null)
//{
//    context.Addresses.Remove(person.Address);
//}

//await context.SaveChangesAsync();
#endregion

#region One to Many Iliskisel Senaryolarda Veri Silme
//Blog blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);
//Post post = blog.Posts.FirstOrDefault(p => p.Id == 3);  

//context.Posts.Remove(post);
//await context.SaveChangesAsync();
#endregion

#region Many to Many Iliskisel Senaryolarda Veri Silme




#endregion

#region Cascade Delete

#endregion


#region ID number degistirme denemesi 
Author author = await context.Authors
    .Include(a => a.AuthorName)
    .FirstOrDefaultAsync(a => a.Id == 4);

Console.WriteLine(author);  

//await context.SaveChangesAsync();

#endregion 



public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
public class Address
{

    public int Id { get; set; }
    public string PersonAddress { get; set; }
    public Person Person { get; set; }
}
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
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}




class ApplicationsDbContext : DbContext
{

    public DbSet<Person> People { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=ApplicationDb; Trusted_Connection=True;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }
}
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
////Kitabin yazarla olan bagini koparma 
//Book book = await context.Books
//    .Include(b => b.Authors)
//    .FirstOrDefaultAsync(b => b.Id == 5);

//Author author = book.Authors.FirstOrDefault(a => a.Id == 5);
//// context.Authors.Remove(author); ---> Bu yazari silmeye kalkar girme bu topa 

//book.Authors.Remove(author);
//await context.SaveChangesAsync();

#endregion

#region Saving Blog 
//Blog blog = new()
//{
//    Name = "Kenterum's Blog",
//    Posts = new List<Post>
//    {
//        new(){ Title = "1. Post "},
//        new(){ Title = "2. Post "},
//        new(){ Title = "3. Post "}


//    }
//};

//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();
#endregion
#region Cascade Delete
//Bu davranis modelleri Fluent API ile konfigure edilebillmektedir.
#region Cascade
//Esas tablodan silinen veri ile karsi/bagimli tabloda bulunan iliskili verilerin silinmesini saglar 

//Blog blog = await context.Blogs.FindAsync(1);
//context.Blogs.Remove(blog);
//await context.SaveChangesAsync();

#endregion

#region SetNull
//Esas tablodan silinen veri ile karsi/bagimli tabloda bulunan iliskili verilere null degerin atanmasini saglar
//One to One senraryolarinda eger ki Foreign Keyy ve primary key kolonlari ayni ise o zaman SetNull davranisini
//Kulllanamayiz!!!

//Blog? blog = await context.Blogs.FindAsync(4);
//context.Blogs.Remove(blog);
//await context.SaveChangesAsync();


#endregion

#region Restrict 
//Esas tablodan herhangi bir veir silinmeye calisildiginde o veriye karsilik dependent table'da iliskisel 
//veriler varsa eger, bu silme islemini engellemesini saglar  


Blog? blog = await context.Blogs.FindAsync(5);
context.Blogs.Remove(blog);
await context.SaveChangesAsync();



#endregion

#endregion


#region ID number degistirme denemesi 
//Author author = await context.Authors.SingleOrDefaultAsync(a => a.Id == 4);
//author.Id=

//context.Authors.Update()
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
    public int? BlogId { get; set; }
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
        // Cascade icin gecerli: Eger Persondan her hangi bir veri silinirse buna karsilik adress tablosunda iliskisel data var ise onu da silecek  

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books);
    }
}
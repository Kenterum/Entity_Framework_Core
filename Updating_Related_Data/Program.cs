using Microsoft.EntityFrameworkCore;

ApplicationsDbContext context = new();


#region One to One iliskisel Senaryolarda Veri Guncelleme  
#region Saving
Person person = new()
{
    Name = "Suleyman",
    Address = new()
    {
        PersonAddress = "Baku/Azerbaycan"
    }
};
Person person2 = new()
{
    Name = "Dogukan"
};

await context.AddAsync(person);
await context.AddAsync(person2);
await context.SaveChangesAsync();

#endregion

#region 1.Durum | Esas tablodaki veriye bagimli veriyi degistirme 
//Person? person = await context.People
//   /*Join/Inner Join Yapilanmasinin Ef Core Hali */ 
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//context.Addresses.Remove(person.Address);
//person.Address = new()
//{
//    PersonAddress = "Atakum/Samsun"
//};
//await context.SaveChangesAsync();
#endregion

#region 2.Durum | Bagimli verinin iliskiler oldugu ana veriyi guncelleme 
//Address? address = await context.Addresses.FindAsync(1);
//address.Id = 2;
//await context.SaveChangesAsync();

//Address? address = await context.Addresses.FindAsync(2);
//context.Addresses.Remove(address);
//await context.SaveChangesAsync();

//address.Person = new()
//{
//    Name = "Rifki"
//};
////Person? person = await context.People.FindAsync(2);
////address.Person = person;
//await context.Addresses.AddAsync(address);

//await context.SaveChangesAsync();
#endregion
#endregion


#region One to Many iliskisel Senaryolarda Veri Guncelleme  
#region Saving 
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

#region 1.Durum | Esas tablodaki veriye bagimli veriyi degistirme 

//Blog? blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);
//Post? silinecekpost = blog.Posts.FirstOrDefault(p => p.Id == 2);
//blog.Posts.Remove(silinecekpost);

//blog.Posts.Add(new() { Title = "4. Post" });
//blog.Posts.Add(new() { Title = "5. Post" });

//await context.SaveChangesAsync();
#endregion
#region 2.Durum | Bagimli verinin iliskiler oldugu ana veriyi guncelleme 
//Post? post = await context.Posts.FindAsync(4);
//post.Blog = new()
//{
//    Name = "2. Blog"
//};
//await context.SaveChangesAsync();   


//Post? post = await context.Posts.FindAsync(5);
//Blog? blog = await context.Blogs.FindAsync(2);
//post.Blog = blog;

//await context.SaveChangesAsync();
#endregion

#endregion
#region Many to Many iliskisel Senaryolarda Veri Guncelleme  
#region Saving 
//Book book1 = new() { BookName = "1. Kitap" };
//Book book2 = new() { BookName = "2. Kitap" };
//Book book3 = new() { BookName = "3. Kitap" };


//Author author1 = new() { AuthorName = "1. Yazar" };
//Author author2 = new() { AuthorName = "2. Yazar" };
//Author author3 = new() { AuthorName = "3. Yazar" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book1.Authors.Add(author1);
//book2.Authors.Add(author2);
//book3.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddAsync(book1);
//await context.AddAsync(book2);
//await context.AddAsync(book3);

//await context.SaveChangesAsync();



#endregion

#region 1.Ornek 
//Kitap yazar iliskisinde bir kitabi ve ya bir yazari birden cok veri ile eslestirme//
//Book? book = await context.Books.FindAsync(5);
//Author? author = await context.Authors.FindAsync(6);
//book.Authors.Add(author);

//await context.SaveChangesAsync();   
#endregion
#region 2.Ornek 
//Bu sorguda diyoruz ki. Eger 6 Id li Author 6 id li kitaptan baska bir kitap ile iliskiliyse onunla olan
//iliskisini kaldir  
//Author? author = await context.Authors
//           .Include(a => a.Books)
//           .FirstOrDefaultAsync(a => a.Id == 6);
//foreach (var book in author.Books)
//{
//    if(book.Id !=6)
//        author.Books.Remove(book);
//}


//await context.SaveChangesAsync();
#endregion
#region 3. Ornek 
//4 Id  sine sahip olan kitabi 5 Id sine sahip olan yazarla iliskisini keselim

//Ardindan 4 Id sine sahip olan kitabi 4 Id sine sahip olan yazarla iliskilendirelim.

//Ekstradan da dorduncu yazari ekleyelim 
#region Benim Kod 
//Author? author = await context.Authors.FindAsync(5);
//Book? book = await context.Books.FindAsync(4);
//    book.Authors.Remove(author);
//await context.SaveChangesAsync(); //Birinci task 

//Author? Author2 = await context.Authors.FindAsync(4);
//book.Authors = Author2;
//await context.SaveChangesAsync();

//Author Authors = new()
//{
//    AuthorName = "4. Yazar"
//};
#endregion

//Book? book = await context.Books
//    .Include(b => b.Authors)
//    .FirstOrDefaultAsync(b => b.Id == 4);
//Author silinecekYazar = book.Authors.FirstOrDefault(a => a.Id == 7);
//book.Authors.Remove(silinecekYazar);

////Author? Author2 = await context.Authors.FindAsync(4);
////book.Authors.Add(Author2);
////book.Authors.Add(new() { AuthorName = "4. Yazar" });

//await context.SaveChangesAsync(); 
#endregion


#endregion


public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address{ get; set; } 
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
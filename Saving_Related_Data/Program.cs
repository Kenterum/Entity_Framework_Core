using Microsoft.EntityFrameworkCore;

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
//    Title = "Post 6",
//    Blog = new() { Name = "B Blog" }
//};

//await context.AddAsync(post);
//await context.SaveChangesAsync();




#endregion

#region 3.Yontem -> Foreign Key Kolonu Uzerinden Veri Ekleme
//Birinci ve ikinci yontemler, hic olmayan verilerin iliskisel olarak eklenmesini saglarken, bu 3. yonrem onceden eklenmis olan bir principal entity verisiyle yeni 
//dependent entitylerin iliskisel olarak elestirilmesini saglamaktadir


//Post post = new()
//{
//    BlogId = 1,
//    Title = "Post 7"
//};

//await context.AddAsync(post);
//await context.SaveChangesAsync();

#endregion


//public class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();

//    }
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public ICollection<Post> Posts { get; set; }


//}

//public class Post
//{

//    public int Id { get; set; }

//    //[ForeignKey(nameof(Blog))]
//    public int BlogId { get; set; }
//    public string Title { get; set; }
//    public Blog Blog { get; set; }
//}

//public class ApplicationsDbContext : DbContext
//{

//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Post> Posts { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(
//            "Server =.; Database=ApplicationDb; Trusted_Connection=True;TrustServerCertificate=True");
//    }
//}
#endregion


#region Many to Many İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem
//n t n ilişkisi eğer ki default convention üzerinden tasarlanmışsa kullanılan bir yöntemdir.

//Book book = new()
//{
//    BookName = "A Kitabı",
//    Authors = new HashSet<Author>()
//    {
//        new(){ AuthorName = "Hilmi" },
//        new(){ AuthorName = "Ayşe" },
//        new(){ AuthorName = "Fatma" },
//    }
//};

//await context.Books.AddAsync(book);
//await context.SaveChangesAsync();



//class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<Author>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }

//    public ICollection<Author> Authors { get; set; }
//}

//class Author
//{
//    public Author()
//    {
//        Books = new HashSet<Book>();
//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }

//    public ICollection<Book> Books { get; set; }
//}
#endregion
#region 2. Yöntem
//n t n ilişkisi eğer ki fluent api ile tasarlanmışsa kullanılan bir yöntemdir.

Author author = new()
{
    AuthorName = "Mustafa",
    Books = new HashSet<AuthorBook>() {
        new(){ BookId = 1},
        new(){ Book = new () { BookName = "B Kitap" } }
    }
};

await context.AddAsync(author);
await context.SaveChangesAsync();

class Book
{
    public Book()
    {
        Authors = new HashSet<AuthorBook>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<AuthorBook> Authors { get; set; }
}

class AuthorBook
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}

class Author
{
    public Author()
    {
        Books = new HashSet<AuthorBook>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<AuthorBook> Books { get; set; }
}
#endregion



class ApplicationsDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server =.; Database=ApplicationDb; Trusted_Connection=True;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBook>()
            .HasKey(ba => new { ba.AuthorId, ba.BookId });

        modelBuilder.Entity<AuthorBook>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<AuthorBook>()
            .HasOne(ba => ba.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(ba => ba.AuthorId);
    }
}
#endregion


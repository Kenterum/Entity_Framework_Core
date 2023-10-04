using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new();

#region Default Convention 
//Default convention yonteminde bire cok iliskiyi kurarken foreign key kolonuna karsilik gelen bir property tanimlamak mecburiyetinde degiliz. Eger tanimlamaz isek Ef Core 
//bunu kendisini tamamamlayacak. Eger tanimkar  isek, tanimladigimizi baz alacaktir. 
//class Calisan //Dependent entity (Calisan Departmana Bagli) 
//{
//    public int Id { get; set; }
//    public int DepartmanId { get; set; }    
//    public string Adi { get; set; }

//    public Departman Departman { get; set; }    

//}

//class Departman //Principal entity 
//{

//    public int Id { get; set; }

//    public string DepartmanAdi { get; set; }   

//    public ICollection<Calisan> Calisanlar {get; set; }
//}



#endregion

#region Data Annotations 
//ForeignKey Zorunludur 
//Defult convention yonteminde foreign key kolonuna karsilik gelen property i tanimladigimizda bu property ismi temel geleneksel entity tanimlama kurallarina uymuyorsa eger
//Data annotations'lar ile mudahalede bulunabiliriz  
//class Calisan //Dependent entity (Calisan Departmana Bagli) 
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int DId { get; set; }
//    public string Adi { get; set; }

//    public Departman Departman { get; set; }

//}

//class Departman //Principal entity 
//{

//    public int Id { get; set; }

//    public string DepartmanAdi { get; set; }

//    public ICollection<Calisan> Calisanlar { get; set; }
//}

#endregion

#region Fluent API 

class Calisan //Dependent Entity    
{
    public int Id { get; set; } 

    public int DId { get; set; } //Ornek olarak koyuldu. Bir spesifik foreignkey olmasa da ef core kendisi belirleyebilir  
    public string Adi { get; set; }


    public Departman Departman { get; set; }

}

class Departman //Principal entity 
{

    public int Id { get; set; }

    public string DepartmanAdi { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; }
}


#endregion





public class ESirketDbContext : DbContext
{
    DbSet<Calisan> Calisanlar { get; set; }
    DbSet<Departman> Departmanlar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=ESirketDb; Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.Departman)
            .WithMany(d => d.Calisanlar)
            .HasForeignKey(c => c.DId);
    }
}


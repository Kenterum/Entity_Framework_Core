using Microsoft.EntityFrameworkCore;
ECommerceDbContext context = new();




#region Relationships Terimleri 

#region Principal Entity (Asil Entity)
//Kendi basina var olbilen tabloyu modelleyen entity'e denir. 

//Departmanlar tablosunu modelleyen 'Departman' entity'sidir.

#endregion

#region Dependent Entity(Bagimli Entity)
//Kendi basina var olmayan, bir baska tabloya bagimli(iliskisel olarak bagimli) olan tabloyu modelleyen entity'e denir

//Calisanlar tablosunu modelleyen 'Calisan' entity'sidir. 



#endregion

#region Foreign Key
//Principal Entity ile Dependent Entity arasindaki iliskiyi saglayan key'dir  

//Dependent Entity'de tanimlanir 
//Principal Entity'de ki Principal Key'i tutar.

#endregion

#region Principal Key
//Principal Entity'deki id'nin kendisidir
//Principal Entity'nin kimligi olan kolonu ifade eden propertydir.
#endregion
#endregion

#region Navigation Property

//Iliskisel tablolar arasindaki fiziksel erisimi entity class'lari uzerinden saglayan property'lerdir

//Bir property'nin navigation property olabilmesi icin kesinlikle entity turunden olmasi gerekiyor. 

//Navigation Property'ler entity'lerdeki tanimlarina gore n'e n yahut 1'e n seklinde iliski turlarini ifade etmektedirler 

#endregion

#region Iliski Turleri

#region One to One 
//Calisan ile adresi arasindaki iliski
//Kari koca arasindaki iliski 
#endregion

#region One to Many 
//Calisan ile departman arasindaki iliski
//Anne ve cocuklari arasindaki iliski
#endregion

#region Many to Many 
//Calisanlar ile proje arasindaki iliski
//Kardesler arasindaki iliski 
#endregion





#endregion

#region Iliski Yapilandirma Yontemleri

#region Default Conventions 
//Varsayilan entity kurallarini kullanarak yapilan iliski yapilandirma yontemleridir.  

//Navigation property'leri kullanarak iliski sablonlarini cikarmaktadir. 
#endregion

#region Data Annotations Attribtes
//Entity'nin niteliklerine gore ince ayarlar yapmamizi saglayan attribute'lardir [Key] , [ForeignKey]
#endregion

#region Fluent API 

//Entity modellerindeki iliskileri yapilandirirken daha detayli calismamizi saglayan yontemdir.  


#endregion

#region HasOne
//Ilgili entity'nin iliskisel entity'ye birebir ya da bire cok olacak sekilde iliskisini yapilandirmaya baslayan metottur 
#endregion

#region HasMany
//Ilgili entity'nin iliskisel entity'ye  bire cok  ya da coka cok olacak sekilde  iliskisini yapilandirmaya baslayan metottur
#endregion

#region WithMany
//HasOne ya da HasMany'den sonra bire cok ya da coka cok olacak sekilde iliski yapilandirmasini tamamlayan metottur. 

#endregion

#endregion











public class ECommerceDbContext : DbContext
{


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server =.; Database=IsletmeDB; Trusted_Connection=True;TrustServerCertificate=True");
    }
}


public class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }

    public Departman Departman { get; set; } //Navigation Property
}
public class Departman
{
    public int Id { get; set; }

    public string DepartmanAdi { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; }
}

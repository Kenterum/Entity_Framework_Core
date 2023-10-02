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


#region Navigation Property

//Iliskisel tablolar arasindaki fiziksel erisimi entity class'lari uzerinden saglayan property'lerdir

//Bir property'nin navigation property olabilmesi icin kesinlikle entity turunden olmasi gerekiyor. 



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
    public string CalisanAdi {get; set; }
    public int DepartmanId { get; set; }    

    public Departman Departman {  get; set; } //Navidation Property
}
public class Departman
{
    public int Id { get; set; }

    public string DepartmanAdi { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; }    
}
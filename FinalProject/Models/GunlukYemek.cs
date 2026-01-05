using SQLite;

namespace FinalProject.Models
{
    [Table("gunluk_yemekler")]
    public class GunlukYemek
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Tarih { get; set; } = string.Empty;
        
        public string OgunTipi { get; set; } = string.Empty;
        
        public string YemekAdi { get; set; } = string.Empty;
        
        public double Kalori { get; set; }
        
        public DateTime EklenmeTarihi { get; set; }
    }
}
using SQLite;

namespace FinalProject.Models
{
    [Table("yakilan_kalori")]
    public class YakilanKalori
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Tarih { get; set; } = string.Empty;
        
        public double Kalori { get; set; }
        
        public DateTime EklenmeTarihi { get; set; }
    }
}
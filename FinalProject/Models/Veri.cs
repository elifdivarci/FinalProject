using System;
using SQLite;

namespace FinalProject.Models
{
    [Table("veriler")]
    public class Veri
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Cinsiyet { get; set; } = string.Empty;
        public int Yas { get; set; }
        public double Boy { get; set; }
        public double Kilo { get; set; }
        public double BMH { get; set; }
        public double AlinanKalori { get; set; } = 0;
        public double YakilanKalori { get; set; } = 0;
        public DateTime KayitTarihi { get; set; }
    }
}

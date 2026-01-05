using SQLite;
using FinalProject.Models;

namespace FinalProject.Services
{
    public class DbService
    {
        private SQLiteAsyncConnection _database;

        public DbService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "finalproject.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Veri>().Wait();
            _database.CreateTableAsync<GunlukYemek>().Wait();
            _database.CreateTableAsync<YakilanKalori>().Wait();
        }
        
        public Task<List<Veri>> GetVerilerAsync()
        {
            return _database.Table<Veri>().ToListAsync();
        }

        public async Task<Veri?> GetSonVeriAsync()
        {
            var veriler = await _database.Table<Veri>().ToListAsync();
            return veriler.LastOrDefault();
        }

        public Task<int> SaveVeriAsync(Veri veri)
        {
            if (veri.Id != 0)
            {
                return _database.UpdateAsync(veri);
            }
            else
            {
                veri.KayitTarihi = DateTime.Now;
                return _database.InsertAsync(veri);
            }
        }

        public Task<int> DeleteVeriAsync(Veri veri)
        {
            return _database.DeleteAsync(veri);
        }
        
        public Task<int> SaveGunlukYemekAsync(GunlukYemek yemek)
        {
            yemek.EklenmeTarihi = DateTime.Now;
            return _database.InsertAsync(yemek);
        }

        public async Task<List<GunlukYemek>> GetGunlukYemeklerAsync(string tarih)
        {
            return await _database.Table<GunlukYemek>()
                .Where(y => y.Tarih == tarih)
                .ToListAsync();
        }

        public async Task<double> GetGunlukToplamKaloriAsync(string tarih)
        {
            var yemekler = await GetGunlukYemeklerAsync(tarih);
            return yemekler.Sum(y => y.Kalori);
        }

        public async Task<double> GetOgunToplamKaloriAsync(string tarih, string ogunTipi)
        {
            var yemekler = await _database.Table<GunlukYemek>()
                .Where(y => y.Tarih == tarih && y.OgunTipi == ogunTipi)
                .ToListAsync();
            return yemekler.Sum(y => y.Kalori);
        }

        public async Task<Dictionary<string, double>> GetHaftalikKaloriAsync()
        {
            var sonuc = new Dictionary<string, double>();
            var bugun = DateTime.Today;

            for (int i = 6; i >= 0; i--)
            {
                var tarih = bugun.AddDays(-i).ToString("yyyy-MM-dd");
                var toplam = await GetGunlukToplamKaloriAsync(tarih);
                sonuc[tarih] = toplam;
            }

            return sonuc;
        }

        public Task<int> DeleteGunlukYemekAsync(GunlukYemek yemek)
        {
            return _database.DeleteAsync(yemek);
        }

        public async Task<int> DeleteOgunYemekleriAsync(string tarih, string ogunTipi)
        {
            var yemekler = await _database.Table<GunlukYemek>()
                .Where(y => y.Tarih == tarih && y.OgunTipi == ogunTipi)
                .ToListAsync();
            
            int silinensayisi = 0;
            foreach (var yemek in yemekler)
            {
                silinensayisi += await _database.DeleteAsync(yemek);
            }
            return silinensayisi;
        }
        
        public Task<int> SaveYakilanKaloriAsync(YakilanKalori yakilanKalori)
        {
            yakilanKalori.EklenmeTarihi = DateTime.Now;
            return _database.InsertAsync(yakilanKalori);
        }

        public async Task<double> GetGunlukYakilanKaloriAsync(string tarih)
        {
            var kayitlar = await _database.Table<YakilanKalori>()
                .Where(y => y.Tarih == tarih)
                .ToListAsync();
            return kayitlar.Sum(y => y.Kalori);
        }

        public async Task<int> DeleteYakilanKaloriAsync(string tarih)
        {
            var kayitlar = await _database.Table<YakilanKalori>()
                .Where(y => y.Tarih == tarih)
                .ToListAsync();
            
            int silinensayisi = 0;
            foreach (var kayit in kayitlar)
            {
                silinensayisi += await _database.DeleteAsync(kayit);
            }
            return silinensayisi;
        }
    }
}
using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject
{
    public partial class YakilanKaloriPage : ContentPage
    {
        private AnaSayfa _anaSayfaPage;
        private DbService _dbService;
        private double _hesaplananKalori = 0;

        public YakilanKaloriPage(AnaSayfa anaSayfaPage)
        {
            InitializeComponent();
            _anaSayfaPage = anaSayfaPage;
            _dbService = new DbService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await BugunkuVeriYukle();
        }

        private async Task BugunkuVeriYukle()
        {
            var tarihStr = DateTime.Today.ToString("yyyy-MM-dd");
            var mevcutKalori =
                await _dbService.GetGunlukYakilanKaloriAsync(tarihStr);

            if (mevcutKalori > 0)
            {
                LabelYakilanKalori.Text = mevcutKalori.ToString("F0");
                FrameSonuc.IsVisible = true;
                BtnKaydet.IsVisible = false;
            }
        }

        private async void OnHesaplaClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryAdimSayisi.Text) ||
                !int.TryParse(EntryAdimSayisi.Text, out int adimSayisi) ||
                adimSayisi <= 0)
            {
                await DisplayAlert(
                    "Uyarı",
                    "Lütfen geçerli bir adım sayısı girin.",
                    "Tamam");
                return;
            }

            _hesaplananKalori = (adimSayisi / 1000.0) * 50;

            LabelYakilanKalori.Text = _hesaplananKalori.ToString("F0");
            FrameSonuc.IsVisible = true;
            BtnKaydet.IsVisible = true;
        }

        private async void OnKaydetClicked(object sender, EventArgs e)
        {
            var tarihStr = DateTime.Today.ToString("yyyy-MM-dd");
            
            var yakilanKalori = new YakilanKalori
            {
                Tarih = tarihStr,
                Kalori = _hesaplananKalori,
                EklenmeTarihi = DateTime.Now
            };

            await _dbService.SaveYakilanKaloriAsync(yakilanKalori);

            await _anaSayfaPage.VerileriYukle();
            await _anaSayfaPage.HaftalikGrafigeCiz();

            await DisplayAlert(
                "Başarılı",
                $"{_hesaplananKalori:F0} kalori eklendi!",
                "Tamam");

            BtnKaydet.IsVisible = false;
            await Navigation.PopAsync();
        }
    }
}

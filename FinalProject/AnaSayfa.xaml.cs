using FinalProject.Models;
using FinalProject.Services;
using System.Globalization;

namespace FinalProject
{
    public partial class AnaSayfa : ContentPage
    {
        private DbService _dbService;
        private double _bmh;
        private DateTime _secilenTarih;
        private CultureInfo _turkishCulture;
        private DateTime? _sonKahvaltiUyariZamani = null;
        private bool _bugunKahvaltiUyarisiKapatildi = false;
        private bool _kaloriAsimUyarisiGosterildi = false;

        public AnaSayfa(double bmh)
        {
            InitializeComponent();
            _dbService = new DbService();
            _bmh = bmh;
            _secilenTarih = DateTime.Today;
            _turkishCulture = new CultureInfo("tr-TR");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (_secilenTarih.Date != DateTime.Today)
            {
                _kaloriAsimUyarisiGosterildi = false;
            }

            if (_sonKahvaltiUyariZamani?.Date != DateTime.Today)
            {
                _sonKahvaltiUyariZamani = null;
                _bugunKahvaltiUyarisiKapatildi = false;
            }

            TarihGuncelle();
            await VerileriYukle();
            await HaftalikGrafigeCiz();
            await KahvaltiKontrolu();
        }

        private void TarihGuncelle()
        {
            LabelSecilenTarih.Text = _secilenTarih.ToString("d MMMM yyyy", _turkishCulture);
        }

        public async Task VerileriYukle()
        {
            var tarihStr = _secilenTarih.ToString("yyyy-MM-dd");

            var kahvaltiKalori = await _dbService.GetOgunToplamKaloriAsync(tarihStr, "Kahvaltı");
            var ogleKalori = await _dbService.GetOgunToplamKaloriAsync(tarihStr, "Öğle");
            var aksamKalori = await _dbService.GetOgunToplamKaloriAsync(tarihStr, "Akşam");

            var toplamAlinan = kahvaltiKalori + ogleKalori + aksamKalori;
            LabelAlinanKalori.Text = toplamAlinan.ToString("F0");

            var yakilanKalori = await _dbService.GetGunlukYakilanKaloriAsync(tarihStr);
            LabelYakilanKalori.Text = yakilanKalori.ToString("F0");

            var kalan = _bmh - toplamAlinan + yakilanKalori;
            LabelKalanKalori.Text = kalan.ToString("F0");

            await KaloriAsimKontrolu(toplamAlinan, yakilanKalori);
        }

        private async Task KahvaltiKontrolu()
        {
            if (_secilenTarih.Date != DateTime.Today)
                return;

            if (_bugunKahvaltiUyarisiKapatildi)
                return;

            var simdi = DateTime.Now;
            var saat = simdi.Hour;

            if (saat < 6 || saat >= 24)
                return;

            if (_sonKahvaltiUyariZamani.HasValue &&
                (simdi - _sonKahvaltiUyariZamani.Value).TotalMinutes < 30)
            {
                return;
            }

            var tarihStr = DateTime.Today.ToString("yyyy-MM-dd");
            var kahvaltiKalori =
                await _dbService.GetOgunToplamKaloriAsync(tarihStr, "Kahvaltı");

            if (kahvaltiKalori == 0)
            {
                var secim = await DisplayActionSheet(
                    "Kahvaltı yapmadın, günün en önemli öğününü tamamlamayı unutma",
                    null,
                    null,
                    "Tamam",
                    "Bugün bir daha gösterme");

                if (secim == "Bugün bir daha gösterme")
                {
                    _bugunKahvaltiUyarisiKapatildi = true;
                }
                else
                {
                    _sonKahvaltiUyariZamani = simdi;
                }
            }
        }
        
        private async Task KaloriAsimKontrolu(double toplamAlinan, double yakilanKalori)
        {
            if (_kaloriAsimUyarisiGosterildi)
                return;

            var netKalori = toplamAlinan - yakilanKalori;

            if (netKalori > _bmh)
            {
                var asildi = netKalori - _bmh;

                await DisplayAlert(
                    "DİKKAT!",
                    $"Günlük BMH'nizi aştınız!\n\n" +
                    $"BMH Hedefiniz: {_bmh:F0} kcal\n" +
                    $"Alınan: {toplamAlinan:F0} kcal\n" +
                    $"Yakılan: {yakilanKalori:F0} kcal\n" +
                    $"Net Kalori: {netKalori:F0} kcal\n" +
                    $"Aşılan Miktar: {asildi:F0} kcal",
                    "Tamam"
                );
                
                _kaloriAsimUyarisiGosterildi = true;
            }
        }

             public async Task HaftalikGrafigeCiz()
        {
            var haftalikVeri = await _dbService.GetHaftalikKaloriAsync();

            CubukGrid.Children.Clear();
            GunlerGrid.Children.Clear();

            if (haftalikVeri.Count == 0) return;

            var maxKalori = haftalikVeri.Values.Max();
            if (maxKalori == 0) maxKalori = 2000;

            LabelMaxKalori.Text = maxKalori.ToString("F0");
            LabelMinKalori.Text = "0";

            int kolon = 0;

            foreach (var kvp in haftalikVeri)
            {
                var tarih = DateTime.Parse(kvp.Key);
                var kalori = kvp.Value;

                var oran = kalori / maxKalori;
                var cubukYukseklik = oran * 110;

                var cubukContainer = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = 20 },   // kalori
                        new RowDefinition { Height = 120 },  // çubuk
                        new RowDefinition { Height = 25 }    // gün
                    },
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill
                };

                var kaloriLabel = new Label
                {
                    Text = kalori.ToString("F0"),
                    FontSize = 11,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End
                };

                var cubuk = new BoxView
                {
                    HeightRequest = cubukYukseklik,
                    WidthRequest = 18,
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.Center,
                    CornerRadius = 5,
                    Color = _secilenTarih.Date == tarih.Date
                        ? Color.FromArgb("#3498DB")
                        : Color.FromArgb("#95A5A6")
                };

                var gunLabel = new Label
                {
                    Text = tarih.ToString("ddd", _turkishCulture)
                        .Substring(0, 1)
                        .ToUpper(),
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                Grid.SetRow(kaloriLabel, 0);
                Grid.SetRow(cubuk, 1);
                Grid.SetRow(gunLabel, 2);

                cubukContainer.Children.Add(kaloriLabel);
                cubukContainer.Children.Add(cubuk);
                cubukContainer.Children.Add(gunLabel);

                Grid.SetColumn(cubukContainer, kolon);
                CubukGrid.Children.Add(cubukContainer);

                kolon++;
            }
        }

        public async void KaloriEkle(double kalori, string yemekAdi, string ogunTipi)
        {
            var tarihStr = DateTime.Today.ToString("yyyy-MM-dd");

            var yemek = new GunlukYemek
            {
                Tarih = tarihStr,
                OgunTipi = ogunTipi,
                YemekAdi = yemekAdi,
                Kalori = kalori
            };

            await _dbService.SaveGunlukYemekAsync(yemek);
            await VerileriYukle();
            await HaftalikGrafigeCiz();
        }

        public async void KaloriCikar(double kalori, string yemekAdi, string ogunTipi)
        {
            var tarihStr = DateTime.Today.ToString("yyyy-MM-dd");

            var yemekler = await _dbService.GetGunlukYemeklerAsync(tarihStr);
            var silinecek = yemekler.FirstOrDefault(y =>
                y.OgunTipi == ogunTipi &&
                y.YemekAdi == yemekAdi &&
                y.Kalori == kalori);

            if (silinecek != null)
            {
                await _dbService.DeleteGunlukYemekAsync(silinecek);
                await VerileriYukle();
                await HaftalikGrafigeCiz();
            }
        }

        private async void OnOgunlerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OgunlerPage(this));
        }

        private async void OnYakilanClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new YakilanKaloriPage(this));
        }

        private async void OnProfilClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}

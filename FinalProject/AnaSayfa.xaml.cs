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
            TarihGuncelle();
            await VerileriYukle();
            await HaftalikGrafigeCiz();
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
        }

        public async Task HaftalikGrafigeCiz()
{
    var haftalikVeri = await _dbService.GetHaftalikKaloriAsync();

    CubukGrid.Children.Clear();
    GunlerGrid.Children.Clear();

    if (haftalikVeri.Count == 0)
        return;

    var maxKalori = haftalikVeri.Values.Max();
    if (maxKalori == 0)
        maxKalori = 2000;

    LabelMaxKalori.Text = maxKalori.ToString("F0");
    LabelMinKalori.Text = "0";

    int kolon = 0;

    foreach (var kvp in haftalikVeri)
    {
        var tarih = DateTime.Parse(kvp.Key);
        var kalori = kvp.Value;

        var oran = maxKalori > 0 ? kalori / maxKalori : 0;
        var cubukYukseklik = oran * 110; // biraz küçülttük

        var cubukContainer = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto }, // sayı
                new RowDefinition { Height = new GridLength(120) } // çubuk alanı
            },
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            Margin = new Thickness(2, 0)
        };

        var kaloriLabel = new Label
        {
            Text = kalori.ToString("F0"),
            FontSize = 11,
            TextColor = Color.FromArgb("#2C3E50"),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End
        };

        var cubuk = new BoxView
        {
            HeightRequest = cubukYukseklik,
            VerticalOptions = LayoutOptions.End,
            CornerRadius = 5,
            Color = _secilenTarih.Date == tarih.Date
                ? Color.FromArgb("#3498DB")
                : Color.FromArgb("#95A5A6")
        };

        Grid.SetRow(kaloriLabel, 0);
        Grid.SetRow(cubuk, 1);

        cubukContainer.Children.Add(kaloriLabel);
        cubukContainer.Children.Add(cubuk);

        Grid.SetColumn(cubukContainer, kolon);
        CubukGrid.Children.Add(cubukContainer);

        var gunLabel = new Label
        {
            Text = tarih.ToString("ddd", _turkishCulture).Substring(0, 1).ToUpper(),
            FontSize = 12,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 8, 0, 0),


            TextColor = _secilenTarih.Date == tarih.Date
                ? Color.FromArgb("#3498DB")
                : Color.FromArgb("#7F8C8D"),
            FontAttributes = _secilenTarih.Date == tarih.Date
                ? FontAttributes.Bold
                : FontAttributes.None
        };


        Grid.SetColumn(gunLabel, kolon);
        GunlerGrid.Children.Add(gunLabel);

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
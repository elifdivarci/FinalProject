using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject
{
    public partial class MainPage : ContentPage
    {
        private DbService _dbService;
        private double _hesaplananBMH;

        public MainPage()
        {
            InitializeComponent();
            _dbService = new DbService();
        }

        private async void OnHesaplaClicked(object sender, EventArgs e)
        {
            LabelSonuc.IsVisible = false;
            BtnDevamEt.IsVisible = false;
            
            if (!RadioKadin.IsChecked && !RadioErkek.IsChecked)
            {
                await DisplayAlert("Uyarı", "Lütfen cinsiyetinizi seçin.", "Tamam");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(EntryYas.Text) || !int.TryParse(EntryYas.Text, out int yas) || yas <= 0)
            {
                await DisplayAlert("Uyarı", "Lütfen geçerli bir yaş girin.", "Tamam");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(EntryBoy.Text) || !double.TryParse(EntryBoy.Text, out double boy) || boy <= 0)
            {
                await DisplayAlert("Uyarı", "Lütfen geçerli bir boy değeri girin.", "Tamam");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(EntryKilo.Text) || !double.TryParse(EntryKilo.Text, out double kilo) || kilo <= 0)
            {
                await DisplayAlert("Uyarı", "Lütfen geçerli bir kilo değeri girin.", "Tamam");
                return;
            }
            
            string cinsiyet = RadioKadin.IsChecked ? "Kadın" : "Erkek";
            
            double bmh;
            if (cinsiyet == "Erkek")
            {
                bmh = 66.47 + (13.75 * kilo) + (5 * boy) - (6.76 * yas);
            }
            else
            {
                bmh = 655.10 + (9.56 * kilo) + (1.85 * boy) - (4.68 * yas);
            }
            
            _hesaplananBMH = bmh;
            
            LabelSonuc.Text = $"Bazal Metabolik Hızınız: {bmh:F2} kalori/gün";
            LabelSonuc.IsVisible = true;
            BtnDevamEt.IsVisible = true;
            
            var veri = new Veri
            {
                Cinsiyet = cinsiyet,
                Yas = yas,
                Boy = boy,
                Kilo = kilo,
                BMH = bmh,
                KayitTarihi = DateTime.Now
            };

            await _dbService.SaveVeriAsync(veri);
        }

        private async void OnDevamEtClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AnaSayfa(_hesaplananBMH));
        }
    }
}
namespace FinalProject
{
    public partial class AnaSayfa : ContentPage
    {
        private double _toplamKalori;
        private double _alinanKalori = 0;
        private double _yakilanKalori = 0;
        
        public AnaSayfa(double bmh)
        {
            InitializeComponent();
            _toplamKalori = bmh;
            UpdateKaloriGostergeleri();
        }

        public void UpdateKaloriGostergeleri()
        {
            LabelAlinanKalori.Text = $"{_alinanKalori:F0}";
            LabelYakilanKalori.Text = $"{_yakilanKalori:F0}";
            
            double kalan = _toplamKalori - _alinanKalori + _yakilanKalori;
            LabelKalanKalori.Text = $"{kalan:F0}";
        }

        public void KaloriEkle(double kalori)
        {
            _alinanKalori += kalori;
            UpdateKaloriGostergeleri();
        }

        public void KaloriCikar(double kalori)
        {
            _alinanKalori -= kalori;
            if (_alinanKalori < 0) _alinanKalori = 0;
            UpdateKaloriGostergeleri();
        }

        private async void OnKahvaltiClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KahvaltiPage(this));
        }

        private async void OnOgleClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Öğle", "Öğle yemeği ekleme özelliği yakında eklenecek", "Tamam");
        }

        private async void OnAksamClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Akşam", "Akşam yemeği ekleme özelliği yakında eklenecek", "Tamam");
        }
    }
}
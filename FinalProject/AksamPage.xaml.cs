namespace FinalProject
{
    public partial class AksamPage : ContentPage
    {
        private AnaSayfa _anaSayfaPage;
        private int _kofteAdet = 0;
        private int _makarnaAdet = 0;
        private int _patatesAdet = 0;
        private int _mantiAdet = 0;
        
        private const int KOFTE_KALORI = 235;
        private const int MAKARNA_KALORI = 330;
        private const int PATATES_KALORI = 250;
        private const int MANTI_KALORI = 332;

        public AksamPage(AnaSayfa anaSayfaPage)
        {
            InitializeComponent();
            _anaSayfaPage = anaSayfaPage;
        }
        
        private void OnKofteEkleClicked(object sender, EventArgs e)
        {
            _kofteAdet++;
            LabelKofteAdet.Text = _kofteAdet.ToString();
            LabelKofteKalori.Text = $"Toplam: {_kofteAdet * KOFTE_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(KOFTE_KALORI, "Köfte", "Akşam");
        }

        private void OnKofteSilClicked(object sender, EventArgs e)
        {
            if (_kofteAdet > 0)
            {
                _kofteAdet--;
                LabelKofteAdet.Text = _kofteAdet.ToString();
                LabelKofteKalori.Text = $"Toplam: {_kofteAdet * KOFTE_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(KOFTE_KALORI, "Köfte", "Akşam");
            }
        }
        
        private void OnMakarnaEkleClicked(object sender, EventArgs e)
        {
            _makarnaAdet++;
            LabelMakarnaAdet.Text = _makarnaAdet.ToString();
            LabelMakarnaKalori.Text = $"Toplam: {_makarnaAdet * MAKARNA_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(MAKARNA_KALORI, "Makarna", "Akşam");
        }

        private void OnMakarnaSilClicked(object sender, EventArgs e)
        {
            if (_makarnaAdet > 0)
            {
                _makarnaAdet--;
                LabelMakarnaAdet.Text = _makarnaAdet.ToString();
                LabelMakarnaKalori.Text = $"Toplam: {_makarnaAdet * MAKARNA_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(MAKARNA_KALORI, "Makarna", "Akşam");
            }
        }
        
        private void OnPatatesEkleClicked(object sender, EventArgs e)
        {
            _patatesAdet++;
            LabelPatatesAdet.Text = _patatesAdet.ToString();
            LabelPatatesKalori.Text = $"Toplam: {_patatesAdet * PATATES_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(PATATES_KALORI, "Patates Kızartması", "Akşam");
        }

        private void OnPatatesSilClicked(object sender, EventArgs e)
        {
            if (_patatesAdet > 0)
            {
                _patatesAdet--;
                LabelPatatesAdet.Text = _patatesAdet.ToString();
                LabelPatatesKalori.Text = $"Toplam: {_patatesAdet * PATATES_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(PATATES_KALORI, "Patates Kızartması", "Akşam");
            }
        }
        
        private void OnMantiEkleClicked(object sender, EventArgs e)
        {
            _mantiAdet++;
            LabelMantiAdet.Text = _mantiAdet.ToString();
            LabelMantiKalori.Text = $"Toplam: {_mantiAdet * MANTI_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(MANTI_KALORI, "Mantı", "Akşam");
        }

        private void OnMantiSilClicked(object sender, EventArgs e)
        {
            if (_mantiAdet > 0)
            {
                _mantiAdet--;
                LabelMantiAdet.Text = _mantiAdet.ToString();
                LabelMantiKalori.Text = $"Toplam: {_mantiAdet * MANTI_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(MANTI_KALORI, "Mantı", "Akşam");
            }
        }

        private async void OnTamamlaClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
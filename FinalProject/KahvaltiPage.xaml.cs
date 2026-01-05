namespace FinalProject
{
    public partial class KahvaltiPage : ContentPage
    {
        private AnaSayfa _anaSayfaPage;
        private int _zeytinAdet = 0;
        private int _yumurtaAdet = 0;
        private int _ekmekAdet = 0;
        private int _sutAdet = 0;
        
        private const int ZEYTIN_KALORI = 5;
        private const int YUMURTA_KALORI = 155;
        private const int EKMEK_KALORI = 70;
        private const int SUT_KALORI = 120;

        public KahvaltiPage(AnaSayfa anaSayfaPage)
        {
            InitializeComponent();
            _anaSayfaPage = anaSayfaPage;
        }
        
        private void OnZeytinEkleClicked(object sender, EventArgs e)
        {
            _zeytinAdet++;
            LabelZeytinAdet.Text = _zeytinAdet.ToString();
            LabelZeytinKalori.Text = $"Toplam: {_zeytinAdet * ZEYTIN_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(ZEYTIN_KALORI, "Zeytin", "Kahvaltı");
        }

        private void OnZeytinSilClicked(object sender, EventArgs e)
        {
            if (_zeytinAdet > 0)
            {
                _zeytinAdet--;
                LabelZeytinAdet.Text = _zeytinAdet.ToString();
                LabelZeytinKalori.Text = $"Toplam: {_zeytinAdet * ZEYTIN_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(ZEYTIN_KALORI, "Zeytin", "Kahvaltı");
            }
        }
        
        private void OnYumurtaEkleClicked(object sender, EventArgs e)
        {
            _yumurtaAdet++;
            LabelYumurtaAdet.Text = _yumurtaAdet.ToString();
            LabelYumurtaKalori.Text = $"Toplam: {_yumurtaAdet * YUMURTA_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(YUMURTA_KALORI, "Haşlanmış Yumurta", "Kahvaltı");
        }

        private void OnYumurtaSilClicked(object sender, EventArgs e)
        {
            if (_yumurtaAdet > 0)
            {
                _yumurtaAdet--;
                LabelYumurtaAdet.Text = _yumurtaAdet.ToString();
                LabelYumurtaKalori.Text = $"Toplam: {_yumurtaAdet * YUMURTA_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(YUMURTA_KALORI, "Haşlanmış Yumurta", "Kahvaltı");
            }
        }
        
        private void OnEkmekEkleClicked(object sender, EventArgs e)
        {
            _ekmekAdet++;
            LabelEkmekAdet.Text = _ekmekAdet.ToString();
            LabelEkmekKalori.Text = $"Toplam: {_ekmekAdet * EKMEK_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(EKMEK_KALORI, "Ekmek", "Kahvaltı");
        }

        private void OnEkmekSilClicked(object sender, EventArgs e)
        {
            if (_ekmekAdet > 0)
            {
                _ekmekAdet--;
                LabelEkmekAdet.Text = _ekmekAdet.ToString();
                LabelEkmekKalori.Text = $"Toplam: {_ekmekAdet * EKMEK_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(EKMEK_KALORI, "Ekmek", "Kahvaltı");
            }
        }
        
        private void OnSutEkleClicked(object sender, EventArgs e)
        {
            _sutAdet++;
            LabelSutAdet.Text = _sutAdet.ToString();
            LabelSutKalori.Text = $"Toplam: {_sutAdet * SUT_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(SUT_KALORI, "Süt", "Kahvaltı");
        }

        private void OnSutSilClicked(object sender, EventArgs e)
        {
            if (_sutAdet > 0)
            {
                _sutAdet--;
                LabelSutAdet.Text = _sutAdet.ToString();
                LabelSutKalori.Text = $"Toplam: {_sutAdet * SUT_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(SUT_KALORI, "Süt", "Kahvaltı");
            }
        }

        private async void OnTamamlaClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
using FinalProject.Models;

namespace FinalProject
{
    public partial class OglePage : ContentPage
    {
        private AnaSayfa _anaSayfaPage;
        private int _pilavAdet = 0;
        private int _corbaAdet = 0;
        private int _fasulyeAdet = 0;
        private int _ispanakAdet = 0;
        
        private const int PILAV_KALORI = 250;
        private const int CORBA_KALORI = 66;
        private const int FASULYE_KALORI = 73;
        private const int ISPANAK_KALORI = 133;

        public OglePage(AnaSayfa anaSayfaPage)
        {
            InitializeComponent();
            _anaSayfaPage = anaSayfaPage;
        }
        
        private void OnPilavEkleClicked(object sender, EventArgs e)
        {
            _pilavAdet++;
            LabelPilavAdet.Text = _pilavAdet.ToString();
            LabelPilavKalori.Text = $"Toplam: {_pilavAdet * PILAV_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(PILAV_KALORI, "Pirinç Pilavı", "Öğle");
        }

        private void OnPilavSilClicked(object sender, EventArgs e)
        {
            if (_pilavAdet > 0)
            {
                _pilavAdet--;
                LabelPilavAdet.Text = _pilavAdet.ToString();
                LabelPilavKalori.Text = $"Toplam: {_pilavAdet * PILAV_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(PILAV_KALORI, "Pirinç Pilavı", "Öğle");
            }
        }
        
        private void OnCorbaEkleClicked(object sender, EventArgs e)
        {
            _corbaAdet++;
            LabelCorbaAdet.Text = _corbaAdet.ToString();
            LabelCorbaKalori.Text = $"Toplam: {_corbaAdet * CORBA_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(CORBA_KALORI, "Domates Çorbası", "Öğle");
        }

        private void OnCorbaSilClicked(object sender, EventArgs e)
        {
            if (_corbaAdet > 0)
            {
                _corbaAdet--;
                LabelCorbaAdet.Text = _corbaAdet.ToString();
                LabelCorbaKalori.Text = $"Toplam: {_corbaAdet * CORBA_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(CORBA_KALORI, "Domates Çorbası", "Öğle");
            }
        }
        
        private void OnFasulyeEkleClicked(object sender, EventArgs e)
        {
            _fasulyeAdet++;
            LabelFasulyeAdet.Text = _fasulyeAdet.ToString();
            LabelFasulyeKalori.Text = $"Toplam: {_fasulyeAdet * FASULYE_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(FASULYE_KALORI, "Zeytinyağlı Fasulye", "Öğle");
        }

        private void OnFasulyeSilClicked(object sender, EventArgs e)
        {
            if (_fasulyeAdet > 0)
            {
                _fasulyeAdet--;
                LabelFasulyeAdet.Text = _fasulyeAdet.ToString();
                LabelFasulyeKalori.Text = $"Toplam: {_fasulyeAdet * FASULYE_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(FASULYE_KALORI, "Zeytinyağlı Fasulye", "Öğle");
            }
        }
        
        private void OnIspanakEkleClicked(object sender, EventArgs e)
        {
            _ispanakAdet++;
            LabelIspanakAdet.Text = _ispanakAdet.ToString();
            LabelIspanakKalori.Text = $"Toplam: {_ispanakAdet * ISPANAK_KALORI} kalori";
            
            _anaSayfaPage.KaloriEkle(ISPANAK_KALORI, "Ispanak Yemeği", "Öğle");
        }

        private void OnIspanakSilClicked(object sender, EventArgs e)
        {
            if (_ispanakAdet > 0)
            {
                _ispanakAdet--;
                LabelIspanakAdet.Text = _ispanakAdet.ToString();
                LabelIspanakKalori.Text = $"Toplam: {_ispanakAdet * ISPANAK_KALORI} kalori";
                
                _anaSayfaPage.KaloriCikar(ISPANAK_KALORI, "Ispanak Yemeği", "Öğle");
            }
        }

        private async void OnTamamlaClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
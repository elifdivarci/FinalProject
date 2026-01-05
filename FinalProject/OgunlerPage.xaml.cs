using FinalProject.Services;

namespace FinalProject
{
    public partial class OgunlerPage : ContentPage
    {
        private AnaSayfa _anaSayfaPage;
        private DbService _dbService;

        public OgunlerPage(AnaSayfa anaSayfaPage)
        {
            InitializeComponent();
            _anaSayfaPage = anaSayfaPage;
            _dbService = new DbService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VerileriYukle();
        }

        private async Task VerileriYukle()
        {
            var tarihStr = DateTime.Today.ToString("yyyy-MM-dd");
            
            var kahvaltiKalori = await _dbService.GetOgunToplamKaloriAsync(tarihStr, "Kahvaltı");
            var ogleKalori = await _dbService.GetOgunToplamKaloriAsync(tarihStr, "Öğle");
            var aksamKalori = await _dbService.GetOgunToplamKaloriAsync(tarihStr, "Akşam");
            
            LabelKahvaltiKalori.Text = $"{kahvaltiKalori:F0} kcal";
            LabelOgleKalori.Text = $"{ogleKalori:F0} kcal";
            LabelAksamKalori.Text = $"{aksamKalori:F0} kcal";
        }

        private async void OnKahvaltiClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KahvaltiPage(_anaSayfaPage));
        }

        private async void OnOgleClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OglePage(_anaSayfaPage));
        }

        private async void OnAksamClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AksamPage(_anaSayfaPage));
        }
    }
}
using FinalProject.Services;

namespace FinalProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        var dbService = new DbService();
        var mevcutVeri = Task.Run(async () => await dbService.GetSonVeriAsync()).Result;

        if (mevcutVeri != null && mevcutVeri.BMH > 0)
        {
            MainPage = new NavigationPage(new AnaSayfa(mevcutVeri.BMH));
        }
        else
        {
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
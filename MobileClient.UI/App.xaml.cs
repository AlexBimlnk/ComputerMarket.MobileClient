namespace MobileClient.UI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Current.UserAppTheme = AppTheme.Dark;

        MainPage = new AppShellMobile();
    }
}
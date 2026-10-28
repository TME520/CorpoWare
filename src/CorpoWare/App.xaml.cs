using System.Windows;
using CorpoWare.Persistence;

namespace CorpoWare;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            var store = AppStore.Open();
            var window = new MainWindow(store);
            MainWindow = window;
            window.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Could not open the local data file next to the app." + Environment.NewLine + Environment.NewLine + ex.Message,
                "CorpoWare",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown(1);
        }
    }
}

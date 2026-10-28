using System.IO;
using System.Windows;
using System.Windows.Controls;
using CorpoWare.Persistence;

namespace CorpoWare.Views;

public partial class RoutinesView : UserControl
{
    private readonly AppStore _store;

    public RoutinesView(AppStore store)
    {
        InitializeComponent();
        _store = store;
        RoutineList.ItemsSource = store.State.Routines;
        RoutineList.AddHandler(CheckBox.ClickEvent, new RoutedEventHandler(OnStepToggled));
        EmptyText.Visibility = store.State.Routines.Count == 0
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void OnStepToggled(object sender, RoutedEventArgs e)
    {
        try
        {
            _store.Save();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show(
                Window.GetWindow(this),
                "Could not save the data file next to the app." + Environment.NewLine + Environment.NewLine + ex.Message,
                "CorpoWare",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}

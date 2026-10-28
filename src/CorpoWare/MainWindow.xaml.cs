using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using CorpoWare.Persistence;
using CorpoWare.Views;

namespace CorpoWare;

public partial class MainWindow : Window
{
    private readonly AppStore _store;
    private readonly Dictionary<string, UserControl> _pages = new();

    public MainWindow(AppStore store)
    {
        InitializeComponent();
        _store = store;

        var version = Assembly.GetExecutingAssembly().GetName().Version;
        VersionText.Text = version is null ? "v0.1.0" : $"v{version.ToString(3)}";
        DataPathText.Text = "Data file: " + store.DataFilePath;
        DataPathText.ToolTip = store.DataFilePath;

        NavList.SelectedIndex = 0;
    }

    private void OnNavSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (NavList.SelectedItem is not ListBoxItem item || item.Tag is not string key)
        {
            return;
        }

        if (!_pages.TryGetValue(key, out var page))
        {
            page = key switch
            {
                "impromptu" => new ImpromptuView(_store),
                "kanban" => new KanbanView(_store),
                "routines" => new RoutinesView(_store),
                _ => throw new InvalidOperationException($"Unknown section '{key}'.")
            };
            _pages[key] = page;
        }

        PageHost.Content = page;
    }
}

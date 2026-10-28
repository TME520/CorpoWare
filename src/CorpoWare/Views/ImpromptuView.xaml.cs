using System.Windows;
using System.Windows.Controls;
using CorpoWare.Persistence;

namespace CorpoWare.Views;

public partial class ImpromptuView : UserControl
{
    public ImpromptuView(AppStore store)
    {
        InitializeComponent();
        ItemList.ItemsSource = store.State.Impromptu;
        EmptyText.Visibility = store.State.Impromptu.Count == 0
            ? Visibility.Visible
            : Visibility.Collapsed;
    }
}

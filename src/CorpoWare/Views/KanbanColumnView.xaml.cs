using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CorpoWare.Models;

namespace CorpoWare.Views;

public partial class KanbanColumnView : UserControl
{
    public KanbanColumnView(string title, IReadOnlyList<KanbanCard> cards, Color accent)
    {
        InitializeComponent();
        TitleText.Text = title;
        CountText.Text = cards.Count.ToString(CultureInfo.InvariantCulture);
        HeaderBorder.BorderBrush = new SolidColorBrush(accent);
        CardList.ItemsSource = cards;
        EmptyText.Visibility = cards.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }
}

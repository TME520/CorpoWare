using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CorpoWare.Models;
using CorpoWare.Persistence;

namespace CorpoWare.Views;

public partial class KanbanView : UserControl
{
    public KanbanView(AppStore store)
    {
        InitializeComponent();

        (BoardColumn Column, Color Accent)[] columns =
        [
            (BoardColumn.Backlog, Color.FromRgb(0x8A, 0x94, 0xA6)),
            (BoardColumn.Todo, Color.FromRgb(0x1F, 0x5F, 0xBF)),
            (BoardColumn.Doing, Color.FromRgb(0xB8, 0x6E, 0x00)),
            (BoardColumn.Done, Color.FromRgb(0x1F, 0x7A, 0x4D))
        ];

        for (var index = 0; index < columns.Length; index++)
        {
            var (column, accent) = columns[index];
            var cards = store.State.Cards.Where(card => card.Column == column).ToList();
            var view = new KanbanColumnView(column.ToLabel(), cards, accent)
            {
                Margin = new Thickness(index == 0 ? 0 : 8, 0, 0, 0)
            };
            Grid.SetColumn(view, index);
            ColumnGrid.Children.Add(view);
        }
    }
}

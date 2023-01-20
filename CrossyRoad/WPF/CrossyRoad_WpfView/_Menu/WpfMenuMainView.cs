using CrossyRoad_Model._Menu;
using CrossyRoad_View._Menu;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._Menu
{
  /// <summary>
  /// Представление окна главного меню в приложении WPF
  /// </summary>
  public class WpfMenuMainView : MenuView
  {
    /// <summary>
    /// Графический элемент главного меню
    /// </summary>
    private Grid _element;

    /// <summary>
    /// Конструктор представления окна главного меню в приложении WPF
    /// </summary>
    public WpfMenuMainView()
      : base(MenuMain.Instance)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _element = new Grid
        {
          Margin = new Thickness(0, WpfViewConfiguration.DEFAULT_MARGIN, 0, 0)
        };
        _element.RowDefinitions.Add(new RowDefinition());
        _element.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        _element.RowDefinitions.Add(new RowDefinition());
        _element.ColumnDefinitions.Add(new ColumnDefinition());
        _element.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        _element.ColumnDefinitions.Add(new ColumnDefinition());
        StackPanel panel = new StackPanel();
        Grid.SetRow(panel, 1);
        Grid.SetColumn(panel, 1);
        _ = _element.Children.Add(panel);
        foreach (CrossyRoad_Model._Menu.MenuItem elItem in Menu.Items)
        {
          WpfMenuMainItemView menuItemView = new WpfMenuMainItemView(elItem);
          _ = panel.Children.Add(menuItemView.Element);
          elItem.Changed += menuItemView.Draw;
          ItemsViews.Add(menuItemView);
        }
      });
    }

    /// <summary>
    /// Отображает главное меню на экране приложения
    /// </summary>
    public override void Draw()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        WpfUtils.ClearWindow();
        WpfUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_Menu);
        WpfUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Menu_GameName);
        WpfUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Menu_Title);
        WpfUtils.DrawOnWindow(_element);
      });
    }
  }
}
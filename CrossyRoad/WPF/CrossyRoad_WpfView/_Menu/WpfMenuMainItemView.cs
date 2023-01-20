using CrossyRoad_View._Menu;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._Menu
{
  /// <summary>
  /// Представление пункта меню в приложении WPF
  /// </summary>
  public class WpfMenuMainItemView : MenuItemView
  {
    /// <summary>
    /// Графический элемент пункта меню
    /// </summary>
    public TextBlock Element { get; private set; }

    /// <summary>
    /// Конструктор представления пункта меню в приложении WPF
    /// </summary>
    /// <param name="parItem">Модель пункта меню</param>
    public WpfMenuMainItemView(CrossyRoad_Model._Menu.MenuItem parItem)
      : base(parItem)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Element = new TextBlock
        {
          Text = Item.Name,
          HorizontalAlignment = HorizontalAlignment.Center,
          FontSize = WpfViewConfiguration.DEFAULT_FONT_SIZE,
          TextAlignment = TextAlignment.Center,
          Foreground = WpfViewConfiguration.MenuItemStatesColors[Item.State],
          Margin = new Thickness(0, 0, 0, WpfViewConfiguration.DEFAULT_MARGIN)
        };
      });
    }

    /// <summary>
    /// Отображает пункт меню на экране приложения
    /// </summary>
    public override void Draw()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        Element.Foreground = WpfViewConfiguration.MenuItemStatesColors[Item.State];
      });
    }
  }
}